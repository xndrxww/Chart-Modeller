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

            foreach (var item in MainWindow.ServersList)
            {
                foreach (var item1 in item.Databases)
                {
                    if (item1.Name == dbBox.SelectedValue.ToString())
                    {
                        foreach (var item2 in item1.Panels)
                        {
                            Button panel = new Button
                            {
                                Margin = new Thickness(0, 50, 0, 0),
                                Width = 600,
                                Height = 80,
                                Content = item2.Name,
                                Background = new SolidColorBrush(Color.FromRgb(19, 28, 38)),
                                Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                                FontSize = 22
                            };

                            panel.Click += (s, ev) =>
                            {
                                MainWindow.MainFrameInstance.Navigate(new ChartsPage(panel.Content.ToString()));
                                MainWindow.Database.Name = dbBox.SelectedValue.ToString();
                            };

                            sp.Children.Add(panel);
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

            if (!isWindowOpen)
            {
                AddPanelWindow newwindow = new AddPanelWindow(dbBox.Text);
                newwindow.Show();
            }
        }
    }
}
