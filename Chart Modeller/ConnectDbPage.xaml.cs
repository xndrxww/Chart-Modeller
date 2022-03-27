using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Xml.Serialization;
using HandyControl.Controls;

namespace Chart_Modeller
{
    public partial class ConnectDbPage : Page
    {
        public ConnectDbPage()
        {
            InitializeComponent();
        }

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = $@"Data Source={serverTxt.Text};Initial Catalog=master;Persist Security Info=True;User ID={loginTxt.Text};Password={passwordTxt.Text}";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    AddServer();
                    MainWindow.MainFrameInstance.Navigate(new PanelsPage());
                }
                catch (SqlException)
                {
                    System.Windows.MessageBox.Show("Не удалось подключиться к серверу");
                    //HandyControl.Controls.MessageBox.Show("Не удалось подключиться к серверу", "Ошибка подключения", MessageBoxButton.OK);
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.PageName.Text = "Подключение к серверу";
        }

        private void AddServer()
        {
            MainWindow.Server = new Server()
            {
                ServerName = serverTxt.Text,
                Login = loginTxt.Text,
                Password = passwordTxt.Text
            };
            MainWindow.ServersList.Add(MainWindow.Server);
        }
    }
}
