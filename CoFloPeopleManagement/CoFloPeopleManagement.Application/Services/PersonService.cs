using CoFloPeopleManagement.Application.DTOs.Person;
using CoFloPeopleManagement.Application.Interfaces;
using CoFloPeopleManagement.Infrastructure.Entities;
using CoFloPeopleManagement.Infrastructure.Interfaces;

namespace CoFloPeopleManagement.Application.Services;

public class PersonService : IPersonService
{
    private readonly IPeopleRepository _repository;

    public PersonService(IPeopleRepository repository)
    {
        _repository = repository;
    }
    
    // Return single person found by Id
    public async Task<PersonDtoResponse?> GetPersonById(int id)
    {
        var person = await _repository.GetPersonById(id);
        return person == null ? null : ToPersonDto(person);
    }
    
    // Add person to database
    public async Task<PersonDtoResponse> CreatePerson(PersonDtoRequest dtoRequest)
    {
        var person = ToPersonEntity(dtoRequest);
        var newPerson = await _repository.AddPerson(person);
        return ToPersonDto(newPerson);
    }
    
    // Update single person details
    public async Task UpdatePersonDetails(int id, PersonDtoRequest dtoRequest)
    {
        var personToUpdate = ToPersonEntity(dtoRequest);
        personToUpdate.Id = id;
        
        await _repository.UpdatePerson(personToUpdate);
    }
    
    // Delete single person by Id
    public async Task DeletePersonById(int id)
    {
        await _repository.DeletePerson(id);
    }
    
    // Return list of all people, ordered by first and last name
    public async Task<IEnumerable<PersonDtoResponse>> GetAllPeople()
    {
        var people = await _repository.GetAllPeople();
        return people.Select(ToPersonDto);
    }

    // Returns only age of person filtered by Id
    public async Task<int?> GetPersonAgeById(int id)
    {
        var person = await _repository.GetPersonById(id);
        return person?.Age;
    }

    // Return list of all people, ordered by first and last name
    public async Task<IEnumerable<PersonDtoResponse>> SearchPeopleByNameAndSurname(string? searchQuery)
    {
        var people = await _repository.SearchPeopleByNameAndSurname(searchQuery);
        return people.Select(ToPersonDto);
    }
    
    // Helper functions
    // ========================================================================================================================
    
    // Convert Person Entity to PersonDTOResponse
    private static PersonDtoResponse ToPersonDto(Person person)
    {
        return new PersonDtoResponse
        {
            Id = person.Id,
            FirstName = person.FirstName,
            LastName = person.LastName,
            Age = person.Age,
            DateOfBirth = person.DateOfBirth,
            DateCreated = person.DateCreated
        };
    }
    
    // Convert PersonDTORequest to Person Entity
    private static Person ToPersonEntity(PersonDtoRequest personDto)
    {
        return new Person
        {
            FirstName = personDto.FirstName,
            LastName = personDto.LastName,
            DateOfBirth = personDto.DateOfBirth
        };
    }
}