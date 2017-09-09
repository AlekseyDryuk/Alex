using System;
using System.Windows;
using System.Windows.Media;


namespace Tetris
{
    public class Tetramino
    {
        private Point currPosition;
        private Point[] currShape;
        private Brush currColor;
        private bool rotate;



        Random rand = new Random();
        int randCurr;

        public Tetramino()
        {
            currPosition = new Point(0, 0);
            currColor = Brushes.Transparent;
            currShape = setRandomShape();
        }


        public int getrandCurr()
        {
            return randCurr;
        }

        public Point[] setRandomShape()
        {
            randCurr = rand.Next() % 7;
            switch (randCurr)
            {
                case 0: // |
                    rotate = true;
                    currColor = Brushes.Cyan;

                    return new Point[] {
                        new Point(-1, 0),
                        new Point(0, 0),
                        new Point(1, 0),
                        new Point(2, 0)
                    };
                case 1: // J
                    rotate = true;
                    currColor = Brushes.Blue;
                    return new Point[] {
                        new Point(-1, 1),
                        new Point(-1, 0),
                        new Point(0, 0),
                        new Point(1, 0)
                    };
                case 2: // L
                    rotate = true;
                    currColor = Brushes.Orange;
                    return new Point[] {
                        new Point(1, 1),
                        new Point(1, 0),
                        new Point(0, 0),
                        new Point(-1, 0)
                    };
                case 3:// O
                    rotate = true;
                    currColor = Brushes.Yellow;
                    return new Point[] {
                        new Point(0, 0),
                        new Point(0, 1),
                        new Point(1, 1),
                        new Point(1, 0)
                    };
                case 4: // S
                    rotate = true;
                    currColor = Brushes.Green;
                    return new Point[] {
                        new Point(1, 1),
                        new Point(1, 0),
                        new Point(0, 0),
                        new Point(-1, 0)
                    };
                case 5: // T
                    rotate = true;
                    currColor = Brushes.Purple;
                    return new Point[] {
                        new Point(0, 1),
                        new Point(0, 0),
                        new Point(-1, 0),
                        new Point(1, 0)
                    };
                case 6: // Z
                    rotate = true;
                    currColor = Brushes.Red;
                    return new Point[] {
                        new Point(-1, 1),
                        new Point(0, 1),
                        new Point(0, 0),
                        new Point(1, 0)
                    };

                default:
                    return null;

            }
        }

        public void moveLeft()
        {
            currPosition.X -= 1;
        }

        public void moveRight()
        {
            currPosition.X += 1;
        }

        public void moveDown()
        {
            currPosition.Y += 1;
        }

        public void moveRotate()
        {
            if (rotate)
            {
                for (int i = 0; i < currShape.Length; i++)
                {
                    double x = currShape[i].X;
                    currShape[i].X = currShape[i].Y * -1;
                    currShape[i].Y = x;
                }
            }
        }
    }
}
