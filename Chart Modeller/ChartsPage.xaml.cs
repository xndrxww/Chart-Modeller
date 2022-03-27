using System.Windows;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;

namespace Chart_Modeller
{
    public partial class ChartsPage : Page
    {
        private string PanelName;
        public ChartsPage(string panelName)
        {
            InitializeComponent();

            PanelName = panelName;
            
            SeriesCollection seriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double> { 3, 5, 7, 4 }
                },
                new LineSeries
                {
                    Values = new ChartValues<double> { 13, 25, 17, 24 }
                },
            };

            chart.Series = seriesCollection;
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
            ChooseChartWindow chooseChartWindow = new ChooseChartWindow();
            chooseChartWindow.Show();
        }
    }
}
