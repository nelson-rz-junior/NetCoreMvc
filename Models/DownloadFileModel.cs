namespace NetCoreMvc.Models;

public class DownloadFileModel
{
    public string? FileName { get; set; }

    public long Size { get; set; }

    public DateTime LastAccess { get; set; }
}
