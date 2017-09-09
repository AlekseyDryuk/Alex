using System;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Media;
using System.Windows.Controls;

namespace Tetris
{
    public class Board
    {
        private int Rows;
        private int Cols;
        private int NRows;
        private int NCols;
        private int Score;
        private int LinesFilled;
        private Tetramino currTetramino;
        private Tetramino nextTetramino;
        private Label[,] BlockControls;
        private Label[,] NextBlockControls;
     
        static private Brush NoBrush = Brushes.Transparent;
        static private Brush SilverBrush = Brushes.Gray;            

        public Board(Grid TetrisGrid, Grid NextTetrisGrid)
        {
            Rows = TetrisGrid.RowDefinitions.Count;
            Cols = TetrisGrid.ColumnDefinitions.Count;
            NRows = NextTetrisGrid.RowDefinitions.Count;
            NCols = NextTetrisGrid.ColumnDefinitions.Count;
            Score = 0;
            LinesFilled = 0;
            BlockControls = new Label[Cols, Rows];
            NextBlockControls = new Label[NCols, NRows];
            
            for (int i = 0; i < Cols; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    BlockControls[i, j] = new Label();
                    BlockControls[i, j].Background = NoBrush;
                    BlockControls[i, j].BorderBrush = SilverBrush;
                    BlockControls[i, j].BorderThickness = new Thickness(1, 1, 1, 1);
                    Grid.SetRow(BlockControls[i, j], j);
                    Grid.SetColumn(BlockControls[i, j], i);
                    TetrisGrid.Children.Add(BlockControls[i, j]);
                }
            }

            for (int i = 0; i < NCols; i++)
            {
                for (int j = 0; j < NRows; j++)
                {
                    NextBlockControls[i, j] = new Label();
                    NextBlockControls[i, j].Background = NoBrush;
                    NextBlockControls[i, j].BorderThickness = new Thickness(1, 1, 1, 1);
                    Grid.SetRow(NextBlockControls[i, j], j);
                    Grid.SetColumn(NextBlockControls[i, j], i);
                    NextTetrisGrid.Children.Add(NextBlockControls[i, j]);
                }
            }
            currTetramino = new Tetramino();
            currTetraminoDraw();
            nextTetramino = new Tetramino();
            nextTetraminoDraw();
        }

        public int getScore()
        {
            return Score;
        }

        public void setScore()
        {
            if (currTetramino.getrandCurr() == 0) Score += 12;
            else if (currTetramino.getrandCurr() == 1) Score += 15;
            else if (currTetramino.getrandCurr() == 2) Score += 18;
            else if (currTetramino.getrandCurr() == 3) Score += 21;
            else if (currTetramino.getrandCurr() == 4) Score += 24;
            else if (currTetramino.getrandCurr() == 5) Score += 38;
            else Score += 32;
        }

        public int getLines()
        {
            return LinesFilled;
        }

        private void currTetraminoDraw()
        {  
            Point Position = currTetramino.getcurrPosition();
            Point[] Shape = currTetramino.getcurrShape();
            Brush Color = currTetramino.getcurrColor();

            foreach (Point S in Shape)
            {
                BlockControls[(int)(S.X + Position.X) + ((Cols / 2) - 1),
                    (int)(S.Y + Position.Y) + 2].Background = Color;  
            }
        }

        private void nextTetraminoDraw()
        {
            Point Position = nextTetramino.getcurrPosition();
            Point[] Shape = nextTetramino.getcurrShape();
            Brush Color = nextTetramino.getcurrColor();

            foreach (Point S in Shape)
            {
                NextBlockControls[(int)(S.X + Position.X) + ((NCols / 2+1) - 1),
                    (int)(S.Y + Position.Y) + 2].Background = Color;
            }
        }

        private void currTetraminoErase()
        {
            Point Position = currTetramino.getcurrPosition();
            Point[] Shape = currTetramino.getcurrShape();
            foreach (Point S in Shape)
            {
                BlockControls[(int)(S.X + Position.X) + ((Cols / 2) - 1),
                    (int)(S.Y + Position.Y) + 2].Background = NoBrush;
            }
        }

        public bool CheckOver()
        {
            bool check = false;
            for (int j = 0; j < Cols; j++)
            {
                if (BlockControls[j, 2].Background == currTetramino.getcurrColor())
                {
                   MessageBox.Show("You lose!");
                    check = true;
                   
                }
            }
            return check;
        }

        private void CheckRows()
        {
            bool full = true;
            for (int i = Rows - 1; i > 0; i--)
            {
                full = true;
                for (int j = 0; j < Cols; j++)
                {
                    if (BlockControls[j, i].Background == NoBrush)
                    {
                        full = false;
                    }
                }
                if (full) 
                {
                    RemoveRow(i);
                    Score += 100; 
                    LinesFilled += 1; 
                    i++;
                }
            }
            
        }

