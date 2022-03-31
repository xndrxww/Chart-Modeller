using Chart_Modeller.Models;
using LiveCharts;
using LiveCharts.Wpf;
using System;
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
        private SeriesCollection SeriesCollection = new SeriesCollection();

        public List<string> LabelsList { get; set; }

        //private Chart Chart = new Chart();

        public CreateChartsPage(string pageName)
        {
            InitializeComponent();
            PageName = pageName;

            connectionString = $@"Data Source={MainWindow.Server.ServerName};Initial Catalog={MainWindow.Database.Name};Persist Security Info=True;User ID={MainWindow.Server.Login};Password={MainWindow.Server.Password}";
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.PageName.Text = PageName;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Table = new DataTable();
                new SqlDataAdapter("select TABLE_NAME from INFORMATION_SCHEMA.TABLES", connection).Fill(Table);

                tablesBox.ItemsSource = Table.DefaultView;
                tablesBox.SelectedIndex = tablesBox.Items.Count - 1;
            }
        }

        private void tablesBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Table = new DataTable();
                new SqlDataAdapter($"select * from [{MainWindow.Database.Name}].[dbo].[{tablesBox.SelectedValue}]", connection).Fill(Table);

                dataGrid.ItemsSource = Table.DefaultView;
            }

            FillBox();
        }

        private void FillBox()
        {
            columnsBox.Items.Clear();

            foreach (var item in dataGrid.Columns)
            {
                columnsBox.Items.Add(item.Header.ToString());
            }

            xBox.Items.Clear();
            foreach (var item in dataGrid.Columns)
            {
                xBox.Items.Add(item.Header.ToString());
            }
        }

        private void GetChartsData(ComboBox comboBox)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Table = new DataTable();
                new SqlDataAdapter($"select {comboBox.SelectedItem} from [{MainWindow.Database.Name}].[dbo].[{tablesBox.SelectedValue}]", connection).Fill(Table);
            }
        }

        private void CreateChart()
        {
            SeriesCollection.Add(CreateLineSeries());

            chart.Series = SeriesCollection;

            AddChart();
            //тестик
            //chart.Background = new SolidColorBrush(Color.FromRgb(32, 34, 38));
        }

        private LineSeries CreateLineSeries()
        {
            List<int> valuesInt = new List<int>();
            List<decimal> valuesDecimal = new List<decimal>();
            List<double> valuesDouble = new List<double>();

            foreach (DataColumn column in Table.Columns)
            {
                LineSeries = new LineSeries();

                if (Table.Columns[0].DataType == typeof(int))
                {
                    foreach (DataRow row in Table.Rows)
                    {
                        valuesInt.Add((int)row[column.ColumnName]);
                    };
                    LineSeries.Values = new ChartValues<int>(valuesInt);
                }
                else if (Table.Columns[0].DataType == typeof(decimal))
                {
                    foreach (DataRow row in Table.Rows)
                    {
                        valuesDecimal.Add((decimal)row[column.ColumnName]);
                    };
                    LineSeries.Values = new ChartValues<decimal>(valuesDecimal);
                }
                else if (Table.Columns[0].DataType == typeof(double))
                {
                    foreach (DataRow row in Table.Rows)
                    {
                        valuesDouble.Add((double)row[column.ColumnName]);
                    };
                    LineSeries.Values = new ChartValues<double>(valuesDouble);
                }
            }

            SetLineDecoration();

            return LineSeries;
        }
        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            if (columnsBox.SelectedItem != null)
            {
                GetChartsData(columnsBox);
                CreateChart();
            }
        }

        private void SetLineDecoration()
        {
            LineSeries.Title = lineNameTxt.Text;

            if (colorPicker.SelectedBrush.Color.R != 255 && colorPicker.SelectedBrush.Color.G != 255 && colorPicker.SelectedBrush.Color.B != 255)
            {
                LineSeries.Stroke = new SolidColorBrush(Color.FromRgb(colorPicker.SelectedBrush.Color.R, colorPicker.SelectedBrush.Color.G, colorPicker.SelectedBrush.Color.B));
                LineSeries.Fill = new SolidColorBrush(Color.FromArgb(123, colorPicker.SelectedBrush.Color.R, colorPicker.SelectedBrush.Color.G, colorPicker.SelectedBrush.Color.B));
            }

            //LineSeries.StrokeDashArray = "2";
            if (xBox.SelectedItem != null)
            {
                GetChartsData(xBox);
                SetLabels();
            }
        }

        private void SetLabels()
        {
            LabelsList = new List<string>();
            LabelsList.Clear();
            foreach (DataColumn column in Table.Columns)
            {
                foreach (DataRow row in Table.Rows)
                {
                    LabelsList.Add((string)row[column.ColumnName]);
                };
            }

            LineSeries.DataLabels = true;
            DataContext = this;
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            SeriesCollection.Clear();
        }

        private void AddChart()
        {
            //Chart.SeriesCollection = SeriesCollection;
            //MainWindow.Panel.Charts.Add(Chart);
        }
    }
}
