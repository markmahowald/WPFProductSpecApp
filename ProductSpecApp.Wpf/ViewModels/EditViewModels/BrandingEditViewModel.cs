using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using ProductSpecificationApp.Data.Repositories;
using System.Windows.Input;
using ProductSpecificationApp.Data.BusinessObjects;
using ProductSpecificationApp.Data.InterfacesAndInheritables;
using CommunityToolkit.Mvvm.Messaging;
using ProductSpecificationApp.Data.Messages;
using System.Collections.Generic;
using System.Linq;
using ProductSpecApp.Wpf.MiscAndUtility;

namespace ProductSpecApp.Wpf.ViewModels
{
    public class BrandingEditViewModel : ObservableObject
    {
        private readonly IProductSpecAppRepo _repository;
        private readonly Stack<IUndoableCommand> _undoStack = new Stack<IUndoableCommand>();
        private readonly Stack<IUndoableCommand> _redoStack = new Stack<IUndoableCommand>();

        public BrandingEditViewModel(IProductSpecAppRepo repository, Branding branding)
        {
            _repository = repository;
            FocusedBranding = branding;
        }

        public Branding FocusedBranding { get; }

        private string _lastFocusedProperty;
        private string _originalValue;

        public ICommand SaveCommand => new RelayCommand(Save);
        public ICommand UndoCommand => new RelayCommand(Undo);
        public ICommand RedoCommand => new RelayCommand(Redo);
        public ICommand FocusChangedCommand => new RelayCommand<string>(OnFocusChanged);

        private void OnFocusChanged(string propertyName)
        {
            if (!string.IsNullOrEmpty(_lastFocusedProperty))
            {
                var currentValue = GetPropertyValue(FocusedBranding, _lastFocusedProperty)?.ToString();
                if (_originalValue != currentValue)
                {
                    var undoCommand = new UndoRedoCommand(
                        execute: () => SetPropertyValue(FocusedBranding, _lastFocusedProperty, currentValue),
                        undo: () => SetPropertyValue(FocusedBranding, _lastFocusedProperty, _originalValue),
                        redo: () => SetPropertyValue(FocusedBranding, _lastFocusedProperty, currentValue)
                    );

                    _undoStack.Push(undoCommand);
                    _redoStack.Clear();
                }
            }

            _lastFocusedProperty = propertyName;
            _originalValue = GetPropertyValue(FocusedBranding, propertyName)?.ToString();
        }

        private object GetPropertyValue(object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName)?.GetValue(obj, null);
        }

        private void SetPropertyValue(object obj, string propertyName, object value)
        {
            var prop = obj.GetType().GetProperty(propertyName);
            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(obj, Convert.ChangeType(value, prop.PropertyType), null);
                //OnPropertyChanged(propertyName);
            }
        }

        private void Save()
        {
            bool success = false;
            var originalBranding = FocusedBranding.Clone(); // Assuming Branding implements ICloneable

            try
            {
                var saveCommand = new UndoRedoCommand(
                    execute: () =>
                    {
                        if (FocusedBranding.BrandingId == 0)
                        {
                            FocusedBranding.UpdateDBObject(FocusedBranding.tblBranding);
                            success = _repository.AddBranding(FocusedBranding.tblBranding);
                        }
                        else
                        {
                            FocusedBranding.UpdateDBObject(FocusedBranding.tblBranding);
                            success = _repository.SaveChanges();
                        }
                    },
                    undo: () =>
                    {
                        FocusedBranding.CopyFrom(originalBranding as Branding);
                        _repository.SaveChanges();
                    },
                    redo: () =>
                    {
                        FocusedBranding.UpdateDBObject(FocusedBranding.tblBranding);
                        _repository.SaveChanges();
                    });

                _undoStack.Push(saveCommand);
                _redoStack.Clear();

                saveCommand.Execute(null);
            }
            catch (Exception)
            {
                success = false;
            }

            WeakReferenceMessenger.Default.Send(new SaveSuccessfulMessage(typeof(Branding), FocusedBranding.BrandingId, success));
        }

        private void Undo()
        {
            if (_undoStack.Any())
            {
                var command = _undoStack.Pop();
                command.Undo();
                _redoStack.Push(command);
            }
        }

        private void Redo()
        {
            if (_redoStack.Any())
            {
                var command = _redoStack.Pop();
                command.Redo();
                _undoStack.Push(command);
            }
        }
    }
}
