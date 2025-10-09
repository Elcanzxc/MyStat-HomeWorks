
using Microsoft.EntityFrameworkCore;
using ServerApp.Sql;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerApp.Server;

public class Server
{

    private string Ip { get; }
    private int Port { get; }

    private TcpListener tcpListener;

    private TcpClient tcpClient;

    private NetworkStream stream;

    private readonly Dictionary<TcpClient, string> tcpСlients = new();

    private readonly ChatDbContext db;
    public Server(string Ip, int Port, ChatDbContext db)
    {
        if (string.IsNullOrEmpty(Ip))
            throw new ArgumentNullException(nameof(Ip));
        this.Ip = Ip;
        if (!int.IsPositive(Port))
            throw new ArgumentException(nameof(Port));
        this.Port = Port;
        this.db = db;
    }

    public async Task StartAsync()
    {
        tcpListener = new TcpListener(IPAddress.Parse(Ip), Port);
        tcpListener.Start();
        Console.WriteLine($"✅ Server started on {Ip}:{Port}");



        while (true)
        {
            var client = await tcpListener.AcceptTcpClientAsync();
            _ = HandleClientAsync(client);
        }
    }

    private async Task SendChatHistoryAsync(TcpClient client)
    {

            var history = await db.Messages
                .Include(m => m.User)
                .OrderBy(m => m.SentAt)
                .ToListAsync();

            if (history.Count == 0)
            {
                await SendToClientAsync(client, "📭 История пуста.");
                return;
            }

            await SendToClientAsync(client, "📜 История чата:");
            foreach (var m in history)
            {
                string formatted = $"[{m.SentAt:HH:mm}] {m.User.Nickname}: {m.Text}";
                await SendToClientAsync(client, formatted);
            }

            await SendToClientAsync(client, "──────────────────────────────");
   
    }
    private async Task SendToClientAsync(TcpClient client, string message)
    {
        try
        {
            byte[] data = Encoding.UTF8.GetBytes(message + Environment.NewLine);
            await client.GetStream().WriteAsync(data);
        }
        catch
        {
        }
    }
    private async Task HandleClientAsync(TcpClient client)
    {
        var stream = client.GetStream();
        var reader = new StreamReader(stream, Encoding.UTF8);
        var writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

        User? userEntity = null;
        string nickname = "Unknown";
        try
        {

            var ip = (client.Client.RemoteEndPoint as IPEndPoint)?.Address.ToString() ?? "Unknown";

            await writer.WriteLineAsync("Введите ваш никнейм:");
            nickname = (await reader.ReadLineAsync() ?? "Unknown").Trim();
            if (string.IsNullOrWhiteSpace(nickname))
                nickname = "Unknown";

            bool isNicknameTaken;
            lock (tcpСlients)
            {
                isNicknameTaken = tcpСlients.ContainsValue(nickname);
            }

            if (isNicknameTaken)
            {
                await writer.WriteLineAsync($"❌ Ошибка: Никнейм '{nickname}' уже используется в активной сессии.");
                client.Close();
                return;
            }

            await using (var db = new ChatDbContext())
            {
                userEntity = await db.Users.FirstOrDefaultAsync(u => u.Nickname == nickname);

                if (userEntity == null)
                {
                    userEntity = new User
                    {
                        Nickname = nickname,
                        IpAddress = ip,
                        ConnectedAt = DateTime.UtcNow
                    };
                    db.Users.Add(userEntity);

                    try
                    {

                        await db.SaveChangesAsync();
                    }
                    catch (DbUpdateException)
                    {

                        db.ChangeTracker.Clear();
                        userEntity = await db.Users.FirstAsync(u => u.Nickname == nickname);
                    }
                }
                else
                {
 
                    userEntity.IpAddress = ip;
                    userEntity.ConnectedAt = DateTime.UtcNow;
                    userEntity.DisconnectedAt = null;
                    await db.SaveChangesAsync();
                }
            }

            await SendChatHistoryAsync(client);


            lock (tcpСlients)
                tcpСlients[client] = nickname;

            Console.WriteLine($"🟢 {nickname} подключился ({client.Client.RemoteEndPoint})");
            await BroadcastMessageAsync($"🟢 {nickname} вошёл в чат!", client);

     
            while (true)
            {
                string? message = await reader.ReadLineAsync();
                if (message == null) break;

                Console.WriteLine($"💬 [{nickname}]: {message}");
                await BroadcastMessageAsync($"[{nickname}]: {message}", client);

                using (var db = new ChatDbContext())
                {
                    var userEntity2 = await db.Users.FirstOrDefaultAsync(u => u.Nickname == nickname);
                    if (userEntity2 != null)
                    {
                        db.Messages.Add(new Message
                        {
                            Text = message,
                            SentAt = DateTime.Now,
                            UserId = userEntity2.Id
                        });
                        await db.SaveChangesAsync();
                    }
                }
            }
        }
        catch (IOException)
        {
        }
        finally
        {
            string name;
            lock (tcpСlients)
            {
                name = tcpСlients.GetValueOrDefault(client, "Unknown");
                tcpСlients.Remove(client);
            }

            client.Close();
            Console.WriteLine($"🔴 {name} покинул чат");
            await BroadcastMessageAsync($"🔴 {name} вышел из чата");

            using (var db = new ChatDbContext())
            {
                var userEntity3 = await db.Users.FirstOrDefaultAsync(u => u.Nickname == name && u.DisconnectedAt == null);
                if (userEntity3 != null)
                {
                    userEntity3.DisconnectedAt = DateTime.Now;
                    await db.SaveChangesAsync();
                }
            }
        }
    }



    private async Task BroadcastMessageAsync(string message, TcpClient? sender = null)
    {
        byte[] data = Encoding.UTF8.GetBytes(message + Environment.NewLine);

        List<TcpClient> clientsSnapshot;
        lock (tcpСlients)
        {
            clientsSnapshot = tcpСlients.Keys.ToList();
        }

        foreach (var client in clientsSnapshot)
        {
            if (client == sender) continue;
            try
            {
                await client.GetStream().WriteAsync(data);
            }
            catch
            {

            }
        }
    }
}
