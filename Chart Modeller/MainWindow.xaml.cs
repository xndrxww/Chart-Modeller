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
        
        public static Frame MainFrameInstance;

        //Сервер
        private const string ServerFileName = "server.xml";


        //Панели
        public static List<Panels> PanelsList;
        private const string PanelsFileName = "panels.xml";
        private static readonly XmlSerializer PanelsSerializer = new XmlSerializer(typeof(List<Panels>));

        public MainWindow()
        {
            InitializeComponent();
            PanelsDeserialization();
            MainFrameInstance = MainFrame;
            CheckServer();

            Closing += (sender, args) =>
            {
                PanelsSerializer.Serialize(File.Create(PanelsFileName), PanelsList);
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
            if (File.Exists(PanelsFileName))
            {
                using (FileStream stream = File.OpenRead(PanelsFileName))
                {
                    PanelsList = (List<Panels>)PanelsSerializer.Deserialize(stream);
                }
            }
            else
                PanelsList = new List<Panels>();
        }
    }
}
