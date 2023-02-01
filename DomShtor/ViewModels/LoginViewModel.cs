using System.ComponentModel.DataAnnotations;

namespace DomShtor.ViewModels;

public class LoginViewModel
{
    public LoginViewModel()
    {
        
    }
    
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    [Required]
    public  bool RememberMe { get; set; }
}