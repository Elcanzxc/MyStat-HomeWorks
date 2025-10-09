using FTP.Models;
using System.Text;

// Вообще не понравился способ как с этим протоколом работать в c#
class Program
{
    private static FtpClientManager ftpManager;
    private static string currentPath;
    public static async Task Main(string[] args)
    {

        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        var server = "ftp5.us.freebsd.org";
        var initialPath = "/pub/CRAN/web/packages/gtExtras/refman/";


        currentPath = initialPath;
        ftpManager = new FtpClientManager(server);


        while (true)
        {
            List<FtpItem> ftpItems;
            try
            {
                ftpItems = await ftpManager.ListDirectoryAsync(currentPath);
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine($"Ошибка при доступе к '{currentPath}': {ex.Message}");
                Console.ReadKey();
                GoUpDirectory();
                continue;
            }

            var menuItems = new List<MenuItem>();

            menuItems.Add(new MenuItem("..", () =>
            {
                GoUpDirectory();
                return Task.FromResult<string>(null);
            }));

            foreach (var item in ftpItems)
            {
                string remoteItemPath = CombineRemotePath(currentPath, item.Name);

                if (item.IsDirectory)
                {
                    menuItems.Add(new MenuItem($"📁 {item.Name}", () =>
                    {
                        currentPath = remoteItemPath;
                        return Task.FromResult<string>(null);
                    }));
                }
                else
                {
                    menuItems.Add(new MenuItem($"📄 {item.Name}", async () =>
                    {
        
                        string localFile = Path.Combine(AppContext.BaseDirectory, item.Name);

                        await ftpManager.DownloadFileAsync(remoteItemPath, localFile);

                        return $"✅ Файл '{item.Name}' скачан в папку запуска программы.";
                    }));
                }
            }

            await MenuManager.DisplayMenu(menuItems, currentPath);
        }
    }



private static string CombineRemotePath(string path1, string path2)
{
    return path1.EndsWith("/") ? path1 + path2 : path1 + "/" + path2;
}

private static void GoUpDirectory()
{
    if (currentPath == "/") return;

    int lastSlash = currentPath.TrimEnd('/').LastIndexOf('/');
    currentPath = lastSlash > 0 ? currentPath.Substring(0, lastSlash) : "/";
}
}
