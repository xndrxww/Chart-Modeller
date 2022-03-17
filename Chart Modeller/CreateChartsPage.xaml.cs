using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Chart_Modeller
{
    public partial class CreateChartsPage : Page
    {
        private string ChartName;
        public CreateChartsPage(string pageName)
        {

            InitializeComponent();
            ChartName = pageName;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.PageName.Text = ChartName;

            //string connectionString = $@"Data Source={serverTxt.Text};Initial Catalog=master;Persist Security Info=True;User ID={loginTxt.Text};Password={passwordTxt.Text}";
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    try
            //    {
            //        connection.Open();
            //        Serialization();
            //        MainWindow.MainFrameInstance.Navigate(new PanelsPage());
            //    }
            //    catch (SqlException)
            //    {
            //        MessageBox.Show("Не удалось подключиться к серверу");
            //    }
            //}
        }
    }
}
