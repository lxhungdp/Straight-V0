using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Classes;
using Provider;

namespace Mainform
{
    public partial class CContent : Form
    {
        public CContent()
        {
            InitializeComponent();
        }

        private void CContent_Load(object sender, EventArgs e)
        {
            pictureBox1.Load(Const.Folderstring + @"\Picture\fl.PNG");
        }
    }
}
