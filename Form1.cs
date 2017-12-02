using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CCPD_v1._0._0._0._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        Int32 KOM_OV = 1;
        Int32 CHASTIC = 2;
        Int32 BAZIS = 3;
        Int32 R_ROV = 4;
        Int32 DIAGRAMA = 5;
        double S_F = 0.0001;
        double S_IN_A = 0.00002;
        double S_I = 0.00004;

        double T_C = 25.00;
        double T_K = 299.15;
        double W_SOL = 0.00;
        double VISK = 0.008905;
        double DP = 78.3;
        double DENS = 0.99707;
        double M2 = 0.00;
        double M1 = 18.15;
        

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("CCPD\nversion - 1.0.0.0.1");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            KOM_OV = Convert.ToInt32(textBox1.Text);
            CHASTIC = Convert.ToInt32(textBox2.Text);
            BAZIS = Convert.ToInt32(textBox3.Text);
            R_ROV = Convert.ToInt32(textBox4.Text);
            DIAGRAMA = Convert.ToInt32(textBox5.Text);
            S_F = Convert.ToDouble(textBox6.Text);
            S_IN_A = Convert.ToDouble(textBox7.Text);
            S_I = Convert.ToDouble(textBox8.Text);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            T_K = T_C + 273.15;

            richTextBox1.Text = Convert.ToString(VISK);

            label19.Text = Convert.ToString(T_K);


        }

        private void button3_Click(object sender, EventArgs e)
        {
            T_C = Convert.ToDouble(numericUpDown1.Value);
            label19.Text = Convert.ToString(T_K);
            W_SOL = Convert.ToDouble(textBox9.Text);
            VISK = Convert.ToDouble(textBox10.Text);
            DP = Convert.ToDouble(textBox11.Text);
            DENS = Convert.ToDouble(textBox12.Text);
            M2 = Convert.ToDouble(textBox13.Text);
            M1 = Convert.ToDouble(textBox14.Text);

        }
    }
}
