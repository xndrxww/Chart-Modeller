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

        private Panels panels;

        public AddPanelWindow()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            panels = new Panels()
            {
                Id = 1,
                Name = panelName.Text
            };
            MainWindow.PanelsList.Add(panels);
            this.Close();
            MainWindow.MainFrameInstance.Navigate(new PanelsPage());
        }
    }
}
