using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HashCal
{
    public sealed class CalculatorEntry:
        INotifyPropertyChanged
    //    DependencyObject
    {
        private bool _IsEnabled;
        public bool IsEnabled
        {
            get => _IsEnabled;
            set
            {
                if(value != _IsEnabled)
                {
                    _IsEnabled = value;
                    NotifyPropertyChanged();
                }
            }
        }
        //public bool IsEnabled
        //{
        //    get { return (bool)GetValue(IsEnabledProperty); }
        //    set { SetValue(IsEnabledProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for IsEnabled.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty IsEnabledProperty =
        //    DependencyProperty.Register("IsEnabled", typeof(bool), typeof(CalculatorEntry), new PropertyMetadata(false));

        //public HashAlgorithm Algorithm { get; }

        public Func<HashAlgorithm> InitAlgorithm { get; }

        private string _HashResult;
        public string HashResult
        {
            get => _HashResult;
            set
            {
                if(value != _HashResult)
                {
                    _HashResult = value;
                    NotifyPropertyChanged();
                }
            }
        }


        //public string HashResult
        //{
        //    get { return (string)GetValue(HashResultProperty); }
        //    set { SetValue(HashResultProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for HashResult.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty HashResultProperty =
        //    DependencyProperty.Register("HashResult", typeof(string), typeof(CalculatorEntry), new PropertyMetadata(""));

        public CalculatorEntry(Func<HashAlgorithm> Algorithm)
        {
            this.IsEnabled = false;
            this.InitAlgorithm = Algorithm;
            this.HashResult = "";
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
