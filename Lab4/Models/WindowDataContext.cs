using Lab4.Repositories.Implementation;
using Lab4.Services;

namespace Lab4.Models;

public class WindowDataContext
{
    public PersonService PersonService { get; }
    public JsonPersonRepository JsonPersonRepository { get; }
    public PersonRepoInitService PersonRepoInitService { get; }

    public WindowDataContext(PersonService personService, JsonPersonRepository jsonPersonRepository, PersonRepoInitService personRepoInitService)
    {
        PersonService = personService;
        JsonPersonRepository = jsonPersonRepository;
        PersonRepoInitService = personRepoInitService;
    }
}