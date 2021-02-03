using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Matrix
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

			double[][] InputMatA = new double[100][];
			for (int i = 0; i < 100; i++)
				InputMatA[i] = new double[100];
			
			double[][] A = InputMatA;
			int n = InputMatA.GetLength(0);
			double[][] L = new double[n][];
			for (int i = 0; i < n; i++)
			{
				L[i] = new double[n];
			}
			bool isspd = (InputMatA.GetLength(1) == n);
			for (int j = 0; j < n; j++)
			{
				double[] Lrowj = L[j];
				double d = 0.0;
				for (int k = 0; k < j; k++)
				{
					double[] Lrowk = L[k];
					double s = 0.0;
					for (int i = 0; i < k; i++)
					{
						s += Lrowk[i] * Lrowj[i];
					}
					Lrowj[k] = s = (A[j][k] - s) / L[k][k];
					d = d + s * s;
					isspd = isspd & (A[k][j] == A[j][k]);
				}
				d = A[j][j] - d;
				isspd = isspd & (d > 0.0);
				L[j][j] = System.Math.Sqrt(System.Math.Max(d, 0.0));
				for (int k = j + 1; k < n; k++)
				{
					L[j][k] = 0.0;
				}
			}

			double[][] B = new double[100][];
			for (int i = 0; i < 100; i++)
				B[i] = new double[100];

			double[][] X = B;
			int nx = B.GetLength(1);
			// Solve L*Y = B;
			for (int k = 0; k < n; k++)
			{
				for (int j = 0; j < nx; j++)
				{
					X[k][j] /= L[k][k];
				}
				for (int i = k + 1; i < n; i++)
				{
					for (int j = 0; j < nx; j++)
					{
						X[i][j] -= X[k][j] * L[i][k];
					}
				}
			}

			// Solve L'*X = Y;
			for (int k = n - 1; k >= 0; k--)
			{
				for (int j = 0; j < nx; j++)
				{
					X[k][j] /= L[k][k];
				}
				for (int i = 0; i < k; i++)
				{
					for (int j = 0; j < nx; j++)
					{
						X[i][j] -= X[k][j] * L[k][i];
					}
				}
			}
		}
    }
}
