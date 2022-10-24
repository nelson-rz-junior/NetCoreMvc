namespace NetCoreMvc.Services;

public interface IFileDownloader
{
    (MemoryStream? ms, string? contentType, string? errorMessage) DownloadFile(string filePhysicalPath);
}
