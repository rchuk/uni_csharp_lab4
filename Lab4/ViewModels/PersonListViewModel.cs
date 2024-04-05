using System.Collections.ObjectModel;
using System.Windows.Data;
using Lab4.Models;
using Lab4.Services;
using Lab4.ViewModels.Common;

namespace Lab4.ViewModels;

public class PersonListViewModel : BaseServiceViewModel
{
    private PersonCriteria _criteria = new ();
    private readonly object _lock = new ();
    
    private readonly PersonService _personService;
    private readonly PersonRepoInitService _repoInitService;
    
    public PersonListViewModel() : this(null, null)
    {}
    
    public PersonListViewModel(PersonService personService, PersonRepoInitService repoInitService)
    {
        _personService = personService;
        _repoInitService = repoInitService;

        BindingOperations.EnableCollectionSynchronization(Persons, _lock);

        TestClearCommand = new RelayCommand(_ =>
        {
            TryRunBackgroundDisplayError(async () =>
                {
                    await _repoInitService.InitRepo(_personService, 50); 
                },
                Update
            );
        });
        ViewCommand = new RelayCommand(key =>
        {
            OnView?.Invoke((int)key);
        });
        EditCommand = new RelayCommand(key =>
        {
            OnEdit?.Invoke((int)key);
        });
        DeleteCommand = new RelayCommand(key =>
        {
            TryRunBackgroundDisplayError(async () =>
                {
                    await _personService.RemovePerson((int)key);
                },
                Update
            );
        });
        CreateCommand = new RelayCommand(_ =>
        {
            OnCreate?.Invoke();
        });
        
        SortDirectionSelectCommand = new RelayCommand(param =>
        {
            _criteria.IsSortAscending = (bool?)param ?? true;
            Update();
        });
        SortFieldSelectCommand = new RelayCommand(param =>
        {
            _criteria.SortPropertyName = (string?)param;
            Update();
        });
        
        FirstNameSelectCommand = new RelayCommand(param =>
        {
            _criteria.FirstName = (string?)param;
            Update();
        });
        LastNameSelectCommand = new RelayCommand(param =>
        {
            _criteria.LastName = (string?)param;
            Update();
        });
        EmailSelectCommand = new RelayCommand(param =>
        {
            _criteria.Email = (string?)param;
            Update();
        });
        BirthDateSelectCommand = new RelayCommand(param =>
        {
            var (min, max) = (Tuple<DateTime?, DateTime?>)param;
            _criteria.MinBirthDate = min;
            _criteria.MaxBirthDate = max;
            Update();
        });
        AgeSelectCommand = new RelayCommand(param =>
        {
            var (min, max) = (Tuple<int?, int?>)param;
            _criteria.MinAge = min;
            _criteria.MaxAge = max;
            Update();
        });
        IsAdultSelectCommand = new RelayCommand(param =>
        {
            _criteria.IsAdult = new HashSet<bool>(((List<object>)param).OfType<bool>());
            if (_criteria.IsAdult.Count == 0)
                _criteria.IsAdult = null;
            Update();
        });
        ChineseSignSelectCommand = new RelayCommand(param =>
        {
            _criteria.ChineseSigns = new HashSet<ChineseZodiac>(((List<object>)param).OfType<ChineseZodiac>());
            if (_criteria.ChineseSigns.Count == 0)
                _criteria.ChineseSigns = null;
            Update();
        });
        SunSignSelectCommand = new RelayCommand(param =>
        {
            _criteria.SunSigns = new HashSet<WesternZodiac>(((List<object>)param).OfType<WesternZodiac>());
            if (_criteria.SunSigns.Count == 0)
                _criteria.SunSigns = null;
            Update();
        });
    }

    public Action<int>? OnView;
    public Action<int>? OnEdit;
    public Action? OnCreate;
    
    public RelayCommand TestClearCommand { get; }
    public RelayCommand ViewCommand { get; }
    public RelayCommand EditCommand { get; }
    public RelayCommand DeleteCommand { get; }
    public RelayCommand CreateCommand { get; }
    
    public RelayCommand SortDirectionSelectCommand { get; }
    public RelayCommand SortFieldSelectCommand { get; }
    
    public RelayCommand FirstNameSelectCommand { get; }
    public RelayCommand LastNameSelectCommand { get; }
    public RelayCommand EmailSelectCommand { get; }
    public RelayCommand BirthDateSelectCommand { get; }
    public RelayCommand AgeSelectCommand { get; }
    public RelayCommand IsAdultSelectCommand { get; }
    public RelayCommand ChineseSignSelectCommand { get; }
    public RelayCommand SunSignSelectCommand { get; }
    
    public ObservableCollection<KeyValuePair<int, Person>> Persons { get; } = [];
    
    public List<KeyValuePair<object, string>> SortFieldSelectionItems { get; } =
    [
        KeyValuePair.Create((object)nameof(Person.FirstName), "First Name"),
        KeyValuePair.Create((object)nameof(Person.LastName), "Last Name"),
        KeyValuePair.Create((object)nameof(Person.Email), "Email"),
        KeyValuePair.Create((object)nameof(Person.BirthDate), "Birth Date"),
        KeyValuePair.Create((object)nameof(Person.Age), "Age"),
        KeyValuePair.Create((object)nameof(Person.ChineseSign), "Chinese Sign"),
        KeyValuePair.Create((object)nameof(Person.SunSign), "Sun Sign")
    ];
    
    public List<KeyValuePair<object, string>> SortDirectionSelectionItems { get; } =
    [
        KeyValuePair.Create((object)true, "Ascending"),
        KeyValuePair.Create((object)false, "Descending")
    ];
    
    public List<KeyValuePair<object, string>> IsAdultSelectionItems { get; } =
    [
        KeyValuePair.Create((object)true, "Yes"),
        KeyValuePair.Create((object)false, "No")
    ];

    public List<KeyValuePair<object, string>> SunSignSelectionItems { get; } = Enum
        .GetValues<WesternZodiac>()
        .Select(sign => KeyValuePair.Create((object)sign, sign.ToString()))
        .ToList();

    public List<KeyValuePair<object, string>> ChineseSignSelectionItems { get; } = Enum
        .GetValues<ChineseZodiac>()
        .Select(sign => KeyValuePair.Create((object)sign, sign.ToString()))
        .ToList();
    
    public void Update()
    {
        TryRunBackgroundDisplayError(async () =>
        {
            var persons = await _personService.GetPersonList(_criteria);
            Persons.Clear();
            foreach (var person in persons)
                Persons.Add(person);
        });
    }
}