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
    //Класс для вывода окна при возникновении ошибок при пользовании приложением
    public partial class ErrorWindow : Window
    {
        public ErrorWindow(string errorText)
        {
            InitializeComponent();

            this.errorText.Text = errorText;
        }

        //Обработчик события нажатия на кнопку "Продолжить"
        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Обработчик события, при нажатии левой кнопкой мыши на Grid, для перетаскивания текущего окна
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        //Обработчик события нажатия на кнопку для того, чтобы закрыть текущее окно
        private void closeAppButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
