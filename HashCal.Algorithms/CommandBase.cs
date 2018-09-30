using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace HashCal.Algorithms
{
    public abstract class CommandBase :
        ICommand
    {
        public event EventHandler CanExecuteChanged;

        public abstract bool CanExecute(object parameter);
        public abstract void Execute(object parameter);

        protected virtual void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
