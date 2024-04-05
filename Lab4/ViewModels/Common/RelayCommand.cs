﻿using System.Windows.Input;

namespace Lab4.ViewModels.Common;

public class RelayCommand : ICommand
{
    private readonly Action<object?> _execute;
    private readonly Predicate<object?>? _canExecute;

    public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
        CommandManager.RequerySuggested += OnCommandManagerRequerySuggested;
    }

    public event EventHandler? CanExecuteChanged;
    
    public bool CanExecute(object? parameter)
    {
        return _canExecute == null || _canExecute(parameter);
    }

    public void Execute(object? parameter)
    {
        _execute(parameter);
    }

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    private void OnCommandManagerRequerySuggested(object? sender, EventArgs e)
    {
        RaiseCanExecuteChanged();
    }
}