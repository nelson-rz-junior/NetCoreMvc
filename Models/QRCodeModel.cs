using System.ComponentModel.DataAnnotations;

namespace NetCoreMvc.Models;

public class QRCodeModel
{
    [Required(ErrorMessage = "The Url is required")]
    [DataType(DataType.Url)]
    public string? Url { get; set; }

    public string? QrCode { get; set; }
}
