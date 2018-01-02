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

        //Функция из VisualBasic
        public double[,] Invers(int n, double[,] b)
        {
            int[,] r = new int[10, 4];
            int l = 0;
            int m = 0;
            int i = 0;
            int j = 0;
            int k = 0;
            int ja = 0;
            int jb = 0;
            int nj = 0;
            double s = 0;
            double amax = 0;
            double p = 0;
            double c = 0;
            double[] cc = new double[10];

            for (i = 0; i < n; i++)
            {
                for (j = 0; j < 3; j++)
                {
                    r[i, j] = 0;
                }

                p = Math.Abs(b[i, i]);

                if (p <= 1E-20)
                {
                    k = 2;
                }
                else if (p <= 1)
                {
                    k = 1;
                }
                else if (p < 9)
                {
                    k = 2;
                }
                else
                {
                    k = 3;
                }

                j = 0;

                switch (k)
                {
                    case 1:
                        s = 4;
                        while (p <= 2)
                        {
                            j += 1;
                            p *= s;
                        }
                        break;
                    case 3:
                        s = 1 / 4;
                        while (p >= 9)
                        {
                            j += 1;
                            p *= s;
                        }
                        break;
                    case 2:
                        break;
                    default:
                        break;
                }

                if (k == 3)
                {
                    nj = -j;
                }
                else
                {
                    nj = j;
                }

                cc[i] = Math.Exp(nj * Math.Log(2));
            }

            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                {
                    b[i, j] = b[i, j] * cc[i] * cc[j];
                }
            }

            for (i = 0; i < n; i++)
            {
                amax = 0;

                for (j = 0; j < n; j++)
                {
                    if (r[j, 2] != 1)
                    {
                        for (k = 0; k < n; k++)
                        {
                            s = Math.Abs(b[j, k]);
                            if ((r[k, 2] != 1) && (amax < s))
                            {
                                ja = j;
                                jb = k;
                                amax = s;
                            }
                        }
                    }
                }

                r[jb, 2] = 1;
                r[i, 0] = ja;
                r[i, 1] = jb;

                if (ja != jb)
                {
                    for (m = 0; m < n; m++)
                    {
                        s = b[ja, m];
                        b[ja, m] = b[jb, m];
                        b[jb, m] = s;
                    }
                }

                s = b[jb, jb];
                b[jb, jb] = 1;

                for (m = 0; m < n; m++)
                {
                    b[jb, m] /= s;
                }

                for (m = 0; m < n; m++)
                {
                    if (m != jb)
                    {
                        c = b[m, jb];
                        b[m, jb] = 0;
                    }

                    for (l = 0; l < n; l++)
                    {
                        b[m, l] -= b[jb, l] * c;
                    }
                }
            }

            for (i = 0; i < n; i++)
            {
                m = n - i + 1;

                if (r[m, 0] != r[m, 1])
                {
                    ja = r[m, 0];
                    jb = r[m, 1];

                    for (k = 0; k < n; k++)
                    {
                        s = b[k, ja];
                        b[k, ja] = b[k, jb];
                        b[k, jb] = s;
                    }
                }
            }

            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                {
                    b[i, j] = b[i, j] * cc[i] * cc[j];
                }
            }

            return b;
        }

        //Функция обратной матрицы из нескольких частей1
        public static Tuple<double[][], int[]> LUPDecomposition(double[][] A)
        {
            int n = A.Length - 1;
            /*
            * pi represents the permutation matrix.  We implement it as an array
            * whose value indicates which column the 1 would appear.  We use it to avoid 
            * dividing by zero or small numbers.
            * */
            int[] pi = new int[n + 1];
            double p = 0;
            int kp = 0;
            int pik = 0;
            int pikp = 0;
            double aki = 0;
            double akpi = 0;

            //Initialize the permutation matrix, will be the identity matrix
            for (int j = 0; j <= n; j++)
            {
                pi[j] = j;
            }

            for (int k = 0; k <= n; k++)
            {
                /*
                * In finding the permutation matrix p that avoids dividing by zero
                * we take a slightly different approach.  For numerical stability
                * We find the element with the largest 
                * absolute value of those in the current first column (column k).  If all elements in
                * the current first column are zero then the matrix is singluar and throw an
                * error.
                * */
                p = 0;
                for (int i = k; i <= n; i++)
                {
                    if (Math.Abs(A[i][k]) > p)
                    {
                        p = Math.Abs(A[i][k]);
                        kp = i;
                    }
                }
                if (p == 0)
                {
                    throw new Exception("singular matrix");
                }
                /*
                * These lines update the pivot array (which represents the pivot matrix)
                * by exchanging pi[k] and pi[kp].
                * */
                pik = pi[k];
                pikp = pi[kp];
                pi[k] = pikp;
                pi[kp] = pik;

                /*
                * Exchange rows k and kpi as determined by the pivot
                * */
                for (int i = 0; i <= n; i++)
                {
                    aki = A[k][i];
                    akpi = A[kp][i];
                    A[k][i] = akpi;
                    A[kp][i] = aki;
                }

                /*
                    * Compute the Schur complement
                    * */
                for (int i = k + 1; i <= n; i++)
                {
                    A[i][k] = A[i][k] / A[k][k];
                    for (int j = k + 1; j <= n; j++)
                    {
                        A[i][j] = A[i][j] - (A[i][k] * A[k][j]);
                    }
                }
            }
            return Tuple.Create(A, pi);
        }
        //Функция обратной матрицы из нескольких частей2
        public static double[] LUPSolve(double[][] LU, int[] pi, double[] b)
        {
            int n = LU.Length - 1;
            double[] x = new double[n + 1];
            double[] y = new double[n + 1];
            double suml = 0;
            double sumu = 0;
            double lij = 0;

            /*
            * Solve for y using formward substitution
            * */
            for (int i = 0; i <= n; i++)
            {
                suml = 0;
                for (int j = 0; j <= i - 1; j++)
                {
                    /*
                    * Since we've taken L and U as a singular matrix as an input
                    * the value for L at index i and j will be 1 when i equals j, not LU[i][j], since
                    * the diagonal values are all 1 for L.
                    * */
                    if (i == j)
                    {
                        lij = 1;
                    }
                    else
                    {
                        lij = LU[i][j];
                    }
                    suml = suml + (lij * y[j]);
                }
                y[i] = b[pi[i]] - suml;
            }
            //Solve for x by using back substitution
            for (int i = n; i >= 0; i--)
            {
                sumu = 0;
                for (int j = i + 1; j <= n; j++)
                {
                    sumu = sumu + (LU[i][j] * x[j]);
                }
                x[i] = (y[i] - sumu) / LU[i][i];
            }
            return x;
        }
        //Функция обратной матрицы из нескольких частей3
        public static double[][] InvertMatrix(double[][] A)
        {
            int n = A.Length;
            //e will represent each column in the identity matrix
            double[] e;
            //x will hold the inverse matrix to be returned
            double[][] x = new double[n][];
            for (int i = 0; i < n; i++)
            {
                x[i] = new double[A[i].Length];
            }
            /*
            * solve will contain the vector solution for the LUP decomposition as we solve
            * for each vector of x.  We will combine the solutions into the double[][] array x.
            * */
            double[] solve;

            //Get the LU matrix and P matrix (as an array)
            Tuple<double[][], int[]> results = LUPDecomposition(A);

            double[][] LU = results.Item1;
            int[] P = results.Item2;

            /*
            * Solve AX = e for each column ei of the identity matrix using LUP decomposition
            * */
            for (int i = 0; i < n; i++)
            {
                e = new double[A[i].Length];
                e[i] = 1;
                solve = LUPSolve(LU, P, e);
                for (int j = 0; j < solve.Length; j++)
                {
                    x[j][i] = solve[j];
                }
            }
            return x;
        }

        //Тестовая функция обратной матрицы
        public static void InvMax(int n, double[,] M, double[,] MMASS)
        {
            int i, j, k, N1;
            double s, t;
            double[,] a = new double[20, 40];

            N1 = 2 * n;

            for (i = 0; i < n; i++)
            {
                for (j = 0; j < N1; j++)
                {
                    if (j < n)
                    {
                        a[i, j] = M[i, j];
                    }
                    else if (j == n + i)
                    {
                        a[i, j] = 1;
                    }
                    else
                    {
                        a[i, j] = 0;
                    }
                }
            }

            for (i = 0; i <= n; i++)
            {
                k = i;
                s = a[i, i];

                for (j = i + 1; j <= n; j++)
                {
                    t = a[j, i];
                    if (Math.Abs(s) < Math.Abs(t))
                    {
                        s = t;
                        k = j;
                    }
                }

                for (j = i; j <= N1; j++)
                {
                    t = a[k, j];
                    a[k, j] = a[i, j];
                    a[i, j] = t / s;
                }

                for (k = 0; k <= n; k++)
                {
                    if (k != i)
                    {
                        for (j = N1; j <= i; j++)
                        {
                            a[k, j] = a[k, j] - a[i, j] * a[k, i];
                        }
                    }
                }
            }            

            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                {
                    MMASS[i, j] = a[i, j + n];
                }
            }            
        }

        Int32 KOM_OV = 2;
        Int32 CHASTIC = 5;
        Int32 BAZIS = 3;
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
        double[,] LN_A = new double[60, 8];
        double[,] CO_BAZIS = new double[60, 8];
        double[] IONIC = new double[60];
        double[,] LN_GAMMA = new double[60, 10];
        double[,] C_EQUIL = new double[60, 10];
        double[] DELTA_LN_A = new double[10];

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("CCPD\nversion - 1.0.7");
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
            //Подготова начальных значений

            double s, s1, s3, sum_delta;
            double ion_1, ion_2, eLn;
            int count = 0;
            eLn = Math.Log(10);

            for (int k = 0; k < R_ROV; k++)
            {
                count = 0;

                for (int j = 0; j < BAZIS; j++)
                {
                    s1 = 0;

                    for (int i = 0; i < KOM_OV; i++)
                    {
                        s1 = s1 + CONC[k, i] * ARR[i, j];
                    }

                    CO_BAZIS[k, j] = s1;

                    if (s1 > 0)
                    {
                        LN_A[k, j] = Math.Log(s1 * 2);
                    }
                    else
                    {
                        LN_A[k, j] = -30;
                        CO_BAZIS[k, j] = 1.0E-12;
                    }
                }

                IONIC[k] = 0;
            }

            double[] G = new double[10];//нет в новой версии 1.0.7

            //цикл по растворам

            for (int k = 0; k < R_ROV; k++)
            {
                ion_1 = 0;
                ion_2 = 10;
            A:;
                s1 = Math.Sqrt(ion_1);
                s3 = (-A_DEBYE * s1 / (1 + A_0A * B_DEBYE * s1) + B_DEB * ion_1);

                for (int i = 0; i < CHASTIC; i++)
                {
                    s = 0;

                    for (int j = 0; j < BAZIS; j++)
                    {
                        s = s + NU_MATRIX[i, j] * LN_A[k, j];
                    }

                    LN_GAMMA[k, i] = eLn * Math.Pow(CHARGE[i, 0], 2) * s3;
                    C_EQUIL[k, i] = Math.Exp(LGK[i, 0]) * eLn + s - LN_GAMMA[k, i];
                }

                s3 = 0;

                for (int L = 0; L < BAZIS; L++)
                {
                    s = 0;

                    for (int i = 0; i < CHASTIC; i++)
                    {
                        s = s + NU_MATRIX[i, L] * C_EQUIL[k, i];
                    }

                    G[L] = CO_BAZIS[k, L] - s;
                    s3 = s3 + Math.Abs(G[L]) / CO_BAZIS[k, L];
                }

                if (s3 < 1E-4)
                {
                    goto B;
                }

                double[,] H = new double[10, 10];//нет в новой версии 1.0.7
                double[,] H0 = new double[10, 10];//нет в новой версии 1.0.7
                double[,] H1 = new double[10, 10];//нет в новой версии 1.0.7

                //создание матрицы H()

                for (int L = 0; L < BAZIS; L++)
                {
                    for (int j = 1; j < BAZIS; j++)
                    {
                        s = 0;

                        for (int i = 0; i < CHASTIC; i++)
                        {
                            s += NU_MATRIX[i, L] * C_EQUIL[k, i] * NU_MATRIX[i, j];
                        }

                        H[L, j] = s;
                        H[j, L] = s;
                        H1[L, j] = s;
                        H1[j, L] = s;
                    }
                }

                H0 = Invers(BAZIS, H);

                //ПРОВЕРКА ОБРАЩЕНИЯ

                s1 = 0; 

                for (int i = 0; i < BAZIS; i++)
                {
                    for (int j = 0; j < BAZIS; j++)
                    {
                        s = 0;

                        for (int L = 0; L < BAZIS; L++)
                        {
                            s = s + H0[i, L] * H1[L, j];
                        }

                        s1 = s1 + Math.Abs(s);
                    }
                }

                textBox20.Text = Convert.ToString(s1);

                //Расчет поправок - произведение H^(-1)*G

                sum_delta = 0;

                for (int j = 0; j < BAZIS; j++)
                {
                    s = 0;

                    for (int L = 0; L < BAZIS; L++)
                    {
                        s += H0[j, L] * G[L];
                    }

                    DELTA_LN_A[j] = s;
                    sum_delta += Math.Abs(s);
                }

                //Критерий выхода по норме поправок

                if (sum_delta < 1E-5)
                {
                    goto B;
                }

                for (int j = 0; j < BAZIS; j++)
                {
                    LN_A[k, j] += DELTA_LN_A[j] / 3;
                }

                count += 1;

                goto A;

            B:;

                s = 0;

                for (int i = 0; i < CHASTIC; i++)
                {
                    s += Math.Pow(CHARGE[i, 0], 2) * C_EQUIL[k, i];
                }

                ion_1 = s / 2;
                IONIC[k] = ion_1;

                if (Math.Abs((ion_2 - ion_1) / ion_1) > 1E-3)
                {
                    ion_2 = ion_1;
                    goto A;
                }
            }

            //Эксперементальные данные. Вывод из масива CONC в грид7
            /*
            dataGridView9.RowCount = R_ROV;
            dataGridView9.ColumnCount = BAZIS;

            for (int j = 0; j < R_ROV; j++)
            {
                for (int i = 0; i < BAZIS; i++)
                {
                    dataGridView9.Rows[j].Cells[i].Value = LN_A[j, i].ToString();
                    //dataGridView9.Rows[j].HeaderCell.Value = Convert.ToString(j + 1);
                    //dataGridView9.Columns[i].HeaderCell.Value = COMPONENTS[0, i];
                }
            }*/
        }

        private void button21_Click(object sender, EventArgs e)
        {
            //TEST BUTTON #21
            //Проверка обращения матрицы

            //Проверочный тестовый массив
            //{{1.00, 7.00, 9.00}, {2.00, 6.00, 2.00}, {4.00, 74.00, 0.00}};
            double[,] MAZ = new double[3,3] { { 1.00, 7.00, 9.00 }, { 2.00, 6.00, 2.00 }, { 4.00, 74.00, 0.00 } };
            //Запись значений из тестового массива для проверки
            double[,] MAZZ = new double[3,3];

            //Проверка работы функции
            MAZZ = Invers(3, MAZ);            
        }
    }
}
