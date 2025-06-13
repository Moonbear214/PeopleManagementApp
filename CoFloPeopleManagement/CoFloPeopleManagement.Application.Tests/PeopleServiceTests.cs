using Moq;
using Xunit;

using CoFloPeopleManagement.Application.Interfaces;
using CoFloPeopleManagement.Application.DTOs.Person;
using CoFloPeopleManagement.Application.Services;
using CoFloPeopleManagement.Infrastructure.Entities;
using CoFloPeopleManagement.Infrastructure.Interfaces;

namespace CoFloPeopleManagement.Application.Tests;

public class PeopleServiceTests
{
    private readonly Mock<IPeopleRepository> _mockRepo;
    private readonly IPersonService _service;

    public PeopleServiceTests()
    {
        _mockRepo = new Mock<IPeopleRepository>();
        _service = new PersonService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetPeople_WhenPeopleExist()
    {
        // Setup
        var peopleList = new List<Person>
        {
            new Person { Id = 1, FirstName = "Neldan", LastName = "Janse van Rensburg", DateOfBirth = new DateTime(1990, 1, 1) },
            new Person { Id = 2, FirstName = "Carly", LastName = "Garmany", DateOfBirth = new DateTime(1985, 5, 5) }
        };
        _mockRepo.Setup(repo => repo.GetAllPeople()).ReturnsAsync(peopleList);

        // Test
        var result = await _service.GetAllPeople();

        // Validation
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.IsAssignableFrom<IEnumerable<PersonDtoResponse>>(result);
    }

    [Fact]
    public async Task GetPersonById_WhenPersonExists()
    {
        // Setup
        var person = new Person { Id = 1, FirstName = "Neldan", LastName = "Janse van Rensburg", DateOfBirth = new DateTime(1990, 1, 1) };
        _mockRepo.Setup(repo => repo.GetPersonById(1)).ReturnsAsync(person);

        // Test
        var result = await _service.GetPersonById(1);

        // Validation
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Neldan", result.FirstName);
        Assert.IsType<PersonDtoResponse>(result);
    }
    
    [Fact]
    public async Task CreatePerson_ReturningNewPerson()
    {
        // Setup
        var createDto = new PersonDtoRequest { FirstName = "Neldan", LastName = "Janse van Rensburg", DateOfBirth = new DateTime(2000, 1, 1) };
        var personEntity = new Person { Id = 10, FirstName = "Neldan", LastName = "Janse van Rensburg", DateOfBirth = new DateTime(2000, 1, 1) };
        
        _mockRepo.Setup(repo => repo.AddPerson(It.IsAny<Person>())).ReturnsAsync(personEntity);

        // Test
        var result = await _service.CreatePerson(createDto);

        // Validation
        Assert.NotNull(result);
        Assert.Equal(10, result.Id);
        Assert.Equal("Neldan", result.FirstName);
        
        _mockRepo.Verify(repo => repo.AddPerson(It.IsAny<Person>()), Times.Once);
    }
}