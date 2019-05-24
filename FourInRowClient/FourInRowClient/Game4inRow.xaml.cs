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
using System.Windows.Shapes;

namespace FourInRowClient
{
    /// <summary>
    /// Interaction logic for Game4inRow.xaml
    /// </summary>
    public partial class Game4inRow : Window
    {
        SolidColorBrush redcolor = new SolidColorBrush(Color.FromRgb(255, 0, 0));
        SolidColorBrush yellowcolor = new SolidColorBrush(Color.FromRgb(255, 255, 0));
        private int[,] GameBoard;
        private int clr = 0;
        private int pos = 0;
        private int[] lastball = new int[2];
        public bool my_turn = true;

        public Game4inRow()
        {
            GameBoard = new int[7, 6];
            InitializeComponent();
            startGameBord();
        }

        public void startGameBord()
        {
            int i = 0, j = 0;
            for (i = 0; i < 7; i++)
            {
                for (j = 0; j < 6; j++)
                {
                    GameBoard[i, j] = 0;
                }
            }
        }

        int getColumn(double x)
        {
            if (x < 35)
                return 0;
            else if (x < 104)
                return 1;
            else if (x < 174)
                return 2;
            else if (x < 244)
                return 3;
            else if (x < 314)
                return 4;
            else if (x < 384)
                return 5;
            else
                return 6;

        }

        int getrow(Ball ball, int column)
        {
            int i = 5;
            Console.Write(column);
            while (i > -1 && GameBoard[column, i] != 0)
            {
                i--;
            }
            if (i < 0)
            {
                return -1;
            }
            if (ball.Circle.Fill == yellowcolor)
                GameBoard[column, i] = 1;
            else
                GameBoard[column, i] = 2;
            return i;
        }

        int GetPos(Ball ball)
        {
            int x = getColumn(ball.X);
            int y = getrow(ball, x);
            lastball[0] = x;
            lastball[1] = y;
            switch (y)
            {
                case 0:
                    y = 1;
                    break;
                case 1:
                    y = 70;
                    break;
                case 2:
                    y = 140;
                    break;
                case 3:
                    y = 210;
                    break;
                case 4:
                    y = 280;
                    break;
                case 5:
                    y = 350;
                    break;
                default:
                    y = -1;
                    break;
            }
            return y;
        }

        private bool Tie()
        {
            int i = 0, j = 0;
            for (i = 0; i < 7; i++)
            {
                if (GameBoard[i, j] == 0)
                    return false;
            }
            return true;
        }

        private Point travel_lift_right(int color)
        {
            int i = 1;
            Point p = new Point();
            while ((lastball[0] - i) > -1 && GameBoard[lastball[0] - i, lastball[1]] == color)//lift
            {
                lastball[0]--;
            }
            p.X = lastball[0];
            p.Y = lastball[1];

            return p;
        }

        private Point travel_up(int color)
        {
            int i = 1;
            Point p = new Point();
            int[] maxpos = new int[4];
            while (((lastball[1] - i) > -1 && (lastball[0] + i) < 7) && GameBoard[lastball[0] + 1, lastball[1] - 1] == color)//up-right
            {
                lastball[0]++;
                lastball[1]--;
            }
            maxpos[0] = lastball[0];//שומר שורה גבוהה
            maxpos[1] = lastball[1];
            while (((lastball[0] - i) > -1 && (lastball[1] - i) > -1) && GameBoard[lastball[0] - 1, lastball[1] - 1] == color)//up-lift
            {
                lastball[0]--;
                lastball[1]--;
            }
            maxpos[2] = lastball[0];
            maxpos[3] = lastball[1];
            if (maxpos[1] > maxpos[3])
            {
                p.X = maxpos[0];
                p.Y = maxpos[1];
            }
            else
            {
                p.X = maxpos[2];
                p.Y = maxpos[3];
            }
            return p;
        }

        private int checkWinner()
        {
            Point p1 = new Point();
            int color = GameBoard[lastball[0], lastball[1]];
            p1 = travel_lift_right(color);
            lastball[0] = (int)p1.X;
            lastball[1] = (int)p1.Y;
            int i = 1;
            int[] cnt = new int[4];
            for (int q = 0; q < 2; q++)
            {
                while ((lastball[1] + i) < 6 && GameBoard[lastball[0], lastball[1] + i] == color)//down
                {
                    i++;
                    if (i == 4)
                    {
                        return color;
                    }
                }
                i = 1;
                while ((lastball[0] + i) < 7 && GameBoard[lastball[0] + i, lastball[1]] == color)//right
                {
                    i++;
                    if (i == 4)
                    {
                        return color;
                    }
                }
                i = 1;
                while ((lastball[0] - i) > -1 && GameBoard[lastball[0] - i, lastball[1]] == color)//lift
                {
                    i++;
                    if (i == 4)
                    {
                        return color;
                    }
                }
                i = 1;
                while (((lastball[0] - i) > -1 && (lastball[1] - i) > -1) && GameBoard[lastball[0] - i, lastball[1] - i] == color)//up-lift
                {
                    i++;
                    if (i == 4)
                    {
                        return color;
                    }
                }
                i = 1;
                while (((lastball[1] - i) > -1 && (lastball[0] + i) < 7) && GameBoard[lastball[0] + i, lastball[1] - i] == color)//up-right
                {
                    i++;
                    if (i == 4)
                    {
                        return color;
                    }
                }
                i = 1;
                while (((lastball[1] + i) < 6 && (lastball[0] + i) < 7) && GameBoard[lastball[0] + i, lastball[1] + i] == color)//down-right
                {
                    i++;
                    if (i == 4)
                    {
                        return color;
                    }
                }
                i = 1;
                while (((lastball[1] + i) < 6 && (lastball[0] - i) > -1) && GameBoard[lastball[0] - i, lastball[1] + i] == color)//down-lift
                {
                    i++;
                    if (i == 4)
                    {
                        return color;
                    }
                }
                p1 = travel_up(color);
                lastball[0] = (int)p1.X;
                lastball[1] = (int)p1.Y;
            }
            return 0;
        }


