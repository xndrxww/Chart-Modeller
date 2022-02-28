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
    }
}
