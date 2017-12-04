        public double Invers(int n, double[,] b)
        {
            int[,] r = new int[20, 3];
            int l, m, i, j, k, ja, jb, nj;
            double s, c, amax, p;
            double[] cc = new double[20];
            
            void Scale(){                
                for (i = 1; i <= n; i++)
                {
                    for (j = 1; j<= n; j++)
                    {
                        b[i, j] *= cc[i] * cc[j];                        
                    }
                }
            }

            for (i = 1; i < n; i++)
            {
                for (j = 1; j<=3; j++)
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
                else if (p <= 9)
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

            Scale();

            for (i = 1; i <= n; i++)
            {
                amax = 0.00;

                for (j = 1; j <= n; j++)
                {
                    if (r[j, 3] != 1)
                    {
                        for (k = 1; k <= n; k++)
                        {
                            s = Math.Abs(b[j, k]);
                            if ((r[k, 3] != 1) && (amax < s))
                            {
                                ja = j;
                                jb = k;
                                amax = s;
                            }
                        }
                    }
                }

                if (amax < 1E-70)
                {
                    goto A;
                }

                r[jb, 3] = 1;
                r[i, 1] = ja;
                r[i, 2] = jb;

                if (ja != jb)
                {
                    for (m = 1; m <= n; m++)
                    {
                        s = b[ja, m];
                        b[ja, m] = b[jb, m];
                        b[jb, m] = s;
                    }
                }

                s = b[jb, jb];
                b[jb, jb] = 1;

                for (m = 1; m <= n; m++)
                {
                    b[jb, m] /= s;
                }

                for (m = 1; m <= n; m++)
                {
                    if (m != jb)
                    {
                        c = b[m, jb];
                        b[m, jb] = 0;
                    }

                    for (l = 1; l <= n; l++)
                    {
                        b[m, l] -= b[jb, l] * c;
                    }
                }
            }

            for (i = 1; i <= n; i++)
            {
                m = n - i + 1;
                if (r[m, 1] != r[m, 2])
                {
                    ja = r[m, 1];
                    jb = r[m, 2];

                    for (k = 1; k <= n; k++)
                    {
                        s = b[k, ja];
                        b[k, ja] = b[k, jb];
                        b[k, jb] = s;
                    }
                }
            }

            Scale();
            
        A:;

            return 0;
        }
