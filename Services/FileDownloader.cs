namespace NetCoreMvc.Services;

public class FileDownloader : IFileDownloader
{
    public (MemoryStream? ms, string? contentType, string? errorMessage) DownloadFile(string filePhysicalPath)
    {
        if (System.IO.File.Exists(filePhysicalPath))
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePhysicalPath);
            MemoryStream ms = new(fileBytes);

            string extension = Path.GetExtension(filePhysicalPath);
            string contentType = extension switch
            {
                ".txt" => "text/plain",
                ".gif" => "image/gif",
                ".png" => "image/png",
                ".jpeg" => "image/jpeg",
                ".pdf" => "application/pdf",
                ".docx" => "application/vnd.ms-word",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                _ => ""
            };

            if (string.IsNullOrWhiteSpace(contentType))
            {
                return (null, null, "File not supported.");
            }

            return (ms, contentType, null);
        }

        return (null, null, "File not found.");
    }
}
