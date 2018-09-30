using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HashCal.Core
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

        public abstract string AlgorithmName { get; }

        public HashAlgorithmEntry()
        {
            HashValue = Array.Empty<byte>();
            IsEnabled = true;
        }

        public abstract byte[] ComputeHash(byte[] buffer);

        public Task<byte[]> ComputeHashAsync(byte[] buffer, CancellationToken token)
        {
            return Task.Run(() => ComputeHash(buffer), token);
        }

        public abstract byte[] ComputeHash(Stream buffer);

        public Task<byte[]> ComputeHashAsync(Stream buffer, CancellationToken token)
        {
            return Task.Run(() => ComputeHash(buffer), token);
        }
    }
}
