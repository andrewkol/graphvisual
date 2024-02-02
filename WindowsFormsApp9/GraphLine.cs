using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApp9
{
    class GraphLine
    {
        protected int startX, startY, endX, endY, radofVert, radofVert2, number;
        protected bool intoself;
        protected Point[] points;

        public GraphLine(int x1, int y1, int x2, int y2, int radofvert, int radofvert2, bool intoself, int number)
        {
            this.startX = x1;
            this.startY = y1;
            this.endX = x2;
            this.endY = y2;
            this.intoself = intoself;
            this.radofVert = radofvert;
            this.radofVert2 = radofvert2;
            this.number = number;
            if (intoself)
            {
                points = new Point[3];
                points[0].X = (int) (radofVert * Math.Cos(2*Math.PI)) + startX;
                points[0].Y = (int)(radofVert * Math.Sin(2 * Math.PI)) + startY;
                points[1].X = -(int)(radofVert * Math.Cos(Math.PI/2)) + startX;
                points[1].Y = -(int)(radofVert * Math.Sin(Math.PI/2)) + startY + 40 ;
                points[2].X = (int)(radofVert * Math.Cos(Math.PI)) + startX;
                points[2].Y = (int)(radofVert * Math.Sin(Math.PI)) +startY;
            }
            else
            {
                points = new Point[2];
                int moduleRaznX = Math.Abs(x2 - x1);
                int moduleRaznY = Math.Abs(y2 - y1);
                if(moduleRaznX >= moduleRaznY)
                {
                    if (endX >= startX)
                    {
                        points[0].X = (int)(radofVert * Math.Cos(2 * Math.PI)) + x1;
                        points[0].Y = (int)(radofVert * Math.Sin(2 * Math.PI)) + y1;
                        points[1].X = (int)(radofVert2 * Math.Cos(Math.PI)) + x2;
                        points[1].Y = (int)(radofVert2 * Math.Sin(Math.PI)) + y2;
                    }
                    else
                    {
                        points[0].X = (int)(radofVert * Math.Cos(Math.PI)) + x1;
                        points[0].Y = (int)(radofVert * Math.Sin(Math.PI)) + y1;
                        points[1].X = (int)(radofVert2 * Math.Cos(2 * Math.PI)) + x2;
                        points[1].Y = (int)(radofVert2 * Math.Sin(2 * Math.PI)) + y2;
                    }
                }
                else
                {
                    if (endY >= startY)
                    {
                        points[0].X = (int)(radofVert * Math.Cos(3 * Math.PI/2)) + x1;
                        points[0].Y = -(int)(radofVert * Math.Sin(3 * Math.PI/2)) + y1;
                        points[1].X = (int)(radofVert2 * Math.Cos(Math.PI / 2)) + x2;
                        points[1].Y = -(int)(radofVert2 * Math.Sin(Math.PI / 2)) + y2;
                    }
                    else
                    {
                        points[0].X = (int)(radofVert * Math.Cos(Math.PI / 2)) + x1;
                        points[0].Y = -(int)(radofVert * Math.Sin(Math.PI / 2)) + y1;
                        points[1].X = (int)(radofVert2 * Math.Cos(3 * Math.PI / 2)) + x2;
                        points[1].Y = -(int)(radofVert2 * Math.Sin(3 * Math.PI / 2)) + y2;
                    }
                }
              
            }
        }
        public int Number { get { return number; } set { number = value; } }
        public int X1 { get { return points[0].X; }}
        public int Y1 { get { return points[0].Y; } }
        public int X2 { get { return points[1].X; } }
        public int Y2 { get { return points[1].Y; } }

        public virtual Graphics Draw(Graphics graf)
        {
            graf.DrawCurve(new Pen(Color.Black, 5), points);
            return graf;
        }
        public bool Entry(int x, int y)
        {
            if ((y - points[0].Y) / (points[1].Y - points[0].Y) == (x - points[0].X) / (points[1].X - points[0].X))
                if (((x > points[0].X) && (x < points[1].X)) || ((x < points[0].X) && (x > points[1].X)))
                {
                    if (((y > points[0].Y) && (y < points[1].Y)) || ((y < points[0].Y) && (y > points[1].Y)))
                    {
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            else
                return false;
        }
    }
}
