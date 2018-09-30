using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HashCal.Command
{
    public interface IGenericCommand<in TParam> :
        ICommand
    {
        bool CanExecute(TParam param);
        void Execute(TParam param);
    }
}
