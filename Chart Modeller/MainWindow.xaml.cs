using Chart_Modeller.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Chart_Modeller
{
    //Класс главного окна приложения
    public partial class MainWindow : Window
    {
        public static TextBlock PageName;

        public static Frame MainFrameInstance;

        public static string ConnectionString;

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
                if (ServersList.Count != 0)
                {
                    ServerSerializer.Serialize(File.Create(ServerFileName), ServersList);
                }
            };
        }

        //Метод для проверки на существование файла "server.xml"
        private void CheckServer()
        {
            if (File.Exists(ServerFileName))
                MainFrameInstance.Navigate(new PanelsPage());
            else
                MainFrameInstance.Navigate(new ConnectDbPage());
        }

        //Метод для десериализации данных из файла
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
                    Server.ServerType = server.ServerType;
                    Server.ServerName = server.ServerName;
                    Server.Login = server.Login;
                    Server.Password = server.Password;
                }
            }
            else
                ServersList = new List<Server>();
        }

        //Обработчик события, при нажатии левой кнопкой мыши на Grid, для перетаскивания текущего окна
        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        //Обработчик события нажатия на кнопку для того, чтобы закруть приложение
        private void closeAppButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //Обработчик события нажатия на кнопку для того, чтобы свернуть приложения
        private void minimizeAppButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
