using System.ComponentModel.DataAnnotations;

namespace DomShtor.DAL.Models;

public class UserModel
{
    [Key]
    public int? UserId { get; set; }
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string SecondName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Password { get; set; } = null!;

    public string ReenterPassword { get; set; } = null!;
    public string Salt { get; set; } = null!;

    public int Status { get; set; } = 0;
}