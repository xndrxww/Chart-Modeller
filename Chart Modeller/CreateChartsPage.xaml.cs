using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Chart_Modeller
{
    public partial class CreateChartsPage : Page
    {
        private string ChartName;
        private string connectionString;

        private static List<string> ColumnsList;
        public CreateChartsPage(string pageName)
        {
            InitializeComponent();
            ChartName = pageName;

            connectionString = $@"Data Source=LAPTOP-VAME4LJ4\KUZNETSOV;Initial Catalog={MainWindow.Database.Name};Persist Security Info=True;User ID=sa;Password=3232";

            ColumnsList = new List<string>();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.PageName.Text = ChartName;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var table = new DataTable();
                new SqlDataAdapter("select TABLE_NAME from INFORMATION_SCHEMA.TABLES", connection).Fill(table);

                tablesBox.ItemsSource = table.DefaultView;
                tablesBox.SelectedIndex = tablesBox.Items.Count - 1;
            }
        }

        private void tablesBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var table = new DataTable();
                new SqlDataAdapter($"select * from [{MainWindow.Database.Name}].[dbo].[{tablesBox.SelectedValue.ToString()}]", connection).Fill(table);

                dataGrid.ItemsSource = table.DefaultView;
            }

            FillColumnsBox();
        }

        private void FillColumnsBox()
        {
            columnsBox.Items.Clear();

            foreach (var item in dataGrid.Columns)
            {
                columnsBox.Items.Add(item.Header.ToString());
            }
        }

        //private void CreateChart()
        //{
        //    //foreach (var item in ColumnsList)
        //    //{
        //    //    LineSeries lineSeries = new LineSeries();
        //    //}
            
        //    SeriesCollection seriesCollection = new SeriesCollection
        //    {





        //        new LineSeries
        //        {
        //            Values = new ChartValues<double> { 3, 5, 7, 4 }
        //        },
        //        new LineSeries
        //        {
        //            Values = new ChartValues<double> { 13, 25, 17, 24 }
        //        },
        //    };

        //    chart.Series = seriesCollection;
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in columnsBox.SelectedItems)
            {
                ColumnsList.Add(item.ToString());
            }
        }
    }
}
