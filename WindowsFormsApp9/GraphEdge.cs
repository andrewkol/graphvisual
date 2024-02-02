using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApp9
{
    class GraphEdge : GraphLine
    {
        
        public GraphEdge(int x1, int y1, int x2, int y2, bool intoself, int radofvert, int radofvert2, int number) :base(x1, y1, x2, y2, radofvert, radofvert2, intoself, number)
        {

        }
        public override Graphics Draw(Graphics graf)
        {
            graf.DrawCurve(new Pen(Color.Black, 2), base.points);
            return graf;
        }
    }
}
