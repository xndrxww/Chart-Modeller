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
    //Класс для создания графика или диаграммы
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

        private List<PieSeries> PieSeriesList = new List<PieSeries>();

        public CreateChartsPage(string pageName)
        {
            InitializeComponent();

            PageName = pageName;

            SetConnectionString();

            LabelsList = new List<string>();

            if (PageName == "Создание круговой диаграммы")
            {
                chart.Visibility = Visibility.Hidden;
            }
        }

        //Метод для установки строки подключения в зависимости от выбранной СУБД
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

        //Обработчик события при загрузке страницы
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

        //Обработчик события при изменение выбранной секции в выпадающем списке с таблицами БД
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

        //Метод для заполнения выпадающих списков "Значение Х" и "Значение Y"
        private void FillBox()
        {
            columnsBox.Items.Clear();
            xBox.Items.Clear();

            foreach (DataColumn column in Table.Columns)
            {
                if (column.DataType == typeof(int) || column.DataType == typeof(decimal) || column.DataType == typeof(double) || column.DataType == typeof(long) || column.DataType == typeof(short))
                {
                    columnsBox.Items.Add(column.ColumnName);
                }

                if (column.DataType == typeof(string) || column.DataType == typeof(char) || column.DataType == typeof(DateTime))
                {
                    xBox.Items.Add(column.ColumnName);
                }
            }
        }

        //Метод для получения данных из таблицы в зависимости от выбранных значений в выпадающих списка
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

        //Метод для создания графика или диаграммы
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

        //Метод для добавления значений в график или диаграмму
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
                else if (Table.Columns[0].DataType == typeof(short))
                {
                    foreach (DataRow row in Table.Rows)
                    {
                        Values.Add((short)row[column.ColumnName]);
                    };
                    series.Values = new ChartValues<short>(Values.OfType<short>());
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

            if (Values.Count != 0)
            {
                Value.ValuesList.Add(Values);

                SetDecoration(series);

                EnableSaveButton();
            }
            return series;
        }

        //Метод для возмодности нажатия на кнопку "Сохранить"
        private void EnableSaveButton()
        {
            if (Value.ValuesList.Count != 0)
            {
                saveButton.IsEnabled = true;
            }
        }

        //Метод для добавления значений в круговую диаграмму
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
                        PieSeries.DataLabels = true;
                        PieSeriesList.Add(PieSeries);
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
                        PieSeries.DataLabels = true;
                        PieSeriesList.Add(PieSeries);
                    };
                }
                else if (Table.Columns[0].DataType == typeof(short))
                {
                    foreach (DataRow row in Table.Rows)
                    {
                        Values.Add((short)row[column.ColumnName]);

                        PieSeries = new PieSeries();
                        PieSeries.Values = new ChartValues<short> { (short)row[column.ColumnName] };
                        SeriesCollection.Add(PieSeries);
                        PieSeries.DataLabels = true;
                        PieSeriesList.Add(PieSeries);
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
                        PieSeries.DataLabels = true;
                        PieSeriesList.Add(PieSeries);
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
                        PieSeries.DataLabels = true;
                        PieSeriesList.Add(PieSeries);
                    };
                }
            }


            if (Values.Count != 0)
            {
                Value.ValuesList.Add(Values);

                SetDecoration(PieSeries);

                EnableSaveButton();
            }
        }

        //Метод для установки цвета и названия для графика или диаграммы
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

        //Метод для установки подписей для графика или диаграммы
        private void SetLabels(Series series)
        {
            foreach (DataColumn column in Table.Columns)
            {
                foreach (DataRow row in Table.Rows)
                {
                    LabelsList.Add(row[column.ColumnName].ToString());
                };
            }

            if (series == PieSeries)
            {
                for (int i = 0; i < PieSeriesList.Count; i++)
                {
                    for (int k = i; k < LabelsList.Count;)
                    {
                        PieSeriesList[i].Title = LabelsList[k];
                        break;
                    }
                }
            }

            Chart.LabelsList = this.LabelsList;

            series.DataLabels = true;
            DataContext = this;
        }

        //Обработчик события нажатия на кнопку "Очистить график"
        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            SeriesCollection.Clear();
            Value.ValuesList.Clear();
            DataContext = null;
            Chart = new Chart();
            chartName.Text = "";
            PieSeriesList.Clear();
            LabelsList.Clear();
            saveButton.IsEnabled = false;
        }

        //Метод для добавления графика или диаграммы в список для сохранения
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

        //Обработчик события нажатия на кнопку "Назад"
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrameInstance.Navigate(new ChartsPage());
        }

        //Метод открытия окна для отображения ошибки при создании графика или диаграммы
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

        //Обработчик события нажатия на кнопку "Сохранить"
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

        //Обработчик события нажатия на кнопку "Добавить значения"
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
