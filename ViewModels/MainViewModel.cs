using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using LinearListApp.Models;
using System.Reactive;
using System.Collections.Generic;
using Avalonia.Threading;
using ReactiveUI;

namespace LinearListApp.ViewModels;

public class MainViewModel : ViewModelBase
{
    private LinearList<string> _linearList;

    public ObservableCollection<string> Items { get; }

    private string _currentElement = string.Empty;
    public string CurrentElement
    {
        get => _currentElement;
        set => SetProperty(ref _currentElement, value);
    }

    private string _newItemText = string.Empty;
    public string NewItemText
    {
        get => _newItemText;
        set => SetProperty(ref _newItemText, value);
    }

    public ReactiveCommand<Unit, Unit> MoveNextCommand { get; }
    public ReactiveCommand<Unit, Unit> MoveFirstCommand { get; }
    public ReactiveCommand<Unit, Unit> AddItemCommand { get; }
    public ReactiveCommand<string, Unit> RemoveItemCommand { get; }

    public MainViewModel()
    {
        _linearList = new LinearList<string>();
        Items = new ObservableCollection<string>();

        InitializeList(new[] { "One", "Two", "Three" });

        MoveNextCommand = ReactiveCommand.Create(MoveNext);
        MoveFirstCommand = ReactiveCommand.Create(MoveFirst);

        AddItemCommand = ReactiveCommand.Create(() =>
        {
            if (!string.IsNullOrWhiteSpace(NewItemText))
            {
                AddItem(NewItemText);
                NewItemText = string.Empty;
            }
        });

        RemoveItemCommand = ReactiveCommand.Create<string>(item =>
        {
            RemoveItem(item);
        });
    }

    private void InitializeList(IEnumerable<string> initialItems)
    {
        foreach (var item in initialItems)
        {
            _linearList.Add(item);
            Items.Add(item);
        }

        CurrentElement = _linearList.CurrentElement;
    }

    public void AddItem(string item)
    {
        _linearList.Add(item);
        Items.Add(item);
        UpdateCurrentElement();
    }

    public void RemoveItem(string item)
    {
        if (_linearList.Remove(item))
        {
            Items.Remove(item);
            UpdateCurrentElement();
        }
    }

    public void MoveNext()
    {
        if (_linearList.MoveNext())
        {
            UpdateCurrentElement();
        }
    }

    public void MoveFirst()
    {
        _linearList.MoveFirst();
        UpdateCurrentElement();
    }

    private void UpdateCurrentElement()
    {
        Dispatcher.UIThread.InvokeAsync(() =>
        {
            CurrentElement = _linearList.IsEmpty ? "List Empty" : _linearList.CurrentElement;
        });
    }
}
