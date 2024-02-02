using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp9
{
    public partial class Form1 : Form
    {
        Graphics graf;
        List<GraphVertex> AllVertex = new List<GraphVertex>();
        List<GraphLine> AllLines = new List<GraphLine>();
        int Entering, startX, startY, startIndex, EnterLine, reachibility;
        int typeOfGraph, countOfVertex, countOfLines, countOfLoops, maxdegree, plusdegree, minusdegree, typeOfConnection;
        List<Int32>[] listOfArrayforDFS;
        bool[] visitedforReachabilityMatrix;
        int[,] reachabilityMatrix;

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
        "Клик левой кнопкой мыши по рабочей области.",
        "Добавление вершины",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information,
        MessageBoxDefaultButton.Button1);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
        "Колач Андрей 2221",
        "Автор",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information,
        MessageBoxDefaultButton.Button1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
        "Левый клик по первой вершине. Она выделится красным цветом.\r\n" +
        "Далее левый клик по второй вершине.",
        "Добавление ребра(дуги)",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information,
        MessageBoxDefaultButton.Button1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
        "Клик левой кнопкой мыши по необходимой вершине.",
        "Удаление вершины",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information,
        MessageBoxDefaultButton.Button1);
        }

        bool intoself;
        public Form1()
        {
            InitializeComponent();
            graf = panel3.CreateGraphics();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(AllVertex.Count > 0)
            {
                reachabilityMatrix = new int[AllVertex.Count, AllVertex.Count];
                listOfArrayforDFS = new List<Int32>[AllVertex.Count];
                for (int i = 0; i < AllVertex.Count; i++)
                {
                    listOfArrayforDFS[i] = AllVertex[i].Output;
                }
                if (radioButton1.Checked)
                {
                    typeOfGraph = 2;
                    MaxDegreeOfVertexForArc();
                    CheckDFSforArc();
                }
                else
                {
                    typeOfGraph = 1;
                    MaxDegreeOfVertexForEdge();
                    CheckDFSforEdge();
                }
                GetLoops();
                countOfVertex = AllVertex.Count;
                countOfLines = AllLines.Count;
                if(typeOfGraph == 1)
                {
                    minusdegree = -1;
                    plusdegree = -1;
                }
                else
                {
                    maxdegree = -1;
                }
                Form2 form2 = new Form2(typeOfGraph, countOfVertex, countOfLines, countOfLoops, maxdegree, plusdegree, minusdegree, typeOfConnection);
                form2.Show();
            }
        }

        private void panel3_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Clear();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Clear();
        }

        private int CheckEntryVertex(int x, int y)
        {
            for(int i = 0; i< AllVertex.Count; i++)
            {
                if (AllVertex[i].Entry(x, y))
                {
                    return i;
                }
            }
            return -1;
        }
        private int CheckEntryLine(int x, int y)
        {
            for (int i = 0; i < AllLines.Count; i++)
            {
                if (AllLines[i].Entry(x, y))
                {
                    return i;
                }
            }
            return -1;
        }
        private void panel3_MouseUp(object sender, MouseEventArgs e)
        {
            Entering = -1;
            EnterLine = -1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clear();
        }
        private void Clear()
        {
            graf.Clear(BackColor);
            AllLines.Clear();
            AllVertex.Clear();
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            intoself = false;
            Entering = CheckEntryVertex(e.X, e.Y);
            EnterLine = CheckEntryLine(e.X, e.Y);
            if (radioButton4.Checked && e.Button == MouseButtons.Left)
            {
                if(AllVertex.Count < 1)
                    AllVertex.Add(new GraphVertex(e.X, e.Y, 0));
                else
                    AllVertex.Add(new GraphVertex(e.X, e.Y, AllVertex[AllVertex.Count-1].Number + 1));
                AllVertex[AllVertex.Count - 1].Draw(graf, AllVertex.Count - 1);
            }
            if (radioButton5.Checked && e.Button == MouseButtons.Right && Entering > -1)
            {
                RemoveVertex(Entering);
            }
            if (radioButton6.Checked && e.Button == MouseButtons.Right && EnterLine> -1)
            {
                RemoveLine(EnterLine);
            }
            if (radioButton2.Checked && radioButton3.Checked && Entering > -1 && e.Button == MouseButtons.Left)
            {
                if (startX == 0 && startY == 0)
                {
                    startX = e.X;
                    startY = e.Y;
                    startIndex = Entering;
                    ReDrawWithHighlightVertex();
                }
                else
                {
                    if (startIndex == Entering)
                        intoself = true;
                    AllLines.Add(new GraphEdge(AllVertex[startIndex].CurrentX - AllVertex[startIndex].Radius / 2, AllVertex[startIndex].CurrentY - AllVertex[startIndex].Radius / 2
                        , AllVertex[Entering].CurrentX - AllVertex[Entering].Radius / 2, AllVertex[Entering].CurrentY - AllVertex[Entering].Radius / 2, intoself, AllVertex[startIndex].Radius / 2, AllVertex[Entering].Radius/2, AllLines.Count));
                    AllLines[AllLines.Count - 1].Draw(graf);
                    if(!intoself)
                    {
                        AllVertex[startIndex].AddOutput = AllVertex[Entering].Number;
                        AllVertex[Entering].AddOutput = AllVertex[startIndex].Number;

                    }
                    else
                    {
                        AllVertex[startIndex].AddOutput = AllVertex[Entering].Number;
                    }
                    startX = 0;
                    startY = 0;
                    startIndex = -1;
                    ReDraw();
                }
            }
            if (radioButton1.Checked && radioButton3.Checked && Entering > -1 && e.Button == MouseButtons.Left)
            {
                if (startX == 0 && startY == 0)
                {
                    startX = e.X;
                    startY = e.Y;
                    startIndex = Entering;
                    ReDrawWithHighlightVertex();
                }
                else
                {
                    if (startIndex == Entering)
                        intoself = true;
                    AllLines.Add(new GraphArc(AllVertex[startIndex].CurrentX - AllVertex[startIndex].Radius / 2, AllVertex[startIndex].CurrentY - AllVertex[startIndex].Radius / 2
                        , AllVertex[Entering].CurrentX - AllVertex[Entering].Radius / 2, AllVertex[Entering].CurrentY - AllVertex[Entering].Radius / 2, intoself, AllVertex[startIndex].Radius / 2, AllVertex[Entering].Radius / 2, AllLines.Count));
                    AllLines[AllLines.Count - 1].Draw(graf);
                    AllVertex[startIndex].AddOutput = AllVertex[Entering].Number;
                    AllVertex[Entering].AddInput = AllVertex[startIndex].Number;
                    startX = 0;
                    startY = 0;
                    startIndex = -1;
                    ReDraw();
                }
            }
        }
        private int GetIndex(int number)
        {
            for(int i = 0; i < AllVertex.Count; i++)
            {
                if (AllVertex[i].Number == number)
                    return i;
            }
            return -1;
        }
        private void RemoveVertex(int NumberofDelete)
        {
            for(int i = 0; i < AllVertex.Count; i++)
            {
                AllVertex[i].RemoveInput = NumberofDelete;
                AllVertex[i].RemoveOutput = NumberofDelete;
            }
            AllVertex.RemoveAt(NumberofDelete);
            ReDraw();
        }
        private void RemoveLine(int NumOfDelete)
        {
            if(radioButton1.Checked)
            {
                AllVertex[CheckEntryVertex(AllLines[NumOfDelete].X2, AllLines[NumOfDelete].Y2)].RemoveInput = CheckEntryVertex(AllLines[NumOfDelete].X1, AllLines[NumOfDelete].Y1);
                AllVertex[CheckEntryVertex(AllLines[NumOfDelete].X1, AllLines[NumOfDelete].Y1)].RemoveOutput = CheckEntryVertex(AllLines[NumOfDelete].X2, AllLines[NumOfDelete].Y2);
            }
            if(radioButton2.Checked)
            {
                try
                {
                    AllVertex[CheckEntryVertex(AllLines[NumOfDelete].X1, AllLines[NumOfDelete].Y1)].RemoveOutput = CheckEntryVertex(AllLines[NumOfDelete].X2, AllLines[NumOfDelete].Y2);
                    AllVertex[CheckEntryVertex(AllLines[NumOfDelete].X2, AllLines[NumOfDelete].Y2)].RemoveOutput = CheckEntryVertex(AllLines[NumOfDelete].X1, AllLines[NumOfDelete].Y1);
                }
                catch
                {

                }
            }
            AllLines.RemoveAt(NumOfDelete);
            ReDraw();
        }
        private bool CheckConnect()
        {
            int iter = 0;
            reachibility = 0;
            for (int i = 1; i < reachabilityMatrix.GetLength(0); i++)
            {
                iter = 0;
                reachibility = 0;
                int v = i;
                int standart = reachabilityMatrix[0,v];
                int h = 0;
                while (v > -1)
                {
                    iter++;
                    if (reachabilityMatrix[h, v] == standart)
                    {
                        if (h != v)
                            reachibility++;
                    }
                    v--;
                    h++;
                }
                if (reachibility == iter)
                    return true;
            }
            for (int i = reachabilityMatrix.GetLength(0) - 2; i > 0; i--)
            {
                iter = 0;
                reachibility = 0;
                int v = i;
                int standart = reachabilityMatrix[reachabilityMatrix.GetLength(0) - 1, v];
                int h = reachabilityMatrix.GetLength(0) - 1;
                while (v <= reachabilityMatrix.GetLength(0) - 1)
                {
                    iter++;
                    if (reachabilityMatrix[h,v] == standart)
                    {
                        if (h != v)
                            reachibility++;
                    }
                    v++;
                    h--;
                }
                if (reachibility == iter)
                    return true;
            }
            return false;
            }
        private void ReDraw()
        {
            graf.Clear(BackColor);
            for (int i = 0; i < AllVertex.Count; i++)
            {
                AllVertex[i].Draw(graf, i);
            }
            for (int i = 0; i < AllLines.Count; i++)
            {
                AllLines[i].Draw(graf);
            }
        }
        private void ReDrawWithHighlightVertex()
        {
            graf.Clear(BackColor);
            for (int i = 0; i < AllVertex.Count; i++)
            {
                if (i == Entering)
                    AllVertex[i].Highlight(graf);
                else
                    AllVertex[i].Draw(graf, i);
            }
            for (int i = 0; i < AllLines.Count; i++)
            {
                AllLines[i].Draw(graf);
            }
        }
        private void MaxDegreeOfVertexForEdge()
        {
            maxdegree = -1;
            for(int i = 0; i < AllVertex.Count; i++)
            {
                if (AllVertex[i].CountOutput > maxdegree)
                    maxdegree = AllVertex[i].CountOutput;
            }
        }
        private void MaxDegreeOfVertexForArc()
        {
            plusdegree = -1;
            minusdegree = -1;
            for (int i = 0; i < AllVertex.Count; i++)
            {
                if (AllVertex[i].CountOutput > plusdegree)
                    plusdegree = AllVertex[i].CountOutput;
                if (AllVertex[i].CountInput > minusdegree)
                    minusdegree = AllVertex[i].CountInput;
            }
        }
        private void GetLoops()
        {
            countOfLoops = 0;
            for(int i = 0; i < AllVertex.Count; i++)
            {
                countOfLoops += AllVertex[i].GetLoops();
            }
            if (typeOfGraph == 1)
                countOfLoops /= 2;
        }
        private void CheckDFSforEdge()
        {
            if (DFS(AllVertex[0].Number) == AllVertex.Count)
                typeOfConnection = 2;
            else
                typeOfConnection = 1;
        }
        private void CheckDFSforArc()
        {
            int[] arcconnect = new int[AllVertex.Count];
            for(int i = 0; i < AllVertex.Count; i++)
            {
                arcconnect[i] = DFS(AllVertex[i].Number);
            }
            int max = 0;
            int maxcount = 0;
            matrix1();
            for (int i = 0; i < arcconnect.Length; i++)
            {
                if (arcconnect[i] > max)
                    max = arcconnect[i];
            }
            for (int i = 0; i < arcconnect.Length; i++)
            {
                if (arcconnect[i] == max)
                    maxcount++;
            }
            if (maxcount == AllVertex.Count)
                typeOfConnection = 4;
            else if (max < AllVertex.Count)
                typeOfConnection = 1;
            else
            {
                if (CheckConnect())
                    typeOfConnection = 3;
                else
                    typeOfConnection = 2;
            }
            for (int i = 0; i < reachabilityMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < reachabilityMatrix.GetLength(1); j++)
                {
                    Console.Write($"{reachabilityMatrix[i, j]} \t");
                }
                Console.WriteLine();
            }
        }
        private void matrix1()
        {
            for (int i = 0; i < AllVertex.Count; i++)
            {
                for (int j = 0; j < AllVertex.Count; j++)
                {
                    DFS(AllVertex[i].Number);
                    if (visitedforReachabilityMatrix[j])
                    {
                        reachabilityMatrix[i, j] = 1;
                    }
                    else
                        reachabilityMatrix[i, j] = 0;
                }
            }
        }
        private int DFS(int s)
        {
            int countofvisited = 0;
            bool[] visited = new bool[AllVertex.Count];
            Stack<int> stack = new Stack<int>();
            visited[s] = true;
            stack.Push(s);
            while (stack.Count != 0)
            {
                s = stack.Pop();
                Console.WriteLine(s);
                foreach (int i in listOfArrayforDFS[s])
                {
                    if (!visited[i])
                    {
                        visited[i] = true;
                        stack.Push(i);
                    }
                }
            }
            visitedforReachabilityMatrix = visited;
            for (int i = 0; i < visited.Length; i++)
            {
                if (visited[i])
                    countofvisited++;
            }
            return countofvisited;
        }
    }
}
