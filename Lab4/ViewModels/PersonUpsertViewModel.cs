using Lab4.Models;
using Lab4.Services;
using Lab4.ViewModels.Common;

namespace Lab4.ViewModels;

public class PersonUpsertViewModel : BaseServiceViewModel
{
    private int? _id;
    private PersonView _personView = new ();
    private bool _isInputActive;
    private string _header = "Create Person";

    private readonly PersonService _personService;
    
    public PersonUpsertViewModel() : this(null)
    {}

    public PersonUpsertViewModel(PersonService personService)
    {
        _personService = personService;
        
        SaveCommand = new RelayCommand(_ => 
            {
                TryRunBackgroundDisplayError(async () =>
                    {
                        if (Id != null)
                            await _personService.EditPerson(Id.Value, _personView);
                        else
                            Id = await _personService.CreatePerson(_personView);
                    },
                    () => OnSave?.Invoke(Id.Value)
                );
            }, 
            _ => !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName)
                && !string.IsNullOrWhiteSpace(Email)
        );
        CancelCommand = new RelayCommand(_ => OnCancel?.Invoke());
    }

    public event Action<int>? OnSave;
    public event Action? OnCancel;
    
    public RelayCommand SaveCommand { get; }
    public RelayCommand CancelCommand { get; }

    public int? Id
    {
        get => _id;
        set
        {
            UpdateProperty(ref _id, value, nameof(Id));
            
            if (Id == null) 
            { 
                IsInputActive = true;
                Header = "Create Person";
                
                return;
            }

            TryRunBackgroundDisplayError(async () => 
            { 
                var person = await _personService.GetPerson(Id.Value); 
                FirstName = person.FirstName; 
                LastName = person.LastName; 
                Email = person.Email; 
                BirthDate = person.BirthDate;
                
                IsInputActive = true;
                Header = "Edit person";
            });
        }   
    }
    
    public bool IsInputActive
    {
        get => _isInputActive;
        private set => UpdateProperty(ref _isInputActive, value, nameof(IsInputActive));
    }

    public string Header
    {
        get => _header;
        private set => UpdateProperty(ref _header, value, nameof(Header));
    }
    
    public string? FirstName
    {
        get => _personView.FirstName;
        set
        {
            _personView.FirstName = value;
            OnPropertyChanged(nameof(FirstName));
        }
    }

    public string? LastName
    {
        get => _personView.LastName;
        set
        {
            _personView.LastName = value;
            OnPropertyChanged(nameof(LastName));
        }
    }
    
    public string? Email
    {
        get => _personView.Email;
        set
        {
            _personView.Email = value;
            OnPropertyChanged(nameof(Email));
        }
    }
    
    public DateTime BirthDate
    {
        get => _personView.BirthDate;
        set
        {
            _personView.BirthDate = value;
            OnPropertyChanged(nameof(BirthDate));
        }
    }
}