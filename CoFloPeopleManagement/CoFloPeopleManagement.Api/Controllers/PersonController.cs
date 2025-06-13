using CoFloPeopleManagement.Application.DTOs;
using CoFloPeopleManagement.Application.DTOs.Person;
using CoFloPeopleManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoFloPeopleManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<int>> GetPersonById(int id)
    {
        var person = await _personService.GetPersonById(id);
        return person != null ? Ok(person) : NotFound();
    }
    
    [HttpPost]
    public async Task<ActionResult<PersonDtoResponse>> CreatePerson(PersonDtoRequest personDtoRequest)
    {
        var newPerson = await _personService.CreatePerson(personDtoRequest);
        return CreatedAtAction(nameof(GetAllPeople), new { id = newPerson.Id }, newPerson);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePerson(int id, PersonDtoRequest personDtoRequest)
    {
        await _personService.UpdatePersonDetails(id, personDtoRequest);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson(int id)
    {
        await _personService.DeletePersonById(id);
        return NoContent();
    }
    
    [HttpGet("getAllPeople")]
    public async Task<ActionResult<IEnumerable<PersonDtoResponse>>> GetAllPeople()
    {
        var people = await _personService.GetAllPeople();
        return Ok(people);
    }
    
    [HttpGet("age/{id}")]
    public async Task<ActionResult<int>> GetPersonAge(int id)
    {
        var age = await _personService.GetPersonAgeById(id);
        return age.HasValue ? Ok(age.Value) : NotFound();
    }
    
    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<PersonDtoResponse>>> SearchPeople([FromQuery] string? searchQuery)
    {
        var people = await _personService.SearchPeopleByNameAndSurname(searchQuery);
        return Ok(people);
    }
}