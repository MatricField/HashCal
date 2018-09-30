using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace HashCal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Calculator = new HashCalculator();
            foreach(var kvp in HashProvider.SupportedAlgorithms)
            {
                Calculator.Add(kvp.Key, new CalculatorEntry(kvp.Value));
            }
            NotBusy = true;
            InitializeComponent();
        }

        public HashCalculator Calculator { get; }

        public bool NotBusy
        {
            get { return (bool)GetValue(NotBusyProperty); }
            set { SetValue(NotBusyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Computing.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NotBusyProperty =
            DependencyProperty.Register("NotBusy", typeof(bool), typeof(MainWindow), new PropertyMetadata(false));

        private async void ComputeClick(object sender, RoutedEventArgs e)
        {
            NotBusy = false;
            ComputeButton.IsEnabled = false;
            CancelButton.IsEnabled = true;
            switch(InputType.Text)
            {
                case "File":
                    var info = new FileInfo(InputBox.Text);
                    await Calculator.Compute(info);
                    break;
                case "String":
                    await Calculator.Compute(InputBox.Text);
                    break;
            }
            ComputeButton.IsEnabled = true;
            CancelButton.IsEnabled = false;
            NotBusy = true;
        }

        private void File_Drop(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                InputType.Text = "File";
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                InputBox.Text = files[0];
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show("The window is closing", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.OK);
            if(result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Calculator.Cancel();
            CancelButton.IsEnabled = false;
        }
    }
}
