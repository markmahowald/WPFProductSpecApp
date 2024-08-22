using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProductSpecApp.Wpf.MiscAndUtility
{
  
        public interface IUndoableCommand : ICommand
        {
            public void Undo();
            public void Redo();
   
        }

        public class UndoRedoCommand : IUndoableCommand
        {
            private readonly Action _execute;
            private readonly Action _undo;
            private readonly Action _redo;

            public UndoRedoCommand(Action execute, Action undo, Action redo)
            {
                _execute = execute;
                _undo = undo;
                _redo = redo;
            }

            public bool CanExecute(object parameter) => true;

            public void Execute(object parameter)
            {
                _execute();
            }

            public void Undo()
            {
                _undo();
            }

            public void Redo()
            {
                _redo();
            }

            public event EventHandler CanExecuteChanged;
        }

 }

