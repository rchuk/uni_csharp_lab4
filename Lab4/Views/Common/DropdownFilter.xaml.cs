using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Lab4.Views.Common;

public partial class DropdownFilter : UserControl
{
    public DropdownFilter()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty SelectionModeProperty = DependencyProperty.Register(
        nameof(SelectionMode),
        typeof(SelectionMode),
        typeof(DropdownFilter)
    );
    public static readonly DependencyProperty SelectionItemsProperty = DependencyProperty.Register(
        nameof(SelectionItems),
        typeof(List<KeyValuePair<object, string>>),
        typeof(DropdownFilter)
    );
    public static readonly DependencyProperty OnItemsSelectedProperty = DependencyProperty.Register(
        nameof(OnItemsSelected),
        typeof(ICommand), 
        typeof(DropdownFilter)
    );

    public SelectionMode SelectionMode
    {
        get => (SelectionMode)GetValue(SelectionModeProperty);
        set => SetValue(SelectionItemsProperty, value);
    }
    public List<KeyValuePair<object, string>> SelectionItems
    {
        get => (List<KeyValuePair<object, string>>)GetValue(SelectionItemsProperty);
        set => SetValue(SelectionItemsProperty, value);
    }
    public ICommand OnItemsSelected
    {
        get => (ICommand)GetValue(OnItemsSelectedProperty);
        set => SetValue(OnItemsSelectedProperty, value); 
    }
    
    private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (SelectionMode == SelectionMode.Single)
        {
            OnItemsSelected.Execute(((KeyValuePair<object, string>?)Selector.SelectedItem)?.Key);
            return;
        }

        OnItemsSelected.Execute(Selector
            .SelectedItems
            .OfType<KeyValuePair<object, string>>()
            .Select(entry => entry.Key)
            .ToList()
        );   
    }
    
    private void ResetButton_OnClick(object sender, RoutedEventArgs e)
    {
        Selector.UnselectAll();
    }
}