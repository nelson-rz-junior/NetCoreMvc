using System.ComponentModel.DataAnnotations;

namespace NetCoreMvc.Models;

public class UploadFileModel
{
    [Required(ErrorMessage = "Please select a file")]
    public List<IFormFile>? Files { get; set; }
}
