using System.Windows;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;

namespace Chart_Modeller
{
    public partial class ChartsPage : Page
    {
        private string PanelName;
        public ChartsPage()
        {
            InitializeComponent();

            PanelName = MainWindow.Panel.Name;

            //SeriesCollection seriesCollection = new SeriesCollection
            //{
            //    new LineSeries
            //    {
            //        Values = new ChartValues<double> { 3, 5, 7, 4 }
            //    },
            //    new LineSeries
            //    {
            //        Values = new ChartValues<double> { 13, 25, 17, 24 }
            //    },
            //};

            //chart.Series = seriesCollection;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.PageName.Text = PanelName;
            dbText.Text = MainWindow.Database.Name;
        }

        private void deletePanel_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow.PanelsList.RemoveAt(PanelId-1);
            //MainWindow.MainFrameInstance.Navigate(new PanelsPage());
        }

        private void addChart_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow();
        }

        private void OpenWindow()
        {
            bool isWindowOpen = false;

            foreach (Window w in Application.Current.Windows)
            {
                if (w is ChooseChartWindow)
                {
                    isWindowOpen = true;
                    w.Activate();
                }
            }

            if (!isWindowOpen)
            {
                ChooseChartWindow newwindow = new ChooseChartWindow();
                newwindow.Show();
            }
        }

        private void GetCharts()
        {
            sp.Children.Clear();

            foreach (var server in MainWindow.ServersList)
            {
                foreach (var database in server.Databases)
                {
                    if (database.Name == MainWindow.Database.Name)
                    {
                        foreach (var panel in database.Panels)
                        {
                            if (panel.Name == MainWindow.Panel.Name)
                            {
                                foreach (var chart in panel.Charts)
                                {
                                    CartesianChart cartesianChart = new CartesianChart();
                                    cartesianChart.Series = chart.SeriesCollection;

                                    sp.Children.Add(cartesianChart);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}