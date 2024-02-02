using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WindowsFormsApp9
{
    class GraphArc:GraphLine
    {
        private Pen blackpen;
        public GraphArc(int x1, int y1, int x2, int y2, bool intoself, int radofvert, int radofvert2, int number) : base(x1, y1, x2, y2,radofvert, radofvert2, intoself, number)
        {
            blackpen = new Pen(Color.Black, 2);
            blackpen.CustomEndCap = new AdjustableArrowCap(6, 6);
        }
        public override Graphics Draw(Graphics graf)
        {
            graf.DrawCurve(blackpen, base.points);
            return graf;
        }
    }
}
