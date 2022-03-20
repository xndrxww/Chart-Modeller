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
    public partial class ChooseChartWindow : Window
    {
        public ChooseChartWindow()
        {
            InitializeComponent();
        }
        private void lineSerriesButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrameInstance.Navigate(new CreateChartsPage("Создание линейного графика"));
            this.Close();
        }

        private void heatSeriesButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrameInstance.Navigate(new CreateChartsPage("Создание теплового графика"));
            this.Close();
        }
        private void pieSiriesButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrameInstance.Navigate(new CreateChartsPage("Создание круговой диаграммы"));
            this.Close();
        }

        private void columnSeriesButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrameInstance.Navigate(new CreateChartsPage("Создание гистограммы"));
            this.Close();
        }

        private void stackSeriesButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrameInstance.Navigate(new CreateChartsPage("Создание диаграммы-области"));
            this.Close();
        }

        private void solidGaugeSeriesButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrameInstance.Navigate(new CreateChartsPage("Создание манометрической диаграммы"));
            this.Close();
        }
    }
}
