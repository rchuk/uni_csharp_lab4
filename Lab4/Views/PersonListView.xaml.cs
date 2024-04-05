using System.Windows.Controls;
using Lab4.ViewModels;

namespace Lab4.Views;

public partial class PersonListView : Page
{
    public PersonListViewModel ViewModel
    {
        get => (PersonListViewModel)DataContext;
        set => DataContext = value;
    }
    
    public PersonListView()
    {
        InitializeComponent();
        
        var windowContext = MainWindow.WindowContext;
        ViewModel = new PersonListViewModel(windowContext.PersonService, windowContext.PersonRepoInitService);
        ViewModel.OnView += id =>
        {
            MainWindow.NavigationService.Navigate(new PersonViewView(), id);
        };
        ViewModel.OnEdit += id =>
        {
            MainWindow.NavigationService.Navigate(new PersonUpsertView(), id);
        };
        ViewModel.OnCreate += () =>
        {
            MainWindow.NavigationService.Navigate(new PersonUpsertView(), null);
        };

        Loaded += (_, _) =>
        {
            ViewModel.Update();
        };
    }
}