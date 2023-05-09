namespace DomShtor.DAL.Models;

public class ProfileModel
{
    public  int? ProfileId { get; set; }
    
    public int UserId { get; set; }
    public string? FirstName { get; set; }
    public string? SecondName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    
    public  string? ProfileImage { get; set; }
}