using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Lab4.Views.Common;

public partial class DropdownDateRangeFilter : UserControl
{
    public DropdownDateRangeFilter()
    {
        InitializeComponent();
    }
    
    public static readonly DependencyProperty OnItemsSelectedProperty = DependencyProperty.Register(
        nameof(OnItemsSelected),
        typeof(ICommand),
        typeof(DropdownDateRangeFilter)
    );
    
    public ICommand OnItemsSelected
    {
        get => (ICommand)GetValue(OnItemsSelectedProperty);
        set => SetValue(OnItemsSelectedProperty, value); 
    }

    private void ResetButton_OnClick(object sender, RoutedEventArgs e)
    {
        MinDate.SelectedDate = null;
        MaxDate.SelectedDate = null;
        OnItemsSelected.Execute(Tuple.Create<DateTime?, DateTime?>(null, null));
    }
    
    private void OnSelectedDateChanged(object? sender, SelectionChangedEventArgs e)
    {
        OnItemsSelected.Execute(Tuple.Create(MinDate.SelectedDate, MaxDate.SelectedDate));
    }
}