using System.Windows.Controls;
using System.Windows.Navigation;
using Lab4.ViewModels;
using Lab4.Views.Common;

namespace Lab4.Views;

public partial class PersonUpsertView : Page
{
    public PersonUpsertViewModel ViewModel
    {
        get => (PersonUpsertViewModel)DataContext;
        set => DataContext = value;
    }
    
    public PersonUpsertView()
    {
        InitializeComponent();
        
        var windowContext = MainWindow.WindowContext;
        ViewModel = new PersonUpsertViewModel(windowContext.PersonService);
        ViewModel.OnSave += id =>
        {
            MainWindow.NavigationService.NavigateSkipBackEntry(new PersonViewView(), id);
        };
        ViewModel.OnCancel += () =>
        {
            MainWindow.NavigationService.GoBack();
        };
        
        MainWindow.NavigationService.HandleLoad(data => ViewModel.Id = (int?)data);
    }
}