using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        private string[] words = { "kabanos", "ala ma kota", "komputer jest przyszłością", 
            "dominik ma małego", "adiomi games", "kto się lubi ten się czubi" };
        private char[] currentlyPass;
        private string pass;
        private int mistake = 0;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            StartGame();
        }

        private void StartGame()
        {
            mistake = 0;
            Uri sourceImg = new Uri($"Resources/Images/blank.png", UriKind.Relative);
            img.Source = new BitmapImage(sourceImg);

            pass = RandomWord();
            passTxt.Content = pass;

            currentlyPass = new char[pass.Length];
            for (int i = 0; i < currentlyPass.Length; i++)
            {
                if (pass[i] != ' ') currentlyPass[i] = '_';
                else currentlyPass[i] = ' ';
            }

            RefreshPasswordAndStickman();

            var btn_list = parent.Children.OfType<Button>().ToList();
            foreach (var btn in btn_list) btn.IsEnabled = true;
            
        }

        private string RandomWord()
        {
            var rand = new Random();
            return words[rand.Next(0, words.Length)].ToString();
        }

        private void RefreshPasswordAndStickman()
        {
            passTxt.Content = null;
            string word = "";
            for(int index = 0; index < pass.Length; index++)
            {
                passTxt.Content += $" {currentlyPass[index]}";
                word += $"{currentlyPass[index]}";             
            }

            if (word == pass)
            {
                MessageBox.Show($"Hasło to: {pass}", "Świetnie! Udało ci się odgadnąć hasło!");
                StartGame();
            }

            bool lose = mistake < 10 ? true : false;
            if (lose)
            {
                Uri sourceImg = new Uri($"Resources/Images/stickman{mistake}.png", UriKind.Relative);
                img.Source = new BitmapImage(sourceImg);
            }
            else
            {
                Uri sourceImg = new Uri($"Resources/Images/stickman10.png", UriKind.Relative);
                MessageBox.Show("przegrałeś życie, klinknij ok, aby zacząc od nowa.", "Przegrana!");
                StartGame();
            }
        }

        private void CheckLetter(object sender, RoutedEventArgs e)
        {
            var letter = Convert.ToChar((sender as Button).Content.ToString()[0]);

            bool succes = false;
            for (int i = 0; i < pass.Length; i++)
            {
                if (letter == pass[i] && currentlyPass[i] != letter && currentlyPass[i] == '_')
                {
                    currentlyPass[i] = letter;
                    succes = true;
                }
            }

            if (!succes)
            {
                //stickman refresh mistake
                mistake++;
            }

            //call to refresh data
            RefreshPasswordAndStickman();
            (sender as Button).IsEnabled = false;
        }

        private void RestartGame(object sender, RoutedEventArgs e) => StartGame();
    }
}
