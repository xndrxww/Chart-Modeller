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

namespace Chart_Modeller
{
    public partial class MainWindow : Window
    {
        private const string ServerFileName = "server.xml";
        public static Frame MainFrameInstance;
        public MainWindow()
        {
            InitializeComponent();
            MainFrameInstance = MainFrame;
            CheckServer();
        }

        private void CheckServer()
        {
            if (File.Exists(ServerFileName))
                MainFrameInstance.Navigate(new PanelsPage());
            else
                MainFrameInstance.Navigate(new ConnectDbPage());
        }
    }
}
