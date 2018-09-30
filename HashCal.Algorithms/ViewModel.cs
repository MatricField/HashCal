using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Input;

namespace HashCal.Algorithms
{
    public class ViewModel:
        NotifyPropertyChanged
    {
        public ObservableCollection<HashAlgorithmEntry> Algorithms { get; }

        private object _HashTarget;

        public object HashTarget
        {
            get => _HashTarget;
            set
            {
                _HashTarget = value;
                OnPropertyChanged();
            }
        }

        private InputMode _InputMode;

        public InputMode InputMode
        {
            get => _InputMode;
            set
            {
                _InputMode = value;
                OnPropertyChanged();
            }
        }

        private Encoding _StringEncoding;

        public Encoding StringEncoding
        {
            get => _StringEncoding;
            set
            {
                _StringEncoding = value ?? throw new ArgumentNullException();
                OnPropertyChanged();
                OnPropertyChanged(nameof(HashTarget));
            }
        }

        private ICommand _ComputeAllCommand;

        public ICommand ComputeAllCommand
        {
            get => _ComputeAllCommand;
            set
            {
                _ComputeAllCommand = value ?? throw new ArgumentNullException();
                OnPropertyChanged();
            }
        }

        public ViewModel()
        {
            Algorithms = new ObservableCollection<HashAlgorithmEntry>();
            HashTarget = "";
            StringEncoding = Encoding.Default;
            ComputeAllCommand = new ComputeAllCommandImpl(this);
        }

        protected void AddPredefined(HashAlgorithmName name)
        {
            Algorithms.Add(new PredefinedHashAlgorithmEntry(name));
        }

        protected void AddCustom(string name, Func<HashAlgorithm> factory)
        {
            Algorithms.Add(new CustomHashAlgorithmEntry(name, factory));
        }

        protected class ComputeAllCommandImpl :
            CommandBase
        {
            private ViewModel AssociatedViewModel;

            public ComputeAllCommandImpl(ViewModel viewModel)
            {
                AssociatedViewModel = viewModel ?? throw new ArgumentNullException();
                AssociatedViewModel.PropertyChanged += AssociatedViewModel_PropertyChanged;
            }

            private void AssociatedViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                if(e.PropertyName == nameof(HashTarget))
                {
                    OnCanExecuteChanged();
                }
            }

            public override bool CanExecute(object parameter)
            {
                switch(AssociatedViewModel.InputMode)
                {
                    case InputMode.File:
                        var path = parameter as string;
                        if(path != null)
                        {
                            return File.Exists(path);
                        }
                        else
                        {
                            return false;
                        }
                    case InputMode.Text:
                        return parameter is byte[] ||
                            parameter is string;
                    default:
                        return false;
                }
            }


            public override void Execute(object parameter)
            {
                Stream stream = null;
                if(!(parameter is byte[]))
                {
                    switch (AssociatedViewModel.InputMode)
                    {
                        case InputMode.File:
                            parameter = stream = File.Open((string)parameter, FileMode.Open, FileAccess.Read, FileShare.Read);
                            break;
                        case InputMode.Text:
                            parameter = AssociatedViewModel.StringEncoding.GetBytes((string)parameter);
                            break;
                        default:
                            throw new InvalidOperationException();
                    }
                }

                using (stream)
                {
                    foreach (var hash in AssociatedViewModel.Algorithms)
                    {
                        if (hash.ComputeHashCommand.CanExecute(parameter))
                        {
                            hash.ComputeHashCommand.Execute(parameter);
                        }
                    }
                }
            }
        }

        //protected class ViewModelComputeHashCommand :
        //    ComputeHashCommandBase
        //{
        //    private ViewModel AssociatedViewModel;

        //    public ViewModelComputeHashCommand(ViewModel viewModel, HashAlgorithmEntry associatedEntry) :
        //        base(associatedEntry)
        //    {
        //        AssociatedViewModel = viewModel ?? throw new ArgumentNullException();
        //        viewModel.PropertyChanged += (_, e) =>
        //        {
        //            if (e.PropertyName == nameof(ViewModel.CanCompute))
        //            {
        //                OnCanExecuteChanged();
        //            }
        //        };
        //    }

        //    public override bool CanExecute(object parameter)
        //    {
        //        return AssociatedViewModel.CanCompute && base.CanExecute(parameter);
        //    }

        //    public override void Execute(object parameter)
        //    {
        //        ComputeHash(AssociatedViewModel.HashTarget);
        //    }
        //}
    }
}
