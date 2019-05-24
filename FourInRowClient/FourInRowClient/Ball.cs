using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FourInRowClient
{
    public class Ball
    {
        public Ellipse Circle;
        public double X { get; set; }
        public double Y { get; set; }
        public int XVelocity { get; set; }
        public int YVelocity { get; set; }
        public SolidColorBrush Color { get; set; }



        public Ball(Point p)
        {
            Circle = new Ellipse();
            Circle.Width = 69;
            Circle.Height = 69;
            XVelocity = 20;
            YVelocity = 20;
            X = p.X - Circle.Height / 2;
            Y = p.Y - Circle.Width / 2;
        }

    }
}
