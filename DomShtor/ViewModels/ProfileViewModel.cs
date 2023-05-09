using System.ComponentModel.DataAnnotations;

namespace DomShtor.ViewModels;

public class ProfileViewModel
{
    public int? ProfileId { get; set; }
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? SecondName { get; set; }
    [Required]
    public string? LastName { get; set; }
}