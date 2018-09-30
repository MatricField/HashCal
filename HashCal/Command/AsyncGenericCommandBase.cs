using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HashCal.Command
{
    public abstract class AsyncGenericCommandBase<TParam> :
        IGenericCommand<TParam>
    {
        public event EventHandler CanExecuteChanged;

        bool ICommand.CanExecute(object parameter)
        {
            if(!(parameter is TParam))
            {
                return false;
            }
            return CanExecute((TParam)parameter);
        }

        public abstract bool CanExecute(TParam param);

        void ICommand.Execute(object parameter)
        {
            if(!(parameter is TParam))
            {
                throw new ArgumentException("Parameter type mismatch");
            }
            Execute((TParam)parameter);
        }

        public async void Execute(TParam param)
        {
            await ExecuteAsync(param);
        }

        protected abstract Task ExecuteAsync(TParam param);

        protected virtual void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
