using Chart_Modeller.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace Chart_Modeller
{
    public partial class MainWindow : Window
    {
        public static TextBlock PageName;

        public static Frame MainFrameInstance;

        //Сервер
        public static List<Server> ServersList;
        public static Server Server;
        public static Database Database;
        public static Models.Panel Panel;
        private const string ServerFileName = "server.xml";
        private static readonly XmlSerializer ServerSerializer = new XmlSerializer(typeof(List<Server>));
        public static int DbIndex;


        public MainWindow()
        {
            InitializeComponent();

            MainFrameInstance = MainFrame;
            PageName = pageName;
            Database = new Database();
            Server = new Server();
            Panel = new Models.Panel();

            Deserialization();

            CheckServer();

            Closing += (sender, args) =>
            {
                ServerSerializer.Serialize(File.Create(ServerFileName), ServersList);
            };
        }

        private void CheckServer()
        {
            if (File.Exists(ServerFileName))
                MainFrameInstance.Navigate(new PanelsPage());
            else
                MainFrameInstance.Navigate(new ConnectDbPage());
        }

        private void Deserialization()
        {
            if (File.Exists(ServerFileName))
            {
                using (FileStream stream = File.OpenRead(ServerFileName))
                {
                    ServersList = (List<Server>)ServerSerializer.Deserialize(stream);
                }
                foreach (var server in ServersList)
                {
                    Server.ServerName = server.ServerName;
                    Server.Login = server.Login;
                    Server.Password = server.Password;
                }
            }
            else
                ServersList = new List<Server>();
        }
    }
}
