using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Npgsql;

namespace Chart_Modeller
{
    public partial class ConnectDbPage : Page
    {
        ArrayList ServerTypesList = new ArrayList();

        private string connectionString;
        public ConnectDbPage()
        {
            InitializeComponent();

            MainWindow.PageName.Text = "";

            SetServerTypeBox();
        }

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            if (serverTypeBox.SelectedValue.ToString() == "MS SQL Server")
            {
                connectionString = $@"Data Source={serverTxt.Text};Initial Catalog=master;Persist Security Info=True;User ID={loginTxt.Text};Password={passwordTxt.Text};Connection Timeout=3";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        CheckServer();
                    }
                    catch (SqlException)
                    {
                        OpenWindow();
                    }
                }
            }
            else if (serverTypeBox.SelectedValue.ToString() == "PostgreSQL")
            {
                connectionString = $@"Host={serverTxt.Text};Username={loginTxt.Text};Password={passwordTxt.Text};Database=postgres";
                try
                {
                    using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            CheckServer();
                        }
                        catch (SqlException)
                        {
                            OpenWindow();
                        }
                    }
                }
                catch (Exception)
                {
                    OpenWindow();
                }
            }
        }

        private void AddServer()
        {
            MainWindow.Server = new Server()
            {
                ServerType = serverTypeBox.SelectedValue.ToString(),
                ServerName = serverTxt.Text,
                Login = loginTxt.Text,
                Password = passwordTxt.Text
            };
            MainWindow.ServersList.Add(MainWindow.Server);
        }

        private void CheckServer()
        {
            if (MainWindow.ServersList.Count == 0)
            {
                AddServer();
                MainWindow.MainFrameInstance.Navigate(new PanelsPage());
            }
            else
            {
                bool ok = true;

                foreach (var server in MainWindow.ServersList)
                {
                    if (server.ServerName == serverTxt.Text || server.Login == loginTxt.Text || server.Password == passwordTxt.Text)
                    {
                        MainWindow.Server = new Server()
                        {
                            ServerType = serverTypeBox.SelectedValue.ToString(),
                            ServerName = serverTxt.Text,
                            Login = loginTxt.Text,
                            Password = passwordTxt.Text
                        };
                        ok = false;
                        MainWindow.MainFrameInstance.Navigate(new PanelsPage());
                        break;
                    }
                }
                if (ok)
                {
                    AddServer();
                    MainWindow.MainFrameInstance.Navigate(new PanelsPage());
                }
            }
        }

        private void SetServerTypeBox()
        {
            ServerTypesList.Add(new ServerType { Photo = "/Images/PostgreSQL.png", Name = "PostgreSQL" });
            ServerTypesList.Add(new ServerType { Photo = "/Images/MSSQLServer.png", Name = "MS SQL Server" });
            serverTypeBox.ItemsSource = ServerTypesList;
            serverTypeBox.SelectedIndex = serverTypeBox.Items.Count - 1;
        }

        public class ServerType
        {
            public string Photo { get; set; }
            public string Name { get; set; }
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
                ErrorWindow newwindow = new ErrorWindow("Ошибка при подключении к серверу");
                newwindow.Show();
            }
        }
    }
}
