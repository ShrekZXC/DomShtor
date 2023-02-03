using System.ComponentModel.DataAnnotations;

namespace DomShtor.ViewModels;

public class LoginViewModel
{
    public LoginViewModel()
    {
        
    }
    
    [Required(ErrorMessage = "Введите почту")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Введите пароль")]
    public string Password { get; set; }
    
    [Required]
    public  bool RememberMe { get; set; }
}