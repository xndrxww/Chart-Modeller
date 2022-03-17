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
        private Server server = new Server();

        public PanelsPage()
        {
            InitializeComponent();
            Deserialization();
            GetPanels();
        }

        private void Deserialization()
        {
            var serializer = new XmlSerializer(typeof(Server));
            using (FileStream stream = File.OpenRead("server.xml"))
            {
                server = (Server)serializer.Deserialize(stream);
            }

            FillDbBox();
        }

        private void FillDbBox()
        {
            string connectionString = $@"Data Source={server.ServerName};Initial Catalog=master;Persist Security Info=True;User ID={server.Login};Password={server.Password}";
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

            foreach (var item in MainWindow.DatabasesList)
            {
                if (item.Name == dbBox.SelectedValue.ToString())
                {
                    foreach (var item1 in item.Panels)
                    {
                        Button panel = new Button
                        {
                            Margin = new Thickness(0, 50, 0, 0),
                            Width = 600,
                            Height = 80,
                            Content = item1.Name + " Id = " + item1.Id,
                            Background = new SolidColorBrush(Color.FromRgb(32, 34, 38)),
                            Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255))
                        };

                        panel.Click += (s, ev) =>
                        {
                            MainWindow.MainFrameInstance.Navigate(new ChartsPage(panel.Content.ToString()));
                        };

                        sp.Children.Add(panel);
                    }
                }
            }
        }

        private void addPanel_Click(object sender, RoutedEventArgs e)
        {
            AddPanelWindow addPanelWindow = new AddPanelWindow(dbBox.Text);
            addPanelWindow.Show();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.PageName.Text = "Панели";
        }

        private void dbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetPanels();
        }
    }
}