        public delegate void SendColNumDelegate(double colNum);
        public event SendColNumDelegate newStepInTheGame;

        public delegate void GotARowDelegate();
        public event GotARowDelegate gotARow;


        private void myCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (my_turn)
            {
                Point p = e.GetPosition(myCanvas);
                Ball newBall = new Ball(p);
                int winner;
                if (clr == 0) { clr = 1; newBall.Circle.Fill = redcolor; }
                else { clr = 0; newBall.Circle.Fill = yellowcolor; }
                //newBall.Circle.Fill = yellowcolor;
                pos = GetPos(newBall);
                if (pos != -1)
                {
                    Canvas.SetTop(newBall.Circle, newBall.Y);
                    Canvas.SetLeft(newBall.Circle, newBall.X);
                    myCanvas.Children.Add(newBall.Circle);
                    ThreadPool.QueueUserWorkItem(MoveBall, newBall);
                    newStepInTheGame(p.X);
                    winner = checkWinner();
                    if (winner == 1)
                    {
                        my_turn = false;
                        MessageBox.Show("Yellow Player WINS, You Won!", "Finish");   
                        gotARow();
                    }
                    else if (winner == 2)
                    {
                        my_turn = false;
                        MessageBox.Show("Red Player WINS, You won!", "Finish");
                        gotARow();
                    }
                    if (Tie() == true)
                    {
                        MessageBox.Show("Its a tie!", "Finish");
                    }
                }
            }
            my_turn = false;
        }


        public void updateBallInTheBoard(double col)
        {
            Point p = new Point(col, 5);
            Ball newBall = new Ball(p);
            if (clr == 0) { clr = 1; newBall.Circle.Fill = redcolor; }
            else { clr = 0; newBall.Circle.Fill = yellowcolor; }
            //newBall.Circle.Fill = redcolor;      
            pos = GetPos(newBall);
            //int winner;
            if (pos != -1)
            {
                Canvas.SetTop(newBall.Circle, newBall.Y);
                Canvas.SetLeft(newBall.Circle, newBall.X);
                myCanvas.Children.Add(newBall.Circle);
                ThreadPool.QueueUserWorkItem(MoveBall, newBall);
                /*winner = checkWinner();
                if (winner == 1)
                {
                    MessageBox.Show("Yellow Player WINS!", "Finish");
                }
                else if (winner == 2)
                    MessageBox.Show("Red Player WINS!", "Finish");

                if (Tie() == true)
                {
                    MessageBox.Show("Its a tie!", "Finish");
                }*/
            }
            my_turn = true;
        }

        public void DispalyTheWinner(string WinnerName)
        {
            my_turn = false;
            MessageBox.Show("Sorry, Your rival: " + WinnerName + " won!!");
        }

        void MoveBall(object obj)
        {
            Ball ball = obj as Ball;
            while (ball.Y != pos)
            {
                this.Dispatcher.Invoke((Action)(() =>
                {
                    if (ball.X < 35)
                    {
                        ball.X = 0;
                        ball.Y += ball.YVelocity;
                        if (ball.Y + ball.YVelocity > pos)
                        {
                            ball.Y = pos;
                        }

                    }
                    else if (ball.X < 104)
                    {
                        ball.X = 70;
                        ball.Y += ball.YVelocity;
                        if (ball.Y + ball.YVelocity > pos)
                        {
                            ball.Y = pos;
                        }
                    }
                    else if (ball.X < 174)
                    {
                        ball.X = 140;
                        ball.Y += ball.YVelocity;
                        if (ball.Y + ball.YVelocity > pos)
                        {
                            ball.Y = pos;
                        }
                    }
                    else if (ball.X < 244)
                    {
                        ball.X = 210;
                        ball.Y += ball.YVelocity;
                        if (ball.Y + ball.YVelocity > pos)
                        {
                            ball.Y = pos;
                        }
                    }
                    else if (ball.X < 314)
                    {
                        ball.X = 280;
                        ball.Y += ball.YVelocity;
                        if (ball.Y + ball.YVelocity > pos)
                        {
                            ball.Y = pos;
                        }
                    }
                    else if (ball.X < 384)
                    {
                        ball.X = 350;
                        ball.Y += ball.YVelocity;
                        if (ball.Y + ball.YVelocity > pos)
                        {
                            ball.Y = pos;
                        }
                    }
                    else
                    {
                        ball.X = 420;
                        ball.Y += ball.YVelocity;
                        if (ball.Y + ball.YVelocity > pos)
                        {
                            ball.Y = pos;
                        }
                    }
                    Canvas.SetTop(ball.Circle, ball.Y);
                    Canvas.SetLeft(ball.Circle, ball.X);
                }));
                Thread.Sleep(20);

            }
        }


        public delegate void CloseGameDelegate();
        public event CloseGameDelegate closeGame;
        private void Window_Closed(object sender, EventArgs e)
        {
            closeGame();
        }
    }
}
