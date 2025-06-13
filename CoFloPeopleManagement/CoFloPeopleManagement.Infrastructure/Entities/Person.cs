using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoFloPeopleManagement.Infrastructure.Entities;

public class Person
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(255)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public DateTime DateOfBirth { get; set; }

    public DateTime DateCreated { get; set; }

    // Age can be calculated by using DateOfBirth when being retrieved
    [NotMapped]
    public int Age
    {
        get
        {
            var today = DateTime.Today;
            var age = today.Year - DateOfBirth.Year;
            if (DateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}