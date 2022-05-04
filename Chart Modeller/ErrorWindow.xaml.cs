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
using System.Windows.Shapes;

namespace Chart_Modeller
{
    public partial class ErrorWindow : Window
    {
        public ErrorWindow(string errorText)
        {
            InitializeComponent();

            this.errorText.Text = errorText;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void closeAppButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
