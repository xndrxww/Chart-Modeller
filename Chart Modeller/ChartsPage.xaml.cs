using System.Windows;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;

namespace Chart_Modeller
{
    public partial class ChartsPage : Page
    {
        private string PanelName;

        //private SeriesCollection SeriesCollection = new SeriesCollection();

        //private LineSeries LineSeries;

        public ChartsPage()
        {
            InitializeComponent();

            PanelName = MainWindow.Panel.Name;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.PageName.Text = PanelName;
            dbText.Text = MainWindow.Database.Name;
        }

        private void deletePanel_Click(object sender, RoutedEventArgs e)
        {
            foreach (var server in MainWindow.ServersList)
            {
                foreach (var database in server.Databases)
                {
                    if (database.Name == MainWindow.Database.Name)
                    {
                        foreach (var panel in database.Panels)
                        {
                            if (panel.Name == PanelName)
                            {
                                database.Panels.Remove(panel);
                                MainWindow.MainFrameInstance.Navigate(new PanelsPage());
                                break;
                            }
                        }
                    }
                }
            }
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

        //private void GetCharts()
        //{
        //    sp.Children.Clear();

        //    foreach (var server in MainWindow.ServersList)
        //    {
        //        foreach (var database in server.Databases)
        //        {
        //            if (database.Name == MainWindow.Database.Name)
        //            {
        //                foreach (var panel in database.Panels)
        //                {
        //                    if (panel.Name == MainWindow.Panel.Name)
        //                    {
        //                        foreach (var chart in panel.Charts)
        //                        {
        //                            LineSeries = new LineSeries();
        //                            LineSeries.Values = new ChartValues<int>(chart.ValuesInt);

        //                            CartesianChart cartesianChart = new CartesianChart();
                                    
        //                            SeriesCollection.Add(LineSeries);

        //                            cartesianChart.Series = SeriesCollection;

        //                            sp.Children.Add(cartesianChart);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
    }
}