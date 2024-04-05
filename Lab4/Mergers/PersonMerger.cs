using Lab4.Models;

namespace Lab4.Mergers;

public class PersonMerger
{
    public void MergeCreate(Person entity, PersonView view)
    {
        MergeMainFields(entity, view);
    }

    public void MergeEdit(Person entity, PersonView view)
    {
        MergeMainFields(entity, view);
    }

    private void MergeMainFields(Person entity, PersonView view)
    {
        entity.FirstName = view.FirstName;
        entity.LastName = view.LastName;
        entity.Email = view.Email;
        entity.BirthDate = view.BirthDate;
    }
}