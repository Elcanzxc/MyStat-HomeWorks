using ClientApp.Client;

class Program
{
    static async Task Main(string[] args)
    {
        var client = new Client("127.0.0.1", 7777);
        await client.ConnectAsync();

        while (true)
        {
            var input = Console.ReadLine();
            if (string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase))
                break;

            await client.SendMessageAsync(input);
        }

        client.Disconnect();
    }
}