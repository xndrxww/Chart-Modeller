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
