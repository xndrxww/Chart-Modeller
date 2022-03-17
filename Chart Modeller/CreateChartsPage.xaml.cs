using System;
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
        private string connectionString = $@"Data Source=SPH-Monitoring;Initial Catalog=master;Persist Security Info=True;User ID=sa;Password=111111@N";

        public CreateChartsPage(string pageName)
        {

            InitializeComponent();
            ChartName = pageName;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.PageName.Text = ChartName;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var table = new DataTable();
                new SqlDataAdapter("select TABLE_NAME from DBAtools.INFORMATION_SCHEMA.TABLES", connection).Fill(table);

                tablesBox.ItemsSource = table.DefaultView;
                //tablesBox.SelectedIndex = dbBox.Items.Count - 1;
            }
        }

        private void tablesBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var table = new DataTable();
                new SqlDataAdapter($"select TOP(30) * from DBAtools.dbo.{tablesBox.SelectedValue.ToString()}", connection).Fill(table);

                dataGrid.ItemsSource = table.DefaultView;
                //tablesBox.SelectedIndex = dbBox.Items.Count - 1;
            }
        }
    }
}
