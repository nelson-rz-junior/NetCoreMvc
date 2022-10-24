using System.ComponentModel.DataAnnotations;

namespace NetCoreMvc.Models;

public class ExampleModel
{
    public int Id { get; set; }

    [Display(Name = "First Name")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last Name")]
    public string LastName { get; set; } = null!;

    public int Age { get; set; }
}
