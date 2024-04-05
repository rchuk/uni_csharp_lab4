using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Lab4.Views.Common;

public partial class DropdownRangeFilter : UserControl
{
    public DropdownRangeFilter()
    {
        InitializeComponent();
    }
    
    public static readonly DependencyProperty OnItemsSelectedProperty = DependencyProperty.Register(
        nameof(OnItemsSelected),
        typeof(ICommand),
        typeof(DropdownRangeFilter)
    );
    
    public ICommand OnItemsSelected
    {
        get => (ICommand)GetValue(OnItemsSelectedProperty);
        set => SetValue(OnItemsSelectedProperty, value); 
    }

    private void ResetButton_OnClick(object sender, RoutedEventArgs e)
    {
        MinText.Clear();
        MaxText.Clear();
        OnItemsSelected.Execute(Tuple.Create<int?, int?>(null, null));
    }

    private void ApplyButton_OnClick(object sender, RoutedEventArgs e)
    {
        int? min = ParseInt(MinText.Text);
        int? max = ParseInt(MaxText.Text);
        OnItemsSelected.Execute(Tuple.Create(min, max));
    }
    
    private static int? ParseInt(string str)
    {
        if (int.TryParse(str, out var number))
            return number;
        
        return null;
    }
}