using System.IO;
using System.Net.Http;
using System.Net.Sockets;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Client;

public class Client
{

    private string Host {  get; }
    private int Port {  get; }

    private TcpClient tcpClient;

    private NetworkStream stream;

    private StreamReader reader;
    private StreamWriter writer;

    public Client(string host, int port)
    {
        if(string.IsNullOrEmpty(host))
            throw new ArgumentNullException(nameof(host));
        this.Host = host;
        if (!int.IsPositive(port))
            throw new ArgumentNullException(nameof(port));
        this.Port = port;
    }

    public async Task ConnectAsync()
    {
        this.tcpClient = new TcpClient();
        await tcpClient.ConnectAsync(Host, Port);
        Console.WriteLine($"✅ Connected to server {Host}:{Port}");

        stream = tcpClient.GetStream();
        reader = new StreamReader(stream, Encoding.UTF8);
        writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

        _ = ListenAsync(); 
    }

    private async Task ListenAsync()
    {
        try
        {
            while (true)
            {
                var message = await reader.ReadLineAsync();
                if (message == null)
                {
                    Console.WriteLine("🔴 Server disconnected.");
                    break;
                }
                Console.WriteLine($"📩 Server: {message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Connection error: {ex.Message}");
        }
    }

    public async Task SendMessageAsync(string message)
    {
        if (string.IsNullOrEmpty(message))
            return;

        await writer.WriteLineAsync(message);
    }

    public void Disconnect()
    {
        writer?.Dispose();
        reader?.Dispose();
        stream?.Dispose();
        tcpClient?.Close();
        Console.WriteLine("🟡 Disconnected from server.");
    }
}
