using Chart_Modeller.Models;
using System.Windows;

namespace Chart_Modeller
{
    public partial class AddPanelWindow : Window
    {
        private string DbName;

        private Panel Panels;

        private Database Database;

        public AddPanelWindow(string currentDb)
        {
            InitializeComponent();
            panelNameTxt.Focus();
            DbName = currentDb;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {

            Panels = new Panel()
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
                        if (item2.Name == DbName)
                        {
                            item2.Panels.Add(Panels);
                            i++;
                            this.Close();
                            MainWindow.MainFrameInstance.Navigate(new PanelsPage());
                        }
                    }
                }
            }

            if (i == 0)
            {
                Database = new Database();
                Database.Name = DbName;
                Database.Panels.Add(Panels);

                foreach (var item in MainWindow.ServersList)
                {
                    if (MainWindow.Server.ServerName == item.ServerName)
                    {
                        item.Databases.Add(Database);
                    }
                }

                this.Close();
                MainWindow.MainFrameInstance.Navigate(new PanelsPage());
            }
        }
    }
}
