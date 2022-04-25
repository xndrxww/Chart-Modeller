using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Serialization;

namespace Chart_Modeller
{
    public partial class PanelsPage : Page
    {
        public PanelsPage()
        {
            InitializeComponent();
            FillDbBox();
            GetPanels();
            this.DataContext = MainWindow.Server;
        }
        private void FillDbBox()
        {
            string connectionString = $@"Data Source={MainWindow.Server.ServerName};Initial Catalog=master;Persist Security Info=True;User ID={MainWindow.Server.Login};Password={MainWindow.Server.Password}";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var table = new DataTable();
                new SqlDataAdapter("select name from sys.databases", connection).Fill(table);
                dbBox.ItemsSource = table.DefaultView;
                dbBox.SelectedIndex = dbBox.Items.Count - 1;
            }
        }

        public void GetPanels()
        {
            sp.Children.Clear();

            foreach (var server in MainWindow.ServersList)
            {
                foreach (var database in server.Databases)
                {
                    if (database.Name == dbBox.SelectedValue.ToString())
                    {
                        foreach (var panel in database.Panels)
                        {
                            Button panelButton = new Button
                            {
                                Margin = new Thickness(0, 50, 0, 0),
                                Width = 700,
                                Height = 80,
                                Content = panel.Name,
                                Background = new SolidColorBrush(Color.FromRgb(19, 28, 38)),
                                Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                                FontSize = 22
                            };

                            panelButton.Click += (s, ev) =>
                            {
                                MainWindow.Database.Name = dbBox.SelectedValue.ToString();
                                MainWindow.Panel.Name = panelButton.Content.ToString();
                                MainWindow.DbIndex = dbBox.SelectedIndex;
                                MainWindow.MainFrameInstance.Navigate(new ChartsPage());
                            };

                            sp.Children.Add(panelButton);
                        }
                    }
                }
            }
        }

        private void addPanel_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.PageName.Text = "Панели";
        }

        private void dbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetPanels(); 
        }

        private void OpenWindow()
        {
            bool isWindowOpen = false;

            foreach (Window w in Application.Current.Windows)
            {
                if (w is AddPanelWindow)
                {
                    isWindowOpen = true;
                    w.Activate();
                }
            }

            MainWindow.DbIndex = dbBox.SelectedIndex;
            if (!isWindowOpen)
            {
                AddPanelWindow newwindow = new AddPanelWindow(dbBox.Text);
                newwindow.Show();
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrameInstance.Navigate(new ConnectDbPage());
        }
    }
}
