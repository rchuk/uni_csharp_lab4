using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Lab4.Views.Common;

public partial class DropdownSearchFilter : UserControl
{
    public DropdownSearchFilter()
    {
        InitializeComponent();
    }
    
    public static readonly DependencyProperty OnItemsSelectedProperty = DependencyProperty.Register(
        nameof(OnItemsSelected),
        typeof(ICommand),
        typeof(DropdownSearchFilter)
    );
    
    public ICommand OnItemsSelected
    {
        get => (ICommand)GetValue(OnItemsSelectedProperty);
        set => SetValue(OnItemsSelectedProperty, value); 
    }

    private void ResetButton_OnClick(object sender, RoutedEventArgs e)
    {
        SearchBar.Clear();
        RaiseChange();
    }
    
    private void OkButton_OnClick(object sender, RoutedEventArgs e)
    {
        RaiseChange();
    }

    private void RaiseChange()
    {
        var text = SearchBar.Text;
        OnItemsSelected.Execute(text.Length != 0 ? text : null);
    }
}