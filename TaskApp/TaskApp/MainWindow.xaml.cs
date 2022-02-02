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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaskApp
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {     
        protected int nrTask = 0;
        int sizeCol;
        private List<Grid> tasks = new List<Grid>();
        private List<ColumnDefinition> columnsData = new List<ColumnDefinition>();
        public MainWindow()
        {
            InitializeComponent();
            OnChangedSizeWindow(null, null);
        }

        private void ClearAllTasks(object sender, RoutedEventArgs e)
        {
            tasks.Clear();
            columnsData.Clear();
            listTask.ItemsSource = null;
            nrTask = 0;
        }

        private void CreateNewTask(object sender, RoutedEventArgs e)
        {
            OnChangedSizeWindow(null, null);
            nrTask++;

            //creating row
            RowDefinition rowData = new RowDefinition();
            rowData.Height = new GridLength(35);

            //creating columns for data sqlite
            ColumnDefinition columnDate = new ColumnDefinition();
            columnDate.Width = new GridLength(sizeCol);
            columnsData.Add(columnDate);

            ColumnDefinition columnTitle = new ColumnDefinition();
            columnTitle.Width = new GridLength(sizeCol);
            columnsData.Add(columnTitle);

            ColumnDefinition columnDetails = new ColumnDefinition();
            columnDetails.Width = new GridLength(sizeCol/2);
            columnsData.Add(columnDetails);

            ColumnDefinition columnEdit = new ColumnDefinition();
            columnEdit.Width = new GridLength(sizeCol/2);
            columnsData.Add(columnEdit);

            //creating grid and assign row and column to parent grid
            Grid bgTask = new Grid();
            bgTask.Height = 50f;
            bgTask.HorizontalAlignment = HorizontalAlignment.Stretch;
            bgTask.RowDefinitions.Add(rowData);
            bgTask.ColumnDefinitions.Add(columnDate);
            bgTask.ColumnDefinitions.Add(columnTitle);
            bgTask.ColumnDefinitions.Add(columnEdit);
            bgTask.ColumnDefinitions.Add(columnDetails);

            //assign date time to column 1
            DateTime dateTime = DateTime.UtcNow;

            Label date = new Label();
            date.Content = dateTime.ToString();
            date.FontSize = 16;
            date.FontWeight = FontWeights.Bold;
            Grid.SetColumn(date, 0);
            bgTask.Children.Add(date);

            //assign title to column 2
            Label title = new Label();
            title.Content = $"Nowe zadanie {nrTask}";
            title.FontSize = 16;
            title.FontWeight = FontWeights.Bold;
            Grid.SetColumn(title, 1);
            bgTask.Children.Add(title);

            //assign btn details to child bg task
            Button btnDetails = new Button();
            btnDetails.Content = $"Szczegóły";
            btnDetails.BorderThickness = new Thickness(1);
            btnDetails.BorderBrush = new SolidColorBrush(Colors.Black);
            Grid.SetColumn(btnDetails, 2);
            bgTask.Children.Add(btnDetails);

            //assign btn edit to child bg task
            Button btnEdit = new Button();
            btnEdit.Content = $"Edytuj";
            btnEdit.BorderThickness = new Thickness(1);
            btnEdit.BorderBrush = new SolidColorBrush(Colors.Black);
            Grid.SetColumn(btnEdit, 3);
            bgTask.Children.Add(btnEdit);

            tasks.Add(bgTask);
            listTask.ItemsSource = tasks.ToArray();
        }

        private void OnChangedSizeWindow(object sender, SizeChangedEventArgs e)
        {
            sizeCol = (int)(listTask.ActualWidth / 4f);
            foreach (ColumnDefinition col in columnsData) col.Width = new GridLength(sizeCol);
        }
    }
}
