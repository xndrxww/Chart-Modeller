using Chart_Modeller.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
            }
        }

        public void GetPanels()
        {
            foreach (var item in MainWindow.PanelsList)
            {
                System.Windows.Controls.Button panel = new Button
                {
                    Margin = new Thickness(0, 50, 0, 0),
                    Width = 600,
                    Height = 80,
                    Content = item.Name + " Id = " + item.Id
                };

                panel.Click += (s, ev) =>
                {
                    MainWindow.MainFrameInstance.Navigate(new ChartsPage(panel.Content.ToString()));
                };

                sp.Children.Add(panel);
            }
        }

        private void addPanel_Click(object sender, RoutedEventArgs e)
        {
            AddPanelWindow addPanelWindow = new AddPanelWindow();
            addPanelWindow.Show();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.PanelName.Text = "Панели";
        }
    }
}
