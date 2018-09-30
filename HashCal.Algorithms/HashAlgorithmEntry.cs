using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace HashCal.Algorithms
{
    public abstract class HashAlgorithmEntry:
        NotifyPropertyChanged
    {
        private byte[] _HashValue;

        public virtual byte[] HashValue
        {
            get => _HashValue;
            protected set
            {
                _HashValue = value ?? throw new ArgumentNullException();
                OnPropertyChanged();
            }
        }

        private bool _IsEnabled;
        public virtual bool IsEnabled
        {
            get => _IsEnabled;
            set
            {
                _IsEnabled = value;
                OnPropertyChanged();
            }
        }

        private ICommand _ComputeHash;

        public virtual ICommand ComputeHashCommand
        {
            get => _ComputeHash ?? throw new ArgumentNullException();
            set
            {
                _ComputeHash = value;
                OnPropertyChanged();
            }
        }

        public abstract string AlgorithmName { get; }

        public HashAlgorithmEntry()
        {
            HashValue = Array.Empty<byte>();
            IsEnabled = true;
            ComputeHashCommand = new ComputeHashCommandImpl(this);
        }

        public abstract byte[] ComputeHash(byte[] buffer);

        public abstract byte[] ComputeHash(Stream buffer);

        protected class ComputeHashCommandImpl :
            CommandBase
        {
            protected HashAlgorithmEntry AssociatedEntry;

            public ComputeHashCommandImpl(HashAlgorithmEntry associatedEntry)
            {
                AssociatedEntry = associatedEntry ?? throw new ArgumentNullException();
                associatedEntry.PropertyChanged += (_, e) =>
                {
                    if (e.PropertyName == nameof(HashAlgorithmEntry.IsEnabled))
                    {
                        OnCanExecuteChanged();
                    }
                };
            }

            public override bool CanExecute(object parameter)
            {
                return AssociatedEntry.IsEnabled &&
                    (parameter is byte[] ||
                    parameter is Stream);
            }

            public override void Execute(object parameter)
            {
                switch (parameter)
                {
                    case byte[] array:
                        AssociatedEntry.ComputeHash(array);
                        break;
                    case Stream stream:
                        AssociatedEntry.ComputeHash(stream);
                        break;
                    default:
                        throw new ArgumentException();
                }
            }
        }
    }
}
