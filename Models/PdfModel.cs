using System.ComponentModel.DataAnnotations;

namespace NetCoreMvc.Models;

public class PdfModel
{
    [Required(ErrorMessage = "The razor content is required")]
    [Display(Name = "Razor Content")]
    public string? RazorContent { get; set; }
}
