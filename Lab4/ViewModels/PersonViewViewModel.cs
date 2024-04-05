using Lab4.Models;
using Lab4.Services;
using Lab4.ViewModels.Common;

namespace Lab4.ViewModels;

public class PersonViewViewModel : BaseServiceViewModel
{
    private int? _id;
    private Person? _person;
    
    private readonly PersonService _personService;

    public PersonViewViewModel() : this(null)
    {}
    
    public PersonViewViewModel(PersonService personService)
    {
        _personService = personService;

        EditCommand = new RelayCommand(
            _ => OnEdit?.Invoke(Id.Value), 
            _ => Id != null
        );
        DeleteCommand = new RelayCommand(
            _ =>
            {
                TryRunBackgroundDisplayError(async () =>
                    {
                        await _personService.RemovePerson(Id.Value);
                    },
                    () => OnDelete?.Invoke()
                );
            },
            _ => Id != null
        );
        CancelCommand = new RelayCommand(
            _ => OnCancel?.Invoke()
        );
    }

    public event Action<int>? OnEdit;
    public event Action? OnDelete;
    public event Action? OnCancel; 
    
    public RelayCommand EditCommand { get; }
    public RelayCommand DeleteCommand { get; }
    public RelayCommand CancelCommand { get; }
    
    public Person? Person
    {
        get => _person;
        set => UpdateProperty(ref _person, value, nameof(Person));
    }

    public int? Id
    {
        get => _id;
        set
        {
            if (UpdateProperty(ref _id, value, nameof(Id)))
            {
                if (_id == null)
                {
                    Person = null;
                    return;
                }
                
                TryRunBackgroundDisplayError(async () =>
                {
                    Person = await _personService.GetPerson(Id.Value);
                });
            }
        }
    }
}