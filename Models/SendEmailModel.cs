using System.ComponentModel.DataAnnotations;

namespace NetCoreMvc.Models;

public class SendEmailModel
{
    [Required(ErrorMessage = "The full name is required")]
    [Display(Name = "Full Name")]
    public string? FullName { get; set; }

    [Required(ErrorMessage = "The address is required")]
    [DataType(DataType.EmailAddress)]
    public string? Address { get; set; }

    [Required(ErrorMessage = "The subject is required")]
    public string? Subject { get; set; }

    [Required(ErrorMessage = "The message is required")]
    public string? Message { get; set; }
}
