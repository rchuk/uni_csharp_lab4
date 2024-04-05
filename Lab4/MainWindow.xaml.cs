using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Lab4.Models;
using Lab4.Repositories.Implementation;
using Lab4.Services;
using Lab4.Views;

namespace Lab4;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private bool _isClosing;
    
    public MainWindow()
    {
        InitializeComponent();
        
        var personRepository = new JsonPersonRepository("test.json");
        var personService = new PersonService(personRepository);
        var personRepoInitService = new PersonRepoInitService();
        DataContext = new WindowDataContext(personService, personRepository, personRepoInitService);
        
        Task.Run(async () =>
        {
            if (!await personRepository.Load())
                await personRepoInitService.InitRepo(personService, 50);
        });
        
        NavigationService.Navigate(new PersonListView());
    }
    
    public static NavigationService NavigationService => ((MainWindow)Application.Current.MainWindow).MainFrame.NavigationService;
    public static WindowDataContext WindowContext => (WindowDataContext)Application.Current.MainWindow.DataContext;
}