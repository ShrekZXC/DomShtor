using System.ComponentModel.DataAnnotations;

namespace DomShtor.DAL.Models;

public class UserModel
{
    [Key]
    public int? UserId { get; set; }

    public string Email { get; set; } = null!;
    
    private string Password { get; set; } = null!;
    
    private string Salt { get; set; } = null!;

    public int Status { get; set; } = 0;
}