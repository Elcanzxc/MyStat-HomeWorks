
using FluentFTP;


namespace FTP.Models;
public class FtpClientManager
{
    private readonly AsyncFtpClient client;

    public FtpClientManager(string host) 
    {
        client = new AsyncFtpClient(host);
    }


    public async Task<List<FtpItem>> ListDirectoryAsync(string remotePath)
    {
        var items = new List<FtpItem>();


        await client.AutoConnect();

        foreach (var ftpListItem in await client.GetListing(remotePath))
        {
            items.Add(new FtpItem
            {
                Name = ftpListItem.Name,
                IsDirectory = ftpListItem.Type == FtpObjectType.Directory
            });
        }

        return items.OrderByDescending(i => i.IsDirectory).ThenBy(i => i.Name).ToList();
    }

    public async Task DownloadFileAsync(string remoteFilePath, string localFilePath)
    {
        Console.WriteLine($"\n[Downloader] Начало загрузки{Path.GetFileName(localFilePath)}...");


        await client.AutoConnect();


       await client.DownloadFile(localFilePath, remoteFilePath, FtpLocalExists.Overwrite, FtpVerify.None);


       Console.WriteLine($"\r[Downloader] Успешно закончено: {localFilePath}");
        

    }

}