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
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new();
        int elapsedTime;
        int matchesFound;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += TimerTick;

            SetUpGame();

        }

        private void TimerTick(object? sender, EventArgs e)
        {
            elapsedTime++;
            timeTextBlock.Text = (elapsedTime / 10f).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " Play again?";
            }
        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>()
            {
                "🐈", "🐈",
                "🐕", "🐕",
                "🐅", "🐅",
                "🦋", "🦋",
                "🐒", "🐒",
                "🦅", "🦅",
                "🐘", "🐘",
                "🐍", "🐍",
            };
            Random rnd = new Random();
            foreach(TextBlock txtBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (txtBlock.Name != "timeTextBlock")
                {
                    txtBlock.Visibility = Visibility.Visible;
                    int index = rnd.Next(animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];
                    txtBlock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                }
            }

            matchesFound = 0;
            elapsedTime = 0; 
            timer.Start();
        }

        bool firstClick = true;
        TextBlock firstAnimal;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock animal = sender as TextBlock;
            if (firstClick) 
            { 
                firstAnimal = animal;
                animal.Visibility = Visibility.Hidden;
                firstClick = false;
            }
            else if (animal.Text == firstAnimal.Text)
            {
                firstClick = true;
                animal.Visibility = Visibility.Hidden;
                matchesFound++;
            }
            else
            {
                firstClick = true;
                firstAnimal.Visibility = Visibility.Visible;
            }
        }

        private void timeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
                SetUpGame();
        }
    }
}
