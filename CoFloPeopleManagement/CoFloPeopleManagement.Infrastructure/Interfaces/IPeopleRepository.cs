using CoFloPeopleManagement.Infrastructure.Entities;

namespace CoFloPeopleManagement.Infrastructure.Interfaces;

public interface IPeopleRepository
{
    // Return single person found by Id
    Task<Person?> GetPersonById(int id);
    
    // Add person to database and set DateCreated to now
    Task<Person> AddPerson(Person person);
    
    // Update single person details
    Task UpdatePerson(Person person);
    
    // Delete single person by Id
    Task DeletePerson(int id);
    
    // Return list of all people, ordered by first and last name
    Task<IEnumerable<Person>> GetAllPeople();
    
    // Return list of people filtering name and surname by searchQuery
    Task<IEnumerable<Person>> SearchPeopleByNameAndSurname(string? searchQuery);
}