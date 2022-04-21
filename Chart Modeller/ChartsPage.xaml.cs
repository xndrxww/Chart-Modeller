using System;
using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Chart_Modeller.Models;
using LiveCharts;
using LiveCharts.Wpf;

namespace Chart_Modeller
{
    public partial class ChartsPage : Page
    {
        private string PanelName;

        private SeriesCollection SeriesCollection;

        private LineSeries LineSeries;
        private ColumnSeries ColumnSeries;
        private StackedAreaSeries StackedAreaSeries;
        private HeatSeries HeatSeries;
        private PieSeries PieSeries;
        private StepLineSeries StepLineSeries;


        public ChartsPage()
        {
            InitializeComponent();

            PanelName = MainWindow.Panel.Name;
            GetCharts();
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

                                PanelsPage panelsPage = new PanelsPage();
                                panelsPage.dbBox.SelectedIndex = MainWindow.DbIndex;
                                MainWindow.MainFrameInstance.Navigate(panelsPage);

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
                                    if (chart.Type == "LineSeries")
                                        CreateChart(LineSeries, chart);

                                    else if (chart.Type == "ColumnSeries")
                                        CreateChart(ColumnSeries, chart);

                                    else if (chart.Type == "StackedAreaSeries")
                                        CreateChart(StackedAreaSeries, chart);

                                    else if (chart.Type == "HeatSeries")
                                        CreateChart(HeatSeries, chart);

                                    else if (chart.Type == "PieSeries")
                                    {
                                        ////////////
                                    }

                                    else if (chart.Type == "StepLineSeries")
                                        CreateChart(StepLineSeries, chart);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void CreateChart(Series series, Chart chart)
        {
            TextBlock chartName = new TextBlock
            {
                Margin = new Thickness(5),
                Text = chart.Name,
                TextAlignment = TextAlignment.Center,
                FontSize = 25
            };

            Button deleteButton = new Button
            {
                Width = 70,
                Height = 35,
                Margin = new Thickness(950,0,0,0),
                ToolTip = "Удалить " + chartName.Text
            };

            deleteButton.Content = new Image
            {
                Source = new BitmapImage(new Uri("/Images/delete.png", UriKind.RelativeOrAbsolute)),
                Width = 18
            };

            deleteButton.Click += (s, ev) =>
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
                                    foreach (var item in panel.Charts)
                                    {
                                        if (item.Name == chart.Name)
                                        {
                                            panel.Charts.Remove(item);
                                            MainWindow.MainFrameInstance.Navigate(new ChartsPage());
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            CartesianChart cartesianChart = new CartesianChart
            {
                Margin = new Thickness(0, 0, 0, 80),
                Width = 1050,
                Height = 420
            };

            SeriesCollection = new SeriesCollection();


            foreach (ArrayList value in chart.ValuesList)
            {
                for (int i = 0; i < chart.ValuesList.Count; i++)
                {
                    if (chart.Type == "LineSeries")
                    {
                        series = new LineSeries();
                    }
                    else if (chart.Type == "ColumnSeries")
                    {
                        series = new ColumnSeries();
                    }
                    else if (chart.Type == "StackedAreaSeries")
                    {
                        series = new StackedAreaSeries();
                    }
                    else if (chart.Type == "HeatSeries")
                    {
                        series = new HeatSeries();
                    }
                    else if (chart.Type == "PieSeries")
                    {
                        ///
                    }
                    else if (chart.Type == "StepLineSeries")
                    {
                        series = new StepLineSeries();
                    }

                    series.Values = new ChartValues<int>(value.OfType<int>());
                    //series.Stroke = new SolidColorBrush(chart.StrokeColor[i]);
                    //series.Fill = new SolidColorBrush(chart.FillColor[i]);
                    series.Stroke = new SolidColorBrush(chart.StrokeColor);
                    series.Fill = new SolidColorBrush(chart.FillColor);
                    series.Title = chart.Title;

                    SeriesCollection.Add(series);
                }
            }

            cartesianChart.Series = SeriesCollection;

            sp.Children.Add(chartName);
            sp.Children.Add(deleteButton);
            sp.Children.Add(cartesianChart);
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            PanelsPage panelsPage = new PanelsPage();
            panelsPage.dbBox.SelectedIndex = MainWindow.DbIndex;
            MainWindow.MainFrameInstance.Navigate(panelsPage);
        }
    }
}