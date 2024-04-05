namespace Lab4.Models;

[Serializable]
public class Person
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public DateTime BirthDate { get; set; }
    
    public int Age { get; set; }
    public bool IsAdult { get; set; }
    public bool IsBirthdayToday { get; set; }
    public ChineseZodiac ChineseSign { get; set; }
    public WesternZodiac SunSign { get; set; }
    
    public Person() {}

    public Person(Person other)
    {
        FirstName = other.FirstName;
        LastName = other.LastName;
        Email = other.Email;
        BirthDate = other.BirthDate;

        Age = other.Age;
        IsAdult = other.IsAdult;
        IsBirthdayToday = other.IsBirthdayToday;
        ChineseSign = other.ChineseSign;
        SunSign = other.SunSign;
    }
}