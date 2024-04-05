using System.Windows.Controls;
using Lab4.ViewModels;
using Lab4.Views.Common;

namespace Lab4.Views;

public partial class PersonViewView : Page
{
    public PersonViewViewModel ViewModel
    {
        get => (PersonViewViewModel)DataContext;
        set => DataContext = value;
    }
    
    public PersonViewView()
    {
        InitializeComponent();

        var windowContext = MainWindow.WindowContext;
        ViewModel = new PersonViewViewModel(windowContext.PersonService);
        ViewModel.OnEdit += id =>
        {
            MainWindow.NavigationService.Navigate(new PersonUpsertView(), id);
        };
        ViewModel.OnDelete += () =>
        {
            MainWindow.NavigationService.NavigateSkipBackEntry(new PersonListView());
        };
        ViewModel.OnCancel += () =>
        {
            MainWindow.NavigationService.GoBack();
        };
       
        MainWindow.NavigationService.HandleLoad(data => ViewModel.Id = (int)data);
    }
}