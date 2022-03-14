using Chart_Modeller.Models;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Chart_Modeller
{
    public partial class AddPanelWindow : Window
    {
        private string dbName;

        private Models.Panel panels;
        private int countPanels;

        private Database database;

        public AddPanelWindow(string currentDb)
        {
            InitializeComponent();
            panelName.Focus();
            dbName = currentDb;

        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            //countPanels = MainWindow.PanelsList.Count();

            panels = new Models.Panel()
            {
                Id = countPanels + 1,
                Name = panelName.Text
            };

            int i = 0;


            if (MainWindow.DatabasesList.Count > 0)
            {
                foreach (var item in MainWindow.DatabasesList.ToArray())
                {
                    if (item.Name == dbName)
                    {
                        item.Panels.Add(panels);
                        i++;
                        this.Close();
                        MainWindow.MainFrameInstance.Navigate(new PanelsPage());
                    }
                }
            }
            if (i == 0)
            {
                database = new Database();
                database.Name = dbName;
                database.Panels.Add(panels);
                MainWindow.DatabasesList.Add(database);
                this.Close();
                MainWindow.MainFrameInstance.Navigate(new PanelsPage());
            }
        }
    }
}
