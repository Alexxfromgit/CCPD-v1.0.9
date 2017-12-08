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
