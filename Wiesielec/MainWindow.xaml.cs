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

namespace Wiesielec
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region variable
        private string[] words = { "kabanos", "ala ma kota", "komputer jest przyszłością" };
        private char[] currentlyPass;
        private string pass;
        private int mistake = 0;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            pass = RandomWord();
            passTxt.Content = pass;

            currentlyPass = new char[pass.Length];
            for(int i = 0; i < currentlyPass.Length; i++)
            {
                currentlyPass[i] = '_';
            }

            RefreshPasswordAndStickman();
        }

        private string RandomWord()
        {
            var rand = new Random();
            return words[rand.Next(0, words.Length)].ToString();
        }

        private void RefreshPasswordAndStickman()
        {
            for(int index = 0; index < pass.Length; index++)
            {
                passTxt.Content = null;
                passTxt.Content += $" {currentlyPass[index]}";
            }
            Console.WriteLine($"length: {pass.Length}");
        }

        private void CheckLetter(object sender, RoutedEventArgs e)
        {
            if (txtBox.Text != "")
            {
                var letter = char.ToLower(txtBox.Text[0]);
                bool succes = false;
                for (int i = 0; i < pass.Length; i++)
                {
                    if (letter == pass[i] && currentlyPass[i] != letter)
                    {
                        currentlyPass[i] = letter;
                        succes = true;
                    }
                }

                if (!succes)
                {
                    //stickman refresh mistake
                }

                //call to refresh data
                RefreshPasswordAndStickman();
                txtBox.Text = null;
            }
            else
            {
                MessageBox.Show("wpisz jakąś litere!", "Nie wpisano litery!");
            }
        }
    }
}
