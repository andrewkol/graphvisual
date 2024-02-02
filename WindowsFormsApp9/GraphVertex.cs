using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApp9
{
    class GraphVertex
    {
        private int currentX, currentY, radius, number;
        private Rectangle rect;
        private StringFormat f;
        private List<int> output;
        private List<int> input;
        private List<int> ListLines;
        public GraphVertex(int X, int Y, int number)
        {
            this.currentX = X;
            this.currentY = Y;
            this.number = number;
            radius = 20;
            rect = new Rectangle(currentX - radius, currentY - radius, radius, radius);
            f = new StringFormat();
            f.Alignment = StringAlignment.Center;
            f.LineAlignment = StringAlignment.Center;
            output = new List<int>();
            input = new List<int>();
            ListLines = new List<int>();
        }
        public int CurrentX { get { return currentX; } }
        public int Number { get { return number; } set { number = value; } }
        public int CurrentY { get { return currentY; } }
        public int Radius { get { return radius; } }
        public int AddOutput { set { output.Add(value); } }
        public int CountOutput { get { return output.Count; } }
        public int AddInput { set { input.Add(value); } }
        public int CountInput { get { return input.Count; } }
        public List<int> Input { get { return input; } }
        public List<int> Output { get { return output; } }
        public int RemoveOutput { set { if(output.Contains(value)) output.Remove(value); } }
        public int RemoveInput { set { if (input.Contains(value)) input.Remove(value); } }
        public int AddListLines { set { ListLines.Add(value); } }
        public int CountListLines{ get { return ListLines.Count; } }
        public List<int> _ListLines { get { return ListLines; } }
        public int RemoveListLines { set { if (ListLines.Contains(value)) ListLines.Remove(value); } }


        public Graphics Draw(Graphics graf, int i)
        {
            graf.DrawEllipse(new Pen(Color.Black), rect);
            graf.DrawString(Convert.ToString(i), new Font("Arial", 12), new SolidBrush(Color.Black), rect, f);
            return graf;
        }
        public bool Entry(int x, int y)
        {
            double s = Math.Sqrt(Math.Pow(x - currentX, 2) + Math.Pow(y - currentY, 2));
            if (s <= radius)
                return true;
            else
                return false;
        }
        public Graphics Highlight(Graphics graf)
        {
            graf.DrawEllipse(new Pen(Color.Red), rect);
            graf.DrawString(Convert.ToString(number), new Font("Arial", 12), new SolidBrush(Color.Red), rect, f);
            return graf;
        }
        public int GetLoops()
        {
            int count1 = 0;
            for(int i = 0; i < output.Count; i++)
            {
                if (number == output[i])
                    count1++;
            }
            return count1;
        }
    }
}
