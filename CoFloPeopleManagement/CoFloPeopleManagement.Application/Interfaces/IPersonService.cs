using CoFloPeopleManagement.Application.DTOs;
using CoFloPeopleManagement.Application.DTOs.Person;

namespace CoFloPeopleManagement.Application.Interfaces;

public interface IPersonService
{
    // Return single person found by Id
    Task<PersonDtoResponse?> GetPersonById(int id);
    
    // Add person to database
    Task<PersonDtoResponse> CreatePerson(PersonDtoRequest dtoRequest);
    
    // Update single person details
    Task UpdatePersonDetails(int id, PersonDtoRequest dtoRequest);
    
    // Delete single person by Id
    Task DeletePersonById(int id);
    
    // Return list of all people, ordered by first and last name
    Task<IEnumerable<PersonDtoResponse>> GetAllPeople();
    
    // Returns only age of person filtered by Id
    Task<int?> GetPersonAgeById(int id);
    
    // Return list of all people, ordered by first and last name
    Task<IEnumerable<PersonDtoResponse>> SearchPeopleByNameAndSurname(string? searchQuery);
}