using CoFloPeopleManagement.Infrastructure.Entities;
using CoFloPeopleManagement.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoFloPeopleManagement.Infrastructure.Data;

public class PersonRepository : IPeopleRepository
{
    private readonly AppDbContext _context;

    public PersonRepository(AppDbContext context)
    {
        _context = context;
    }

    // Return single person found by Id
    public async Task<Person?> GetPersonById(int id) => await _context.People.FindAsync(id);
    
    // Add person to database and set DateCreated to now
    public async Task<Person> AddPerson(Person person)
    {
        person.DateCreated = DateTime.UtcNow;
        _context.People.Add(person);
        await _context.SaveChangesAsync();
        return person;
    }
    
    // Update single person details
    public async Task UpdatePerson(Person person)
    {
        _context.Entry(person).State = EntityState.Modified;
        _context.Entry(person).Property(x => x.DateCreated).IsModified = false;
        await _context.SaveChangesAsync();
    }
    
    // Delete single person by Id
    public async Task DeletePerson(int id)
    {
        var person = await _context.People.FindAsync(id);
        if (person != null)
        {
            _context.People.Remove(person);
            await _context.SaveChangesAsync();
        }
    }
    
    // Return list of all people, ordered by first and last name
    public async Task<IEnumerable<Person>> GetAllPeople()
    {
        var query = _context.People.AsQueryable();

        return await query.OrderBy(p => p.FirstName).ThenBy(p => p.LastName).ToListAsync();
    }
    
    // Return list of people filtering name and surname by searchQuery
    public async Task<IEnumerable<Person>> SearchPeopleByNameAndSurname(string? searchQuery)
    {
        var query = _context.People.AsQueryable();

        if (!string.IsNullOrEmpty(searchQuery))
        {
            var lowerCaseSearchQuery = searchQuery.ToLower();
            query = query.Where(p => p.FirstName.ToLower().Contains(lowerCaseSearchQuery) || p.LastName.ToLower().Contains(lowerCaseSearchQuery));
        }

        return await query.OrderBy(p => p.FirstName).ThenBy(p => p.LastName).ToListAsync();
    }
}