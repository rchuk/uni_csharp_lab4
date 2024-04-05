using Lab4.Models;

namespace Lab4.Services;

public class PersonCalculationService
{
    public async Task CalculateFields(Person person)
    {
        person.Age = CalculateAge(person.BirthDate);
        person.IsAdult = CalculateIsAdult(person.Age);
        person.IsBirthdayToday = CalculateIsBirthdayToday(person.BirthDate);
        person.ChineseSign = ChineseZodiacExtension.FromDate(person.BirthDate);
        person.SunSign = WesternZodiacExtension.FromDate(person.BirthDate);
    }
    
    private static int CalculateAge(DateTime birthDate)
    {
        var today = DateTime.Today;
        int age = today.Year - birthDate.Year;
        if (birthDate.AddYears(age) > today)
            --age;

        return age;
    }

    private static bool CalculateIsAdult(int age)
    {
        return age >= 18;
    }

    private static bool CalculateIsBirthdayToday(DateTime birthDate)
    {
        var today = DateTime.Today;

        return birthDate.Month == today.Month && birthDate.Day == today.Day;
    }
}