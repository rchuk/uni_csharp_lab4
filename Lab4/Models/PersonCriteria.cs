namespace Lab4.Models;

public class PersonCriteria
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    
    public DateTime? MinBirthDate { get; set; }
    public DateTime? MaxBirthDate { get; set; }
    public int? MinAge { get; set; }
    public int? MaxAge { get; set; }
    
    public HashSet<bool>? IsAdult { get; set; }
    public HashSet<ChineseZodiac>? ChineseSigns { get; set; }
    public HashSet<WesternZodiac>? SunSigns { get; set; }
    
    public string? SortPropertyName { get; set; }
    public bool IsSortAscending { get; set; } = true;
}