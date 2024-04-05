using Lab4.Models;

namespace Lab4.Repositories;

public interface IPersonRepository
{
    public Task<int> AddPerson(Person person);
    public Task<Person> GetPerson(int id);
    public Task ReplacePerson(int id, Person person);
    public Task<IEnumerable<KeyValuePair<int, Person>>> GetPersonList(PersonCriteria? criteria = null);
    public Task RemovePerson(int id);
}