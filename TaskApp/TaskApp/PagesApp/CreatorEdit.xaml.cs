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

namespace TaskApp.PagesApp
{
    /// <summary>
    /// Logika interakcji dla klasy CreatorEdit.xaml
    /// </summary>
    public partial class CreatorEdit : Page
    {
        bool edit;
        public CreatorEdit(bool edit = false)
        {
            InitializeComponent();
            this.edit = edit;
        }

        private void DoneTask(object sender, RoutedEventArgs e)
        {

        }

        private void BackToMain(object sender, RoutedEventArgs e)
        {
            
        }

        private void AddNewTask(string title, string description, int time_period = 5)
        {
            MainWindow main = new MainWindow();

            LocalDatabase database = new LocalDatabase($"{main.savePath}baza_test");
            if (database.SetData($"INSERT INTO Tasks(creation_data, title, description, time_period) VALUES({DateTime.UtcNow}, {title}, {description}, {time_period})"))
            {
                MessageBox.Show("Zadanie zostało dodane!");
            }
            else
            {
                MessageBox.Show("Sprawdź czy napewno wpisałeś wszystkie dane do zadania.", "Nie można dodać zadania!");
            }
        }
    }
}
