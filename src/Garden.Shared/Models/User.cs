using System.ComponentModel.DataAnnotations;

namespace Garden.Shared.Models;

public class User
{

    [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email")]
    public string Email { get; set; }
 
    [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
     public string Password { get; set; }
}