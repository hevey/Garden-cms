using System.ComponentModel.DataAnnotations;

namespace Garden.Shared.Models;

public class User
{
    [Required]
    [EmailAddress(ErrorMessage = "Invalid Email")]
    public string Email { get; set; }
 
    [Required]
    public string Password { get; set; }
}