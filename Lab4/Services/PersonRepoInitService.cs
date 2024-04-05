using Lab4.Models;

namespace Lab4.Services;

public class PersonRepoInitService
{
    private static readonly Random Rng = new ();

    private static readonly string[] FirstNames =
    [
        "James", "Robert", "John", "Michael", "David", "William", "Richard", "Joseph", "Thomas", "Christopher",
        "Charles", "Daniel", "Matthew", "Anthony", "Mark", "Donald", "Steven", "Andrew", "Paul", "Joshua",
        "Kenneth", "Kevin", "Brian", "George", "Timothy",
        "Mary", "Patricia", "Jennifer", "Linda", "Elizabeth", "Barbara", "Susan", "Jessica", "Sarah	", "Karen ",
        "Lisa", "Nancy", "Betty", "Sandra", "Margaret", "Ashley", "Kimberly", "Emily", "Donna", "Michelle",
        "Carol", "Amanda", "Melissa", "Deborah", "Stephanie"
    ];

    private static readonly string[] LastNames =
    [
        "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez",
        "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Martin",
        "Lee", "Perez", "Thompson", "White", "Harris", "Sanchez", "Clark", "Ramirez", "Lewis", "Robinson",
        "Walker", "Young", "Allen", "King", "Wright", "Scott", "Torres", "Nguyen", "Hill", "Flores",
        "Green", "Adams", "Nelson", "Baker", "Hall", "Rivera", "Campbell", "Mitchell", "Carter", "Roberts"
    ];

    private static readonly string[] EmailDomains = [ "gmail.com", "yahoo.com", "hotmail.com" ];
    
    public async Task InitRepo(PersonService personService, int count)
    {
        var persons = await personService.GetPersonList();
        await Task.WhenAll(
            persons
                .Select(entry => personService.RemovePerson(entry.Key))
                .ToArray()
        );
            
        await Task.WhenAll(
            Enumerable.Range(0, count)
                    .Select(_ => personService.CreatePerson(CreateRandomPersonView()))
                    .ToArray()
        );
    }

    private static PersonView CreateRandomPersonView()
    {
        return new PersonView
        {
            FirstName = GetRandomFirstName(),
            LastName = GetRandomLastName(),
            Email = GetRandomEmail(),
            BirthDate = GetRandomBirthDate()
        };
    }

    private static string GetRandomFirstName()
    {
        return FirstNames[Rng.Next(FirstNames.Length)];
    }
    
    private static string GetRandomLastName()
    {
        return LastNames[Rng.Next(LastNames.Length)];
    }
    
    private static string GetRandomEmail()
    {
        int rand = Rng.Next(5);
        int domainIndex = 0;
        if (rand < 3) domainIndex = 0;
        else if (rand < 4) domainIndex = 1;
        else if (rand < 5) domainIndex = 2;
        
        return GetRandomString(Rng.Next(5, 15)) + "@" + EmailDomains[domainIndex];
    }

    private static DateTime GetRandomBirthDate()
    {
        int yearsMaxDiff = 80;
        int yearsMinDiff = 10;
        int yearsDiff = Rng.Next(yearsMinDiff, yearsMaxDiff);
        int daysDiff = Rng.Next(0, 365);

        return DateTime.Today.AddYears(-yearsDiff).AddDays(daysDiff);
    }
    
    private static string GetRandomString(int length)
    {
        const string alphabet = "abcdefghijklmnopqrstuvwxyz0123456789";
        var chars = Enumerable.Range(0, length)
            .Select(_ => alphabet[Rng.Next(alphabet.Length)])
            .ToArray();
        
        return new string(chars);
    }
}