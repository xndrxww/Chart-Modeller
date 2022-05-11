using Chart_Modeller.Models;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Chart_Modeller
{
    public partial class CreateChartsPage : Page
    {
        private string PageName;
        private string connectionString;
        private DataTable Table;

        private LineSeries LineSeries;
        private ColumnSeries ColumnSeries;
        private StackedAreaSeries StackedAreaSeries;
        private HeatSeries HeatSeries;
        private PieSeries PieSeries;
        private StepLineSeries StepLineSeries;
        private SeriesCollection SeriesCollection = new SeriesCollection();

        private ArrayList Values;
        public List<string> LabelsList { get; set; }

        private Chart Chart = new Chart();
        private Value Value = new Value();

        public CreateChartsPage(string pageName)
        {
            InitializeComponent();

            PageName = pageName;

            SetConnectionString();

            if (PageName == "Создание круговой диаграммы")
            {
                chart.Visibility = Visibility.Hidden;
            }
        }

        private void SetConnectionString()
        {
            if (MainWindow.Server.ServerType == "MS SQL Server")
            {
                connectionString = $@"Data Source={MainWindow.Server.ServerName};Initial Catalog={MainWindow.Database.Name};Persist Security Info=True;User ID={MainWindow.Server.Login};Password={MainWindow.Server.Password}";
            }
            else if (MainWindow.Server.ServerType == "PostgreSQL")
            {
                connectionString = $@"Host={MainWindow.Server.ServerName};Username={MainWindow.Server.Login};Password={MainWindow.Server.Password};Database={MainWindow.Database.Name}";
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.PageName.Text = PageName;

            if (MainWindow.Server.ServerType == "MS SQL Server")
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    Table = new DataTable();
                    new SqlDataAdapter("select table_name from INFORMATION_SCHEMA.TABLES", connection).Fill(Table);
                    tablesBox.SelectedValuePath = "table_name";
                    tablesBox.DisplayMemberPath = "table_name";
                }
            }

            else if (MainWindow.Server.ServerType == "PostgreSQL")
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    Table = new DataTable();
                    new NpgsqlDataAdapter("SELECT * FROM pg_catalog.pg_tables WHERE schemaname != 'pg_catalog' AND schemaname != 'information_schema';", connection).Fill(Table);
                }
                tablesBox.DisplayMemberPath = "tablename";
                tablesBox.SelectedValuePath = "tablename";
            }

            tablesBox.ItemsSource = Table.DefaultView;
            tablesBox.SelectedIndex = tablesBox.Items.Count - 1;
        }

        private void tablesBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainWindow.Server.ServerType == "MS SQL Server")
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    Table = new DataTable();
                    new SqlDataAdapter($"select * from [{MainWindow.Database.Name}].[dbo].[{tablesBox.SelectedValue}]", connection).Fill(Table);
                }
            }
            else if (MainWindow.Server.ServerType == "PostgreSQL")
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    Table = new DataTable();
                    try
                    {
                      new NpgsqlDataAdapter($"select * from {tablesBox.SelectedValue}", connection).Fill(Table);
                    }
                    catch (Exception)
                    {
                        
                    }
                }
            }

            dataGrid.ItemsSource = Table.DefaultView;

            FillBox();
        }

        private void FillBox()
        {
            columnsBox.Items.Clear();
            xBox.Items.Clear();

            foreach (DataColumn column in Table.Columns)
            {
                if (column.DataType == typeof(int) || column.DataType == typeof(decimal) || column.DataType == typeof(double) || column.DataType == typeof(Int64))
                {
                    columnsBox.Items.Add(column.ColumnName);
                }

                if (column.DataType == typeof(string) || column.DataType == typeof(char) || column.DataType == typeof(DateTime))
                {
                    xBox.Items.Add(column.ColumnName);
                }
            }
        }

        private void GetChartsData(ComboBox comboBox)
        {
            if (MainWindow.Server.ServerType == "MS SQL Server")
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    Table = new DataTable();
                    new SqlDataAdapter($"select {comboBox.SelectedItem} from [{MainWindow.Database.Name}].[dbo].[{tablesBox.SelectedValue}]", connection).Fill(Table);
                }
            }
            else if (MainWindow.Server.ServerType == "PostgreSQL")
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    Table = new DataTable();
                    new NpgsqlDataAdapter($"select {comboBox.SelectedItem} from {tablesBox.SelectedValue}", connection).Fill(Table);
                }
            }
        }

        private void CreateChart()
        {
            LineSeries = new LineSeries();
            ColumnSeries = new ColumnSeries();
            StackedAreaSeries = new StackedAreaSeries();
            HeatSeries = new HeatSeries();
            PieSeries = new PieSeries();
            StepLineSeries = new StepLineSeries();

            if (PageName != "Создание круговой диаграммы")
            {
                if (PageName == "Создание линейного графика")
                {
                    SeriesCollection.Add(CreateSeries(LineSeries));
                    Chart.Type = "LineSeries";
                }
                else if (PageName == "Создание гистограммы")
                {
                    SeriesCollection.Add(CreateSeries(ColumnSeries));
                    Chart.Type = "ColumnSeries";
                }
                else if (PageName == "Создание диаграммы-области")
                {
                    SeriesCollection.Add(CreateSeries(StackedAreaSeries));
                    Chart.Type = "StackedAreaSeries";
                }
                else if (PageName == "Создание теплового графика")
                {
                    SeriesCollection.Add(CreateSeries(HeatSeries));
                    Chart.Type = "HeatSeries";
                }
                else if (PageName == "Создание ступенчатой диаграммы")
                {
                    SeriesCollection.Add(CreateSeries(StepLineSeries));
                    Chart.Type = "StepLineSeries";
                }

                chart.Series = SeriesCollection;
            }
            else
            {
                CreatePieSeries();
                Chart.Type = "PieSeries";
                pieChart.Series = SeriesCollection;
            }
        }

        private Series CreateSeries(Series series)
        {
            Values = new ArrayList();
            Value = new Value();

            foreach (DataColumn column in Table.Columns)
            {
                if (Table.Columns[0].DataType == typeof(int))
                {
                    foreach (DataRow row in Table.Rows)
                    {
                        Values.Add((int)row[column.ColumnName]);
                    };
                    series.Values = new ChartValues<int>(Values.OfType<int>());
                }
                else if (Table.Columns[0].DataType == typeof(long))
                {
                    foreach (DataRow row in Table.Rows)
                    {
                        Values.Add((long)row[column.ColumnName]);
                    };
                    series.Values = new ChartValues<long>(Values.OfType<long>());
                }
                else if (Table.Columns[0].DataType == typeof(decimal))
                {
                    foreach (DataRow row in Table.Rows)
                    {
                        Values.Add((decimal)row[column.ColumnName]);
                    };
                    series.Values = new ChartValues<decimal>(Values.OfType<decimal>());
                }
                else if (Table.Columns[0].DataType == typeof(double))
                {
                    foreach (DataRow row in Table.Rows)
                    {
                        Values.Add((double)row[column.ColumnName]);
                    };
                    series.Values = new ChartValues<double>(Values.OfType<double>());
                }
            }

            Value.ValuesList.Add(Values);

            SetDecoration(series);

            return series;
        }

        private void CreatePieSeries()
        {
            Values = new ArrayList();
            Value = new Value();

            foreach (DataColumn column in Table.Columns)
            {
                if (Table.Columns[0].DataType == typeof(int))
                {
                    foreach (DataRow row in Table.Rows)
                    {
                        Values.Add((int)row[column.ColumnName]);

                        PieSeries = new PieSeries();
                        PieSeries.Values = new ChartValues<int> { (int)row[column.ColumnName] };
                        SeriesCollection.Add(PieSeries);
                        PieSeries.Title = lineNameTxt.Text;
                        PieSeries.DataLabels = true;
                    };
                }
                else if (Table.Columns[0].DataType == typeof(long))
                {
                    foreach (DataRow row in Table.Rows)
                    {
                        Values.Add((long)row[column.ColumnName]);

                        PieSeries = new PieSeries();
                        PieSeries.Values = new ChartValues<long> { (long)row[column.ColumnName] };
                        SeriesCollection.Add(PieSeries);
                        PieSeries.Title = lineNameTxt.Text;
                        PieSeries.DataLabels = true;
                    };
                }
                else if (Table.Columns[0].DataType == typeof(decimal))
                {
                    foreach (DataRow row in Table.Rows)
                    {
                        Values.Add((decimal)row[column.ColumnName]);

                        PieSeries = new PieSeries();
                        PieSeries.Values = new ChartValues<decimal> { (decimal)row[column.ColumnName] };
                        SeriesCollection.Add(PieSeries);
                        PieSeries.Title = lineNameTxt.Text;
                        PieSeries.DataLabels = true;
                    };
                }
                else if (Table.Columns[0].DataType == typeof(double))
                {
                    foreach (DataRow row in Table.Rows)
                    {
                        Values.Add((double)row[column.ColumnName]);

                        PieSeries = new PieSeries();
                        PieSeries.Values = new ChartValues<double> { (double)row[column.ColumnName] };
                        SeriesCollection.Add(PieSeries);
                        PieSeries.Title = lineNameTxt.Text;
                        PieSeries.DataLabels = true;
                    };
                }
            }

            Value.ValuesList.Add(Values);

            SetDecoration(PieSeries);
        }

        private void SetDecoration(Series series)
        {
            if (series != PieSeries)
            {
                series.Title = lineNameTxt.Text;
                Value.Title = series.Title;
            }

            chartName.Text = chartNameTxt.Text;

            if (colorPicker.SelectedBrush.Color.R != 255 && colorPicker.SelectedBrush.Color.G != 255 && colorPicker.SelectedBrush.Color.B != 255 && series != PieSeries)
            {
                series.Stroke = new SolidColorBrush(Color.FromRgb(colorPicker.SelectedBrush.Color.R, colorPicker.SelectedBrush.Color.G, colorPicker.SelectedBrush.Color.B));
                series.Fill = new SolidColorBrush(Color.FromArgb(123, colorPicker.SelectedBrush.Color.R, colorPicker.SelectedBrush.Color.G, colorPicker.SelectedBrush.Color.B));

                Value.StrokeColor = Color.FromRgb(colorPicker.SelectedBrush.Color.R, colorPicker.SelectedBrush.Color.G, colorPicker.SelectedBrush.Color.B);
                Value.FillColor = Color.FromArgb(123, colorPicker.SelectedBrush.Color.R, colorPicker.SelectedBrush.Color.G, colorPicker.SelectedBrush.Color.B);
            }

            Chart.Values.Add(Value);

            if (xBox.SelectedItem != null)
            {
                GetChartsData(xBox);
                SetLabels(series);
            }
        }

        private void SetLabels(Series series)
        {
            LabelsList = new List<string>();

            foreach (DataColumn column in Table.Columns)
            {
                foreach (DataRow row in Table.Rows)
                {
                    string labelRow = row[column.ColumnName].ToString();
                    LabelsList.Add(labelRow);
                };
            }

            Chart.LabelsList = this.LabelsList;

            series.DataLabels = true;
            DataContext = this;
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            SeriesCollection.Clear();
            Value.ValuesList.Clear();
            DataContext = null;
            Chart = new Chart();
            chartName.Text = "";
        }

        private void AddChart()
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
                                if (panel.Name == MainWindow.Panel.Name)
                                {
                                    panel.Charts.Add(Chart);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrameInstance.Navigate(new ChartsPage());
        }

        private void OpenWindow()
        {
            bool isWindowOpen = false;

            foreach (Window w in Application.Current.Windows)
            {
                if (w is ErrorWindow)
                {
                    isWindowOpen = true;
                    w.Activate();
                }
            }

            if (!isWindowOpen)
            {
                ErrorWindow newwindow = new ErrorWindow("Выберите значения для визуализации");
                newwindow.Show();
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (columnsBox.SelectedItem != null)
            {
                if (chartName.Text == "")
                {
                    chartName.Text = "Без названия";
                }

                Chart.Name = chartName.Text;
                AddChart();
                MainWindow.MainFrameInstance.Navigate(new ChartsPage());
            }
            else
                OpenWindow();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (columnsBox.SelectedItem != null)
            {
                GetChartsData(columnsBox);
                CreateChart();
            }
            else
                OpenWindow();
        }
    }
}