        private void RemoveRow(int row)
        {
            for (int i = row; i > 2; i--)
            {
                for (int j = 0; j < Cols; j++)
                {
                    BlockControls[j, i].Background = BlockControls[j, i - 1].Background;
                }
            }
        }

        public void CurrTetraminoMoveLeft() 
        {
            Point Position = currTetramino.getcurrPosition();
            Point[] Shape = currTetramino.getcurrShape();
            bool move = true;
            currTetraminoErase();
            foreach (Point S in Shape)
            {
                if (((int)(S.X + Position.X) + ((Cols / 2) - 1) - 1) < 0)
                {
                    move = false;
                }
                else if (BlockControls[((int)(S.X + Position.X) + ((Cols / 2) - 1) - 1),
                    (int)(S.Y + Position.Y) + 2].Background != NoBrush)
                {

                    move = false;
                }
            }
            if (move)
            {
                currTetramino.moveLeft();
                currTetraminoDraw();
            }
            else
            {
                currTetraminoDraw();
            }
        }

        public void CurrTetraminoMoveRight() 
        {
            Point Position = currTetramino.getcurrPosition();
            Point[] Shape = currTetramino.getcurrShape();
            bool move = true;
            currTetraminoErase();
            foreach (Point S in Shape)
            {
                if (((int)(S.X + Position.X) + ((Cols / 2) - 1) + 1) >= Cols)
                {
                    move = false;
                }
                else if (BlockControls[((int)(S.X + Position.X) + ((Cols / 2) - 1) + 1),
                    (int)(S.Y + Position.Y) + 2].Background != NoBrush)
                {
                    move = false;
                }
            }
            if (move)
            {
                currTetramino.moveRight();
                currTetraminoDraw();
            }
            else
            {
                currTetraminoDraw();
            }
        }
        public void CurrTetraminoMoveDown(Grid NextBlockGrid, MediaPlayer drop) 
        {
            Point Position = currTetramino.getcurrPosition();
            Point[] Shape = currTetramino.getcurrShape();
            Point NPosition = nextTetramino.getcurrPosition();
            Point[] NShape = nextTetramino.getcurrShape();
            bool move = true;
            currTetraminoErase();
            foreach (Point S in Shape)
            {
                if (((int)(S.Y + Position.Y) + 2 + 1) >= Rows)
                {
                    move = false;
                }
                else if (BlockControls[((int)(S.X + Position.X) + ((Cols / 2) - 1)),
                    (int)(S.Y + Position.Y) + 2 + 1].Background != NoBrush)
                {
                    move = false;
                }
            }
            if (move)
            {
                currTetramino.moveDown();
                currTetraminoDraw();
            }
            else
            {
                currTetraminoDraw();
                CheckOver();
                CheckRows();
                drop.Open(new Uri("C:/Users/Aleksey/Desktop/ITEA/Tetris/Tetris/drop.mp3", UriKind.RelativeOrAbsolute));
                drop.Play();
                setScore();
                currTetramino = nextTetramino;

                foreach (Point S in NShape)
                {
                    NextBlockControls[(int)(S.X + NPosition.X) + ((NCols / 2 + 1) - 1),
                        (int)(S.Y + NPosition.Y) + 2].Background = NoBrush;
                }

                nextTetramino = new Tetramino();
                nextTetraminoDraw();  
            }
        }

        public void CurrTetraminoMoveRotate() 
        {
            Point Position = currTetramino.getcurrPosition();
            Point[] S = new Point[4];
            Point[] Shape = currTetramino.getcurrShape();
            bool move = true;
            Shape.CopyTo(S, 0);
            currTetraminoErase();
            for (int i = 0; i < S.Length; i++)
            {
                double x = S[i].X;
                S[i].X = S[i].Y * -1;
                S[i].Y = x;
                if (((int)((S[i].Y + Position.Y) + 2)) >= Rows)
                {
                    move = false;
                }
                else if (((int)(S[i].X + Position.X) + ((Cols / 2) - 1)) < 0)
                {
                    move = false;
                }
                else if (((int)(S[i].X + Position.X) + ((Cols / 2) - 1)) >= Cols)
                {
                    move = false;
                }
                else if (BlockControls[((int)(S[i].X + Position.X) + ((Cols / 2) - 1)),
                    (int)(S[i].Y + Position.Y) + 2].Background != NoBrush)
                {
                    move = false;
                }
            }
            if (move)
            {
                currTetramino.moveRotate();
                currTetraminoDraw();
            }
            else
            {
                currTetraminoDraw();
            }
        }
    }
}
