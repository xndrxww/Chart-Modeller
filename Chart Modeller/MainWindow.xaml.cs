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
        private const string ServerFileName = "server.xml";
        private static readonly XmlSerializer ServerSerializer = new XmlSerializer(typeof(List<Server>));



        public MainWindow()
        {
            InitializeComponent();

            MainFrameInstance = MainFrame;
            PageName = pageName;
            Database = new Database();
            Server = new Server();

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

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.MainFrameInstance.CanGoBack)
                MainWindow.MainFrameInstance.GoBack();
        }

        private void Deserialization()
        {
            if (File.Exists(ServerFileName))
            {
                using (FileStream stream = File.OpenRead(ServerFileName))
                {
                    ServersList = (List<Server>)ServerSerializer.Deserialize(stream);
                }
                foreach (var item in ServersList)
                {
                    Server.ServerName = item.ServerName;
                    Server.Login = item.Login;
                    Server.Password = item.Password;
                }
            }
            else
                ServersList = new List<Server>();
        }
    }
}
