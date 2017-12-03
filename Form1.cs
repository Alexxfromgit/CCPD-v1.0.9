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

        Int32 KOM_OV = 2;
        Int32 CHASTIC = 3;
        Int32 BAZIS = 2;
        Int32 R_ROV = 10;
        Int32 DIAGRAMA = 1;
        double S_F = 0.0001;
        double S_IN_A = 0.000001;
        double S_I = 0.0001;

        double T_C = 25.00;
        double T_K = 299.15;
        double W_SOL = 0.00;
        double VISK = 0.008905;
        double DP = 78.3;
        double DENS = 0.99707;
        double M2 = 0.00;
        double M1 = 18.15;

        double A_DEBYE = 0.00;
        double B_DEBYE = 0.00;
        double A_0A = 3.5;
        double B_DEB = 0.2;
        double RT_F = 59.157;
        double R_A = 3.5;
        double E0_A = 222.00;
        double N_E_B = 1.00;

        string lg_k = "lg(K)";

        Int32[,] ARR = new Int32[5, 10];
        String[,] COMPONENTS = new String[1, 10];
        String[,] BAS_NAME = new String[1, 10];
        String[,] PARTICLES = new String[1, 15];
        double[,] NU_MATRIX = new double[15, 10];
        double[,] LGK = new double[15, 1];
        Int32[,] CHARGE = new Int32[15, 1];
        double[,] CONC = new double[60, 4];
        double[,] DATA = new double[60, 1];

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("CCPD\nversion - 1.0.0.0.2");
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
            //Вывод данных масива ARR
            //Ввод значений из датагрид1 для компонентной матрицы
            //Заполнение рядов из массива COMPONENTS

            for (int i = 0; i < BAZIS; i++)
            {
                //Присвоение из PARTICLES данных в BAS_NAME до значения BAZIS
                for (int j = 0; j < 1; j++)
                {
                    BAS_NAME[j, i] = PARTICLES[j, i];
                }
            }

            dataGridView1.RowCount = KOM_OV;
            dataGridView1.ColumnCount = BAZIS;

            for (int i = 0; i < KOM_OV; i++)
            {
                for (int j = 0; j < BAZIS; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = ARR[i, j].ToString();
                    dataGridView1.Rows[i].HeaderCell.Value = COMPONENTS[0, i];
                    dataGridView1.Columns[j].HeaderCell.Value = BAS_NAME[0, j];
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            T_C = Convert.ToDouble(numericUpDown1.Value);
            T_K = T_C + 273.15;
            label19.Text = Convert.ToString(T_K);
            W_SOL = Convert.ToDouble(textBox9.Text);
            VISK = Convert.ToDouble(textBox10.Text);
            DP = Convert.ToDouble(textBox11.Text);
            DENS = Convert.ToDouble(textBox12.Text);
            M2 = Convert.ToDouble(textBox13.Text);
            M1 = Convert.ToDouble(textBox14.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            A_0A = Convert.ToDouble(textBox15.Text);
            B_DEB = Convert.ToDouble(textBox16.Text);
            R_A = Convert.ToDouble(textBox17.Text);
            E0_A = Convert.ToDouble(textBox18.Text);
            N_E_B = Convert.ToDouble(textBox19.Text);
                        
            B_DEBYE = Math.Sqrt(2529.1171 * DENS / DP / T_K);
            A_DEBYE = B_DEBYE * 36283.167 / DP / T_K;

            RT_F = 8.3142 * T_K / 96487 * Math.Log(10) * 1000;

            label29.Text = Convert.ToString(A_DEBYE);
            label30.Text = Convert.ToString(B_DEBYE);
            label31.Text = Convert.ToString(RT_F);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Вывод значений из датагрид1 в массив ARR

            dataGridView1.RowCount = KOM_OV;
            dataGridView1.ColumnCount = BAZIS;

            for (int i = 0; i < KOM_OV; i++)
            {
                for (int j = 0; j < BAZIS; j++)
                {
                    ARR[i, j] = Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value);
                    //dataGridView2.Rows[i].Cells[j].Value = ARR[i, j].ToString();
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Вывод значений в датагрид2 из массива COMPONENTS

            dataGridView2.RowCount = 1;
            dataGridView2.ColumnCount = KOM_OV;

            for (int j = 0; j < 1; j++)
            {
                for (int i = 0; i < KOM_OV; i++)
                {
                    dataGridView2.Rows[j].Cells[i].Value = COMPONENTS[j, i];
                    //dataGridView1.Rows[i].HeaderCell.Value = Convert.ToString(i + 1);
                    //dataGridView1.Columns[j].HeaderCell.Value = Convert.ToString(j + 1);
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Вывод из датагрид2 в массив COMPONENTS сохранение значений

            dataGridView2.RowCount = 1;
            dataGridView2.ColumnCount = KOM_OV;

            for (int j = 0; j < 1; j++)
            {
                for (int i = 0; i < KOM_OV; i++)
                {
                    COMPONENTS[j, i] = Convert.ToString(dataGridView2.Rows[j].Cells[i].Value);
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            /*
            //ПРОВЕРКА

            dataGridView3.RowCount = 1;
            dataGridView3.ColumnCount = KOM_OV;

            for (int j = 0; j < 1; j++)
            {
                for (int i = 0; i < KOM_OV; i++)
                {
                    dataGridView3.Rows[j].Cells[i].Value = COMPONENTS[j, i];
                    //dataGridView1.Rows[i].HeaderCell.Value = Convert.ToString(i + 1);
                    //dataGridView1.Columns[j].HeaderCell.Value = Convert.ToString(j + 1);
                }
            }
            */

            //Вывод значений в датагрид3 из массива PARTICLES

            dataGridView3.RowCount = 1;
            dataGridView3.ColumnCount = CHASTIC;

            for (int j = 0; j < 1; j++)
            {
                for (int i = 0; i < CHASTIC; i++)
                {
                    dataGridView3.Rows[j].Cells[i].Value = PARTICLES[j, i];
                    //dataGridView1.Rows[i].HeaderCell.Value = Convert.ToString(i + 1);
                    //dataGridView1.Columns[j].HeaderCell.Value = Convert.ToString(j + 1);
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //Вывод из датагрид3 в массив PARTICLES сохранение значений

            dataGridView3.RowCount = 1;
            dataGridView3.ColumnCount = CHASTIC;

            for (int j = 0; j < 1; j++)
            {
                for (int i = 0; i < CHASTIC; i++)
                {
                    PARTICLES[j, i] = Convert.ToString(dataGridView3.Rows[j].Cells[i].Value);
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //Стехиометрическая матрица

            dataGridView4.RowCount = CHASTIC;
            dataGridView4.ColumnCount = BAZIS;

            for (int i = 0; i < BAZIS; i++)
            {
                NU_MATRIX[i, i] = 1;
            }

            for (int j = 0; j < CHASTIC; j++)
            {
                for (int i = 0; i < BAZIS; i++)
                {
                    dataGridView4.Rows[j].Cells[i].Value = NU_MATRIX[j, i].ToString();
                    dataGridView4.Rows[j].HeaderCell.Value = PARTICLES[0, j];
                    dataGridView4.Columns[i].HeaderCell.Value = BAS_NAME[0, i];
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //Вывод значений стехиометрической матрицы из датагрид4 в массив NU_MATRIX

            dataGridView4.RowCount = CHASTIC;
            dataGridView4.ColumnCount = BAZIS;

            for (int i = 0; i < CHASTIC; i++)
            {
                for (int j = 0; j < BAZIS; j++)
                {
                    NU_MATRIX[i, j] = Convert.ToDouble(dataGridView4.Rows[i].Cells[j].Value);
                }
            }
        }
                
        private void button12_Click(object sender, EventArgs e)
        {
            //Константы

            dataGridView5.RowCount = CHASTIC;
            dataGridView5.ColumnCount = 1;

            for (int j = 0; j < CHASTIC; j++)
            {
                for (int i = 0; i < 1; i++)
                {
                    dataGridView5.Rows[j].Cells[i].Value = LGK[j, i].ToString();
                    dataGridView5.Rows[j].HeaderCell.Value = PARTICLES[0, j];
                    dataGridView5.Columns[i].HeaderCell.Value = lg_k;
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //Вывод из грид5 данных в массив LGK

            dataGridView5.RowCount = CHASTIC;
            dataGridView5.ColumnCount = 1;

            for (int i = 0; i < CHASTIC; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    LGK[i, j] = Convert.ToDouble(dataGridView5.Rows[i].Cells[j].Value);
                }
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //Вывод в грид6 из массива CHARGE значений

            dataGridView6.RowCount = CHASTIC;
            dataGridView6.ColumnCount = 1;

            for (int j = 0; j < CHASTIC; j++)
            {
                for (int i = 0; i < 1; i++)
                {
                    dataGridView6.Rows[j].Cells[i].Value = CHARGE[j, i].ToString();
                    dataGridView6.Rows[j].HeaderCell.Value = PARTICLES[0, j];
                    dataGridView6.Columns[i].HeaderCell.Value = "Z";
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            //Вывод из грид6 данных в массив CHARGE

            dataGridView6.RowCount = CHASTIC;
            dataGridView6.ColumnCount = 1;

            for (int i = 0; i < CHASTIC; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    CHARGE[i, j] = Convert.ToInt32(dataGridView6.Rows[i].Cells[j].Value);
                }
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            //Эксперементальные данные. Вывод из масива CONC в грид7

            dataGridView7.RowCount = R_ROV;
            dataGridView7.ColumnCount = KOM_OV;

            for (int j = 0; j < R_ROV; j++)
            {
                for (int i = 0; i < KOM_OV; i++)
                {
                    dataGridView7.Rows[j].Cells[i].Value = CONC[j, i].ToString();
                    dataGridView7.Rows[j].HeaderCell.Value = Convert.ToString(j + 1);
                    dataGridView7.Columns[i].HeaderCell.Value = COMPONENTS[0, i];
                }
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            //Вывод значений Эксперементальных данных из датагрид7 в массив CONC

            dataGridView7.RowCount = R_ROV;
            dataGridView7.ColumnCount = KOM_OV;

            for (int i = 0; i < R_ROV; i++)
            {
                for (int j = 0; j < KOM_OV; j++)
                {
                    CONC[i, j] = Convert.ToDouble(dataGridView7.Rows[i].Cells[j].Value);
                }
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            //Измерения. Вывод из масива DATA в грид8

            dataGridView8.RowCount = R_ROV;
            dataGridView8.ColumnCount = 1;

            for (int j = 0; j < R_ROV; j++)
            {
                for (int i = 0; i < 1; i++)
                {
                    dataGridView8.Rows[j].Cells[i].Value = DATA[j, i].ToString();
                    dataGridView8.Rows[j].HeaderCell.Value = Convert.ToString(j + 1);
                    dataGridView8.Columns[i].HeaderCell.Value = "Data";
                }
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            //Вывод значений Измерений из датагрид8 в массив DATA

            dataGridView8.RowCount = R_ROV;
            dataGridView8.ColumnCount = 1;

            for (int i = 0; i < R_ROV; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    DATA[i, j] = Convert.ToDouble(dataGridView8.Rows[i].Cells[j].Value);
                }
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {

        }
    }
}
