using System.Text.RegularExpressions;
using Lab4.Exceptions;
using Lab4.Models;

namespace Lab4.Validators;

public class PersonValidator
{
    private static readonly Regex EmailRegex = new (
        @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\"
        + @"$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?"
        + @"(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\u"
        + @"FDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*"
        + @"(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A"
        + @"0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d"
        + @"|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]"
        + @"|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*(["
        + @"a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$", 
        RegexOptions.Compiled
    );
    
    public void Validate(Person person)
    {
        ValidateFirstName(person);
        ValidateLastName(person);
        ValidateEmail(person);
        ValidateAge(person);
    }

    private static void ValidateFirstName(Person person)
    {
        if (string.IsNullOrEmpty(person.FirstName))
            throw new EmptyNameException("User first name can't be empty");
    }

    private static void ValidateLastName(Person person)
    {
        if (string.IsNullOrEmpty(person.LastName))
            throw new EmptyNameException("User last name can't be empty");
    }

    private static void ValidateEmail(Person person)
    {
        if (string.IsNullOrEmpty(person.Email) || !EmailRegex.IsMatch(person.Email))
            throw new InvalidEmailException("User email is invalid");
    }
    
    private static void ValidateAge(Person person)
    {
        if (person.Age < 0)
            throw new NotBornException("User was not born yet");
        if (person.Age >= 135)
            throw new TooOldException("User can't be older than 135 years");
    }
}