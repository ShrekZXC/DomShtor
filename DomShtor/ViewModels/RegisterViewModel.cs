using System.ComponentModel.DataAnnotations;
using DomShtor.Controllers;
using DomShtor.DAL.Models;
using Mysqlx.Session;

namespace DomShtor.ViewModels;

public class RegisterViewModel
{
    public RegisterViewModel()
    {
        
    }
    
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string SecondName { get; set; }
    
    public string LastName { get; set; }
    
    [Required]
    public string Password { get; set; }
}