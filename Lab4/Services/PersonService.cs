using Lab4.Mergers;
using Lab4.Models;
using Lab4.Repositories;
using Lab4.Validators;

namespace Lab4.Services;

public class PersonService
{
    private readonly IPersonRepository _repository;
    private readonly PersonMerger _merger;
    private readonly PersonValidator _validator;

    private readonly PersonCalculationService _calculationService;

    public PersonService(IPersonRepository repository)
    {
        _repository = repository;
        _merger = new PersonMerger();
        _validator = new PersonValidator();
        _calculationService = new PersonCalculationService();
    }
    
    public async Task<int> CreatePerson(PersonView view)
    {
        var person = new Person();
        _merger.MergeCreate(person, view);
        await _calculationService.CalculateFields(person);
        _validator.Validate(person);

        return await _repository.AddPerson(person);
    }

    public async Task EditPerson(int id, PersonView view)
    {
        var person = await _repository.GetPerson(id);
        _merger.MergeEdit(person, view);
        await _calculationService.CalculateFields(person);
        _validator.Validate(person);

        await _repository.ReplacePerson(id, person);
    }
    
    public async Task<Person> GetPerson(int id)
    {
        return await _repository.GetPerson(id);
    }
    
    public async Task<IEnumerable<KeyValuePair<int, Person>>> GetPersonList(PersonCriteria? criteria = null)
    {
        return await _repository.GetPersonList(criteria);
    }

    public async Task RemovePerson(int id)
    {
        await _repository.RemovePerson(id);
    }
}