namespace NetCoreMvc.Services;

public interface IPdfGenerator
{
    byte[] GeneratePdf(string html);
}
