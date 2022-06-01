using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Chart_Modeller.Models;
using HandyControl.Controls;
using LiveCharts;
using LiveCharts.Wpf;

namespace Chart_Modeller
{
    //Класс для вывода графиков и диаграмм на панель
    public partial class ChartsPage : Page
    {
        private string PanelName;

        private SeriesCollection SeriesCollection;

        private Series series;


        private CartesianChart CartesianChart;
        private PieChart PieChart;

        private PieSeries PieSeries;

        private List<PieSeries> PieSeriesList = new List<PieSeries>();


        public ChartsPage()
        {
            InitializeComponent();

            PanelName = MainWindow.Panel.Name;
            GetCharts();
        }

        //Обработчик события при загрузке страницы
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.PageName.Text = PanelName;
            dbText.Text = MainWindow.Database.Name;
        }

        // Обработчик события нажатия на кнопку "Удалить панель"
        private void deletePanel_Click(object sender, RoutedEventArgs e)
        {
            foreach (var server in MainWindow.ServersList)
            {
                if (server.ServerName == MainWindow.Server.ServerName)
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
        }

        //Обработчик события нажатия на кнопку "Создать график"
        private void addChart_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow();
        }

        //Метод открытия окна для выбора типа графика или диаграммы
        private void OpenWindow()
        {
            bool isWindowOpen = false;

            foreach (System.Windows.Window w in Application.Current.Windows)
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

        //Метод для получения списка графиков и диграмм на текущей панели
        private void GetCharts()
        {
            sp.Children.Clear();

            foreach (var server in MainWindow.ServersList)
            {
                if (server.ServerName == MainWindow.Server.ServerName)
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
                                        CreateChart(chart);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        //Метод для вывода графиков и диаграмм на панель
        private void CreateChart(Chart chart)
        {
            TextBlock chartName = new TextBlock
            {
                Text = chart.Name,
                TextAlignment = TextAlignment.Center,
                FontSize = 25,
                HorizontalAlignment = HorizontalAlignment.Center,
            };

            Button deleteButton = new Button
            {
                Width = 70,
                Height = 35,
                HorizontalAlignment = HorizontalAlignment.Right,
                ToolTip = "Удалить " + chartName.Text,
                BorderBrush = null,
                VerticalAlignment = VerticalAlignment.Center
            };

            Grid grid = new Grid
            {
                Width = 1000,
                Height = 50,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 5, 0, 0)
            };

            grid.Children.Add(chartName);
            grid.Children.Add(deleteButton);

            deleteButton.Content = new Image
            {
                Source = new BitmapImage(new Uri("/Images/delete.png", UriKind.RelativeOrAbsolute)),
                Width = 18
            };

            deleteButton.Click += (s, ev) =>
            {
                foreach (var server in MainWindow.ServersList)
                {
                    if (server.ServerName == MainWindow.Server.ServerName)
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
                }
            };

            SeriesCollection = new SeriesCollection();

            if (chart.Type == "PieSeries")
            {
                foreach (var value in chart.Values)
                {
                    CreatePieChart(value, chart);
                }

                sp.Children.Add(grid);
                sp.Children.Add(PieChart);
            }
            else
            {
                foreach (var value in chart.Values)
                {
                    CheckSeries(chart);

                    CartesianChart = new CartesianChart
                    {
                        Margin = new Thickness(0, 0, 0, 80),
                        Width = 1050,
                        Height = 420
                    };

                    Type type = null;

                    foreach (ArrayList values in value.ValuesList)
                    {
                        foreach (var item in values)
                        {
                            if (item.GetType() == typeof(int) || item.GetType() == typeof(long) || item.GetType() == typeof(decimal) || item.GetType() == typeof(double) || item.GetType() == typeof(short))
                            {
                                type = item.GetType();
                            }
                        }

                        if (type == typeof(int))
                        {
                            series.Values = new ChartValues<int>(values.OfType<int>());
                        }
                        else if (type == typeof(long))
                        {
                            series.Values = new ChartValues<long>(values.OfType<long>());
                        }
                        else if (type == typeof(short))
                        {
                            series.Values = new ChartValues<short>(values.OfType<short>());
                        }
                        else if (type == typeof(decimal))
                        {
                            series.Values = new ChartValues<decimal>(values.OfType<decimal>());
                        }
                        else if (type == typeof(double))
                        {
                            series.Values = new ChartValues<double>(values.OfType<double>());
                        }
                    }

                    series.Title = value.Title;

                    if (value.StrokeColor != Color.FromRgb(0, 0, 0) && value.FillColor != Color.FromArgb(0, 0, 0, 0))
                    {
                        series.Stroke = new SolidColorBrush(value.StrokeColor);
                        series.Fill = new SolidColorBrush(value.FillColor);
                    }

                    SeriesCollection.Add(series);
                }

                CartesianChart.Series = SeriesCollection;

                Axis axis = new Axis();
                axis.Labels = chart.LabelsList;
                CartesianChart.AxisX.Add(axis);

                sp.Children.Add(grid);
                sp.Children.Add(CartesianChart);
            }
        }

        //Метод для вывода круговых диаграмм на панель
        private void CreatePieChart(Value value, Chart chart)
        {
            PieChart = new PieChart
            {
                Margin = new Thickness(0, 0, 0, 80),
                Height = 350,
                LegendLocation = LegendLocation.Bottom
            };

            Type type = null;

            foreach (ArrayList values in value.ValuesList)
            {
                foreach (var item in values)
                {
                    if (item.GetType() == typeof(int) || item.GetType() == typeof(long) || item.GetType() == typeof(decimal) || item.GetType() == typeof(double) || item.GetType() == typeof(short))
                    {
                        type = item.GetType();
                    }
                }

                if (type == typeof(int))
                {
                    foreach (int item in values)
                    {
                        PieSeries = new PieSeries();
                        PieSeries.Values = new ChartValues<int> { item };
                        SeriesCollection.Add(PieSeries);
                        PieSeries.Title = value.Title;
                        PieSeries.DataLabels = true;
                        PieSeriesList.Add(PieSeries);
                    }
                }
                else if (type == typeof(long))
                {
                    foreach (long item in values)
                    {
                        PieSeries = new PieSeries();
                        PieSeries.Values = new ChartValues<long> { item };
                        SeriesCollection.Add(PieSeries);
                        PieSeries.Title = value.Title;
                        PieSeries.DataLabels = true;
                        PieSeriesList.Add(PieSeries);
                    }
                }
                else if (type == typeof(short))
                {
                    foreach (short item in values)
                    {
                        PieSeries = new PieSeries();
                        PieSeries.Values = new ChartValues<short> { item };
                        SeriesCollection.Add(PieSeries);
                        PieSeries.Title = value.Title;
                        PieSeries.DataLabels = true;
                        PieSeriesList.Add(PieSeries);
                    }
                }
                else if (type == typeof(decimal))
                {
                    foreach (decimal item in values)
                    {
                        PieSeries = new PieSeries();
                        PieSeries.Values = new ChartValues<decimal> { item };
                        SeriesCollection.Add(PieSeries);
                        PieSeries.Title = value.Title;
                        PieSeries.DataLabels = true;
                        PieSeriesList.Add(PieSeries);
                    }
                }
                else if (type == typeof(double))
                {
                    foreach (double item in values)
                    {
                        PieSeries = new PieSeries();
                        PieSeries.Values = new ChartValues<double> { item };
                        SeriesCollection.Add(PieSeries);
                        PieSeries.Title = value.Title;
                        PieSeries.DataLabels = true;
                        PieSeriesList.Add(PieSeries);
                    }
                }
            }

            for (int i = 0; i < PieSeriesList.Count; i++)
            {
                for (int k = i; k < chart.LabelsList.Count;)
                {
                    PieSeriesList[i].Title = chart.LabelsList[k];
                    break;
                }
            }

            PieChart.Series = SeriesCollection;
        }

        //Обработчик события нажжатия на кнопку "Назад"
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            PanelsPage panelsPage = new PanelsPage();
            panelsPage.dbBox.SelectedIndex = MainWindow.DbIndex;
            MainWindow.MainFrameInstance.Navigate(panelsPage);
        }

        //Метод для опрделения типа графика для последующего вывода на панель
        private Series CheckSeries(Chart chart)
        {
            if (chart.Type == "LineSeries")
                series = new LineSeries();

            else if (chart.Type == "ColumnSeries")
                series = new ColumnSeries();

            else if (chart.Type == "StackedAreaSeries")
                series = new StackedAreaSeries();

            else if (chart.Type == "HeatSeries")
                series = new HeatSeries();


            else if (chart.Type == "StepLineSeries")
                series = new StepLineSeries();

            return series;
        }
    }
}