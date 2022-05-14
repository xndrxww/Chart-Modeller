using System;
using System.Collections.Generic;
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
    //Класс для выбора типа графика или диаграммы
    public partial class ChooseChartWindow : Window
    {
        public ChooseChartWindow()
        {
            InitializeComponent();
        }
        
        //Обработчик события нажатия на кнопку с линейным графиком
        private void lineSerriesButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrameInstance.Navigate(new CreateChartsPage("Создание линейного графика"));
            this.Close();
        }

        //Обработчик события нажатия на кнопку с тепловым графиком
        private void heatSeriesButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrameInstance.Navigate(new CreateChartsPage("Создание теплового графика"));
            this.Close();
        }
        
        //Обработчик события нажатия на кнопку с круговой диаграммой
        private void pieSiriesButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrameInstance.Navigate(new CreateChartsPage("Создание круговой диаграммы"));
            this.Close();
        }

        //Обработчик события нажатия на кнопку с гистограммой
        private void columnSeriesButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrameInstance.Navigate(new CreateChartsPage("Создание гистограммы"));
            this.Close();
        }

        //Обработчик события нажатия на кнопку с диаграммой-областью
        private void stackSeriesButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrameInstance.Navigate(new CreateChartsPage("Создание диаграммы-области"));
            this.Close();
        }

        //Обработчик события нажатия на кнопку со ступенчатой диаграммой
        private void stepLineSeries_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrameInstance.Navigate(new CreateChartsPage("Создание ступенчатой диаграммы"));
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
