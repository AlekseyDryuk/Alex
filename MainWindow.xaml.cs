using System;
using System.Windows.Threading;
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

namespace Tetris
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MediaPlayer media = new MediaPlayer();
        MediaPlayer drop = new MediaPlayer();
        DispatcherTimer Timer;
        Board myBoard;

        public MainWindow()
        {
            InitializeComponent();
            Button.Click += new RoutedEventHandler(Button_Click);
            Exit.Click += new RoutedEventHandler(Exit_Click);
        }

        void MainWindow_Initialized(object sender, EventArgs e)
        {
            media.Open(new Uri("C:/Users/Aleksey/Desktop/ITEA/Tetris/Tetris/Gee.mp3", UriKind.RelativeOrAbsolute));
            
            media.Play();
            MainGrid.Visibility = Visibility.Collapsed;
            Score.Visibility = Visibility.Collapsed;
            Lines.Visibility = Visibility.Collapsed;
            NextBlock.Visibility = Visibility.Collapsed;
            
            Timer = new DispatcherTimer();
            Timer.Tick += new EventHandler(GameTick);
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 400);
            GameStart();

        }

        private void GameStart()
        {
            MainGrid.Children.Clear();
            NextBlockGrid.Children.Clear();
            myBoard = new Board(MainGrid, NextBlockGrid);
            Timer.Start();
        }

        public void GamePause()
        {
            Timer.Stop();
            MessageBoxResult result = MessageBox.Show("Game in pause!", "Pause!", MessageBoxButton.OK);
            media.Pause();
            if (result == MessageBoxResult.OK) 
            { Timer.Start(); media.Play(); }
        }

        void GameTick(object sender, EventArgs e)
        {
            Score.Content = myBoard.getScore();
            Lines.Content = myBoard.getLines();
            myBoard.CurrTetraminoMoveDown(NextBlockGrid, drop);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.Left:
                    if (Timer.IsEnabled)
                        myBoard.CurrTetraminoMoveLeft();
                    break;
                case Key.Right:
                    if (Timer.IsEnabled)
                        myBoard.CurrTetraminoMoveRight();
                    break;
                case Key.Down:
                    if (Timer.IsEnabled)
                        myBoard.CurrTetraminoMoveDown(NextBlockGrid, drop);
                    break;
                case Key.Up:
                    if (Timer.IsEnabled)
                        myBoard.CurrTetraminoMoveRotate();
                    break;
                case Key.F2:
                    GameStart();
                    break;
                case Key.F3:
                    {
                        GamePause();
                        break;
                    }

                default:
                    break;
            }
        }

        private void GroupBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Button.Visibility = Visibility.Collapsed;
            MainGrid.Visibility = Visibility.Visible;
            Score.Visibility = Visibility.Visible;
            Lines.Visibility = Visibility.Visible;
            NextBlock.Visibility = Visibility.Visible;
        }

        private void Exit_Click(object Sender, RoutedEventArgs e)
        {
            MainWindow1.Close();
        }
    }
}
