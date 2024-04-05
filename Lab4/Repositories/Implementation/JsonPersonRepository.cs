using System.IO;
using System.Text.Json;
using Lab4.Models;

namespace Lab4.Repositories.Implementation;

public class JsonPersonRepository : IPersonRepository
{
    private readonly string _filename;
    private Dictionary<int, Person> _persons = new ();
    private int _counter;

    public JsonPersonRepository(string filename)
    {
        _filename = filename;
    }
    
    public async Task<int> AddPerson(Person person)
    {
        var id = _counter++;
        _persons.Add(id, new Person(person));
        
        return id;
    }

    public async Task<Person> GetPerson(int id)
    {
        return new Person(_persons[id]);
    }

    public async Task ReplacePerson(int id, Person person)
    {
        _persons[id] = new Person(person);
    }

    public async Task<IEnumerable<KeyValuePair<int, Person>>> GetPersonList(PersonCriteria? criteria = null)
    {
        if (criteria == null)
        {
            return from entry in _persons
                   select KeyValuePair.Create(entry.Key, new Person(entry.Value));
        }
        
        var sortProperty = criteria.SortPropertyName != null ? typeof(Person).GetProperty(criteria.SortPropertyName) : null;
        
        var query = from entry in _persons
            where criteria.FirstName == null || entry.Value.FirstName.Contains(criteria.FirstName)
            where criteria.LastName == null || entry.Value.LastName.Contains(criteria.LastName)
            where criteria.Email == null || entry.Value.Email.Contains(criteria.Email)
            where criteria.MinBirthDate == null || entry.Value.BirthDate >= criteria.MinBirthDate
            where criteria.MaxBirthDate == null || entry.Value.BirthDate <= criteria.MaxBirthDate
            where criteria.MinAge == null || entry.Value.Age >= criteria.MinAge
            where criteria.MaxAge == null || entry.Value.Age <= criteria.MaxAge
            where criteria.IsAdult?.Contains(entry.Value.IsAdult) ?? true
            where criteria.ChineseSigns?.Contains(entry.Value.ChineseSign) ?? true
            where criteria.SunSigns?.Contains(entry.Value.SunSign) ?? true
            select entry;

        query = OrderBy(
            query, 
            entry => sortProperty?.GetValue(entry.Value) ?? entry.Key,
            criteria.IsSortAscending
        );

        return from entry in query
               select KeyValuePair.Create(entry.Key, new Person(entry.Value));
    }

    public async Task RemovePerson(int id)
    {
        _persons.Remove(id);
    }
    
    private static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, bool isAscending)
    {
        return isAscending ? source.OrderBy(keySelector) : source.OrderByDescending(keySelector);
    }

    public async Task Save()
    {
        var data = new RepositoryData(_counter, _persons);
        
        await using var stream = File.Create(_filename);
        await JsonSerializer.SerializeAsync(
            stream,
            data,
            new JsonSerializerOptions { WriteIndented = true }
        );
    }

    public async Task<bool> Load()
    {
        try
        {
            await using var stream = File.OpenRead(_filename);
            var data = await JsonSerializer.DeserializeAsync<RepositoryData>(stream);
            if (data == null)
                return false;

            _counter = data.Counter;
            _persons = data.Persons;

            return true;
        }
        catch
        {
            return false;
        }
    }
}

[Serializable]
internal record RepositoryData(int Counter, Dictionary<int, Person> Persons);
