using System.ComponentModel.DataAnnotations;
using CoFloPeopleManagement.Application.Validations;

namespace CoFloPeopleManagement.Application.DTOs.Person;

public class PersonDtoRequest
{
    [Required(ErrorMessage = "First name is required.")]
    [StringLength(255, ErrorMessage = "First name cannot be longer than 255 characters.")]
    public string FirstName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(255, ErrorMessage = "Last name cannot be longer than 255 characters.")]
    public string LastName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Date of birth is required.")]
    [DateNotInFuture]
    public DateTime DateOfBirth { get; set; }
}