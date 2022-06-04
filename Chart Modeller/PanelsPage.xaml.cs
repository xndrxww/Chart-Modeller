using Npgsql;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace Chart_Modeller
{
    //Класс для отображения панелей
    public partial class PanelsPage : Page
    {
        private string connectionString;

        public PanelsPage()
        {
            InitializeComponent();
            CheckServerType();
            GetPanels();
            this.DataContext = MainWindow.Server;
        }

        //Метод для проверки типа СУБД
        private void CheckServerType()
        {
            if (MainWindow.Server.ServerType == "MS SQL Server")
            {
                connectionString = $@"Data Source={MainWindow.Server.ServerName};Initial Catalog=master;Persist Security Info=True;User ID={MainWindow.Server.Login};Password={MainWindow.Server.Password}";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    var table = new DataTable();
                    new SqlDataAdapter("select name from sys.databases", connection).Fill(table);
                    dbBox.ItemsSource = table.DefaultView;
                    dbBox.SelectedValuePath = "name";
                    dbBox.DisplayMemberPath = "name";
                }
            }
            else if (MainWindow.Server.ServerType == "PostgreSQL")
            {
                connectionString = $@"Host={MainWindow.Server.ServerName};Username={MainWindow.Server.Login};Password={MainWindow.Server.Password};Database=postgres";
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    var table = new DataTable();
                    new NpgsqlDataAdapter("SELECT datname FROM pg_database WHERE datistemplate = false;", connection).Fill(table);
                    dbBox.ItemsSource = table.DefaultView;
                    dbBox.SelectedValuePath = "datname";
                    dbBox.DisplayMemberPath = "datname";
                }
            }

            dbBox.SelectedIndex = dbBox.Items.Count - 1;
        }

        //Метод для получения списка панелей относительно выбранной БД
        public void GetPanels()
        {
            sp.Children.Clear();

            foreach (var server in MainWindow.ServersList)
            {
                foreach (var database in server.Databases)
                {
                    if (database.Name == dbBox.SelectedValue.ToString())
                    {
                        foreach (var panel in database.Panels)
                        {
                            Button panelButton = new Button
                            {
                                Margin = new Thickness(0, 50, 0, 0),
                                Width = 700,
                                Height = 80,
                                Content = panel.Name,
                                Background = new SolidColorBrush(Color.FromRgb(19, 28, 38)),
                                Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                                FontSize = 22,
                                BorderBrush = null
                            };

                            panelButton.Click += (s, ev) =>
                            {
                                MainWindow.Database.Name = dbBox.SelectedValue.ToString();
                                MainWindow.Panel.Name = panelButton.Content.ToString();
                                MainWindow.DbIndex = dbBox.SelectedIndex;
                                MainWindow.MainFrameInstance.Navigate(new ChartsPage());
                            };

                            sp.Children.Add(panelButton);
                        }
                    }
                }
            }
        }

        //Обработчик события нажжатия на кнопку "Создать панель"
        private void addPanel_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow();
        }

        //Обработчик события при загрузке страницы
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.PageName.Text = "Панели";
        }

        //Обработчик события при изменении выбранной секции в выпадающем списке
        private void dbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetPanels();
            SetDefault();
        }

        //Метод открытия окна для создания панели
        private void OpenWindow()
        {
            bool isWindowOpen = false;

            foreach (Window w in Application.Current.Windows)
            {
                if (w is AddPanelWindow)
                {
                    isWindowOpen = true;
                    w.Activate();
                }
            }

            MainWindow.DbIndex = dbBox.SelectedIndex;
            if (!isWindowOpen)
            {
                AddPanelWindow newwindow = new AddPanelWindow(dbBox.Text);
                newwindow.Show();
            }
        }

        //Обработчик события нажатия на кнопку "Выход"
        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrameInstance.Navigate(new ConnectDbPage());
        }

        private void SetDefault()
        {
            if (sp.Children.Count == 0)
            {
                defaultText.Text = $"У базы данных \"{dbBox.SelectedValue.ToString()}\" отсутствуют панели";
                stackPanelDefault.Visibility = Visibility.Visible;
            }
            else
            {
                stackPanelDefault.Visibility = Visibility.Hidden;
            }
        }
    }
}
