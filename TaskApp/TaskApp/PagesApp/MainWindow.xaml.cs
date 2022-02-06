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

namespace TaskApp.PagesApp
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string savePath = "G:/TaskApp/Data/", nameFile = "Database_tasks";
        protected int nrTask = 0;
        int sizeCol;
        private List<Grid> tasks = new List<Grid>();
        private List<ColumnDefinition> columnsData = new List<ColumnDefinition>();
        public MainWindow()
        {
            InitializeComponent();
            OnChangedSizeWindow(null, null);
            InitializeSettings();

            LocalDatabase database = new LocalDatabase($"{savePath}{nameFile}");
            if (!database.SetData($@"CREATE TABLE IF NOT EXISTS Tasks (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    creation_data TEXT NOT NULL,
    title TEXT NOT NULL,
    description text NOT NULL,
    done bool NOT NULL
)"))
            {
                MessageBox.Show("Problem z inicjalizacją bazy danych. Spróbuj uruchomić aplikacje raz jeszcze lub zmienić ścieżke zapisu!", "Błąd aplikacji!");
                savePath = null;
            }
            else
            {
                RefreshTask(null, null);                
            }
        }

        private void InitializeSettings()
        {
            //to change!
            if (!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);
        }

        private void ClearAllTasks(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn?.CommandParameter.ToString() == "deleteTasks")
                DeleteAllTasks();

            //clearing tasks in UI
            tasks.Clear();
            columnsData.Clear();
            listTask.ItemsSource = null;
            nrTask = 0;
        }

        private void DeleteAllTasks()
        {
            if (savePath != "")
            {
                LocalDatabase database = new LocalDatabase($"{savePath}{nameFile}");
                if (database.SetData("DELETE FROM Tasks"))
                    MessageBox.Show("Pomyślnie usunięto wszystkie zadania!");
                else
                    MessageBox.Show("Niestety nie można usunąć zadań :(");

                deleteBtn.IsEnabled = false;
            }
            else
                MessageBox.Show("Problem z inicjalizacją bazy danych. Spróbuj uruchomić aplikacje raz jeszcze lub zmienić ścieżke zapisu!", "Błąd aplikacji!");
        }

        private void RefreshTask(object sender, RoutedEventArgs e)
        {
            if (savePath != "")
            {
                ClearAllTasks(null, null);
                LocalDatabase database = new LocalDatabase($"{savePath}{nameFile}");

                int x = 10;
                Console.WriteLine("liczba: " + database.GetNumberRows("Tasks"));
                while (x < database.GetNumberRows("Tasks") + 10)
                {
                    CreateTaskUI(database.GetData(x));
                    Console.WriteLine("liczba wierszy: " + database.GetNumberRows("Tasks"));
                    Console.WriteLine($"title: {database.GetData(x).Title}, des: {database.GetData(x).Description}");
                    x++;
                }

                if (database.GetNumberRows() > 0) deleteBtn.IsEnabled = true;
                else deleteBtn.IsEnabled = false;
            }
            else
                MessageBox.Show("Problem z inicjalizacją bazy danych. Spróbuj uruchomić aplikacje raz jeszcze lub zmienić ścieżke zapisu!", "Błąd aplikacji!");
        }

        private void AddNewTask(object sender, RoutedEventArgs e)
        {
            if (savePath != "")
            {
                LocalDatabase database = new LocalDatabase($"{savePath}{nameFile}");
                string query = $"INSERT INTO Tasks(creation_data, title, description, done) VALUES('{DateTime.UtcNow}', '{title.Text}', '{description.Text}', false)";
                if (database.SetData(query))
                    MessageBox.Show("Pomyślnie dodano nowe zadanie!");
                else
                    MessageBox.Show("Nie można dodać zadania :(");

                RefreshTask(null, null);
                creatorTask.Visibility = Visibility.Hidden;
                this.Width = 800;
                this.Height = 450;
                this.ResizeMode = ResizeMode.CanResize;
            }
            else
                MessageBox.Show("Problem z inicjalizacją bazy danych. Spróbuj uruchomić aplikacje raz jeszcze lub zmienić ścieżke zapisu!", "Błąd aplikacji!");
        }

        private void CreateTaskUI(DataTask data)
        {
            //creating row
            RowDefinition rowData = new RowDefinition();
            rowData.Height = new GridLength(35);

            //creating columns for data sqlite
            ColumnDefinition columnTitle = new ColumnDefinition();
            columnTitle.Width = new GridLength(sizeCol);
            columnsData.Add(columnTitle);

            ColumnDefinition columnStatus = new ColumnDefinition();
            columnStatus.Width = new GridLength(sizeCol);
            columnsData.Add(columnStatus);

            ColumnDefinition columnDetails = new ColumnDefinition();
            columnDetails.Width = new GridLength(sizeCol / 2);
            columnsData.Add(columnDetails);

            ColumnDefinition columnEdit = new ColumnDefinition();
            columnEdit.Width = new GridLength(sizeCol / 2);
            columnsData.Add(columnEdit);

            //creating grid and assign row and column to parent grid
            Grid bgTask = new Grid();
            bgTask.Height = 50f;
            bgTask.HorizontalAlignment = HorizontalAlignment.Stretch;
            bgTask.RowDefinitions.Add(rowData);
            bgTask.ColumnDefinitions.Add(columnTitle);
            bgTask.ColumnDefinitions.Add(columnStatus);
            bgTask.ColumnDefinitions.Add(columnEdit);
            bgTask.ColumnDefinitions.Add(columnDetails);

            //assign title to column 1
            Label title = new Label
            {
                Content = data.Title,
                FontSize = 16,
                FontWeight = FontWeights.Bold
            };
            Grid.SetColumn(title, 0);
            bgTask.Children.Add(title);

            //assign status (done) to column 2
            Label status = new Label
            {
                Content = data.Done ? "zakończone" : "w trakcie",
                FontSize = 16,
                FontWeight = FontWeights.Bold
            };
            Grid.SetColumn(status, 1);
            bgTask.Children.Add(status);

            //assign btn details to child bg task
            Button btnDetails = new Button
            {
                Content = "Szczegóły",
                BorderThickness = new Thickness(1),
                BorderBrush = new SolidColorBrush(Colors.Black)
            };
            Grid.SetColumn(btnDetails, 2);
            bgTask.Children.Add(btnDetails);

            //assign btn edit to child bg task
            Button btnEdit = new Button
            {
                Content = "Edytuj",
                BorderThickness = new Thickness(1),
                BorderBrush = new SolidColorBrush(Colors.Black)
            };
            Grid.SetColumn(btnEdit, 3);
            bgTask.Children.Add(btnEdit);

            tasks.Add(bgTask);
            listTask.ItemsSource = tasks.ToArray();
        }

        private void MenuCreationTask(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn.CommandParameter.ToString() == "openPanel")
            {
                creatorTask.Visibility = Visibility.Visible;
                this.Width = 810;
                this.Height = 465;
                this.ResizeMode = ResizeMode.NoResize;
            }
            else
            {
                creatorTask.Visibility = Visibility.Hidden;
                this.Width = 800;
                this.Height = 450;
                this.ResizeMode = ResizeMode.CanResize;
            }
        }

        private void OnChangedSizeWindow(object sender, SizeChangedEventArgs e)
        {
            sizeCol = (int)(listTask.ActualWidth / 4f);
            foreach (ColumnDefinition col in columnsData) col.Width = new GridLength(sizeCol);
        }
    }
}
