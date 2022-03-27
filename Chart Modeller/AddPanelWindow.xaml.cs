using Chart_Modeller.Models;
using System.Windows;

namespace Chart_Modeller
{
    public partial class AddPanelWindow : Window
    {
        private string dbName;

        private Panel panels;

        private Database database;

        public AddPanelWindow(string currentDb)
        {
            InitializeComponent();
            panelNameTxt.Focus();
            dbName = currentDb;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {

            panels = new Panel()
            {
                Name = panelNameTxt.Text
            };

            int i = 0;

            foreach (var item in MainWindow.ServersList)
            {
                if (item.Databases.Count > 0)
                {
                    foreach (var item2 in item.Databases.ToArray())
                    {
                        if (item2.Name == dbName)
                        {
                            item2.Panels.Add(panels);
                            i++;
                            this.Close();
                            MainWindow.MainFrameInstance.Navigate(new PanelsPage());
                        }
                    }
                }
            }

            if (i == 0)
            {
                database = new Database();
                database.Name = dbName;
                database.Panels.Add(panels);

                foreach (var item in MainWindow.ServersList)
                {
                    if (MainWindow.Server.ServerName == item.ServerName)
                    {
                        item.Databases.Add(database);
                    }
                }

                this.Close();
                MainWindow.MainFrameInstance.Navigate(new PanelsPage());
            }
        }
    }
}
