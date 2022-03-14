using Chart_Modeller.Models;
using System;
using System.Collections.Generic;
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
    public partial class MainWindow : Window
    {
        public static TextBlock PanelName;

        public static Frame MainFrameInstance;

        //Сервер
        private const string ServerFileName = "server.xml";


        //Панели
        public static List<Models.Panel> PanelsList;
        private const string PanelsFileName = "panels.xml";
        private static readonly XmlSerializer PanelsSerializer = new XmlSerializer(typeof(List<Models.Panel>));

        //новое
        public static List<Database> DatabasesList;
        private const string DatabaseFileName = "database.xml";
        private static readonly XmlSerializer DatabaseSerializer = new XmlSerializer(typeof(List<Database>));

        public MainWindow()
        {
            InitializeComponent();

            MainFrameInstance = MainFrame;
            PanelName = pageName;
            PanelsDeserialization();
            CheckServer();

            Closing += (sender, args) =>
            {
                DatabaseSerializer.Serialize(File.Create(DatabaseFileName), DatabasesList);
            };
        }

        private void CheckServer()
        {
            if (File.Exists(ServerFileName))
                MainFrameInstance.Navigate(new PanelsPage());
            else
                MainFrameInstance.Navigate(new ConnectDbPage());
        }

        private void PanelsDeserialization()
        {
            if (File.Exists(DatabaseFileName))
            {
                using (FileStream stream = File.OpenRead(DatabaseFileName))
                {
                    DatabasesList = (List<Database>)DatabaseSerializer.Deserialize(stream);
                }
            }
            else
                DatabasesList = new List<Database>();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.MainFrameInstance.CanGoBack)
                MainWindow.MainFrameInstance.GoBack();
        }
    }
}
