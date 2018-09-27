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

namespace HashCal
{
    /// <summary>
    /// Interaction logic for Line.xaml
    /// </summary>
    public partial class Line : UserControl
    {
        public double TextBlockWidth
        {
            get => TitleBlock.Width;
            set => TitleBlock.Width = value;
        }

        public bool? IsChecked
        {
            get => IsEnabledBox.IsChecked;
            set => IsEnabledBox.IsChecked = value;
        }

        public string Title
        {
            get => TitleBlock.Text;
            set => TitleBlock.Text = value;
        }

        public string TextContent
        {
            get => ContentBox.Text;
            set => ContentBox.Text = value;
        }

        public Line()
        {
            InitializeComponent();
        }
    }
}
