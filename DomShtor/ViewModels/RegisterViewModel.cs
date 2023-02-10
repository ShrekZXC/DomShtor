using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DomShtor.Controllers;
using DomShtor.DAL.Models;
using Mysqlx;
using Mysqlx.Session;

namespace DomShtor.ViewModels;

public class RegisterViewModel
{
    public RegisterViewModel()
    {
        
    }
    
    [Required(ErrorMessage = "Укажите почту")]
    [EmailAddress(ErrorMessage = "Неверный формат почты")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Укажите имя")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "Укажите фамилию")]
    public string SecondName { get; set; }
    
    public string LastName { get; set; }
    
    [Required(ErrorMessage = "Введите пароль")]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "Введите пароль")]
    public string ReenterPassword { get; set; }
}