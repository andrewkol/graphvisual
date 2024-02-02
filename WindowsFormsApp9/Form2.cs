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
    public partial class Form2 : Form
    {
        int typeOfGraph, countOfVertex, countOfLines, countOfLoops, maxdegree, plusdegree, minusdegree, typeOfConnection;
        public Form2(int typeOfGraph, int countOfVertex, int countOfLines, int countOfLoops, int maxdegree, int plusdegree, int minusdegree, int typeOfConnection)
        {
            InitializeComponent();
            this.typeOfGraph = typeOfGraph;
            this.countOfVertex = countOfVertex;
            this.countOfLines = countOfLines;
            this.countOfLoops = countOfLoops;
            this.maxdegree = maxdegree;
            this.plusdegree = plusdegree;
            this.minusdegree = minusdegree;
            this.typeOfConnection = typeOfConnection;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (typeOfGraph == 1)
            {
                label3.Text = "Неориентированный";
                label6.Text = "3. Количество ребёр:";
                label12.Text = Convert.ToString(maxdegree);
                if (typeOfConnection == 2)
                    label13.Text = "Связный";
                else
                    label13.Text = "Несвязный";
            }
            else
            {
                label3.Text = "Ориентированный";
                label6.Text = "3. Количество дуг:";
                label12.Text = $"По заходам: {plusdegree}. По исходам: {minusdegree}";
                if(typeOfConnection == 1)
                    label13.Text = "Несвязный";
                else if(typeOfConnection == 2)
                    label13.Text = "Слабо связный";
                else if (typeOfConnection == 3)
                    label13.Text = "Односторонне связный";
                else if (typeOfConnection == 4)
                    label13.Text = "Сильно связный";
            }
            label5.Text = Convert.ToString(countOfVertex);
            label7.Text = Convert.ToString(countOfLines);
            label9.Text = Convert.ToString(countOfLoops);
        }
    }
}
