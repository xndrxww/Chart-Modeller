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
        private const string ServerFileName = "server.xml";


        //База данных + панели
        public static List<Database> DatabasesList;
        public static Database Database;
        private const string DatabaseFileName = "database.xml";
        private static readonly XmlSerializer DatabaseSerializer = new XmlSerializer(typeof(List<Database>));

        public MainWindow()
        {
            InitializeComponent();

            MainFrameInstance = MainFrame;
            PageName = pageName;
            Database = new Database();

            DatabaseDeserialization();
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

        private void DatabaseDeserialization()
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
