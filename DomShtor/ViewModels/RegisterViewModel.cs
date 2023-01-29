using DomShtor.Controllers;
using DomShtor.DAL.Models;
using Mysqlx.Session;

namespace DomShtor.ViewModels;

public class RegisterViewModel
{
    public RegisterViewModel()
    {
        
    }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
}