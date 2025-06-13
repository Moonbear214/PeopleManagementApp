namespace CoFloPeopleManagement.Application.DTOs.Person;

// DTO for returning person data
public class PersonDtoResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public int Age { get; set; }
    public DateTime DateCreated { get; set; }
}