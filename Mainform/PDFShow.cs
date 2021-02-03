using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Provider;

namespace Mainform
{
    public partial class PDFShow : Form
    {
        public PDFShow()
        {
            InitializeComponent();
        }

        public string Node;
        private void PDFShow_Load(object sender, EventArgs e)
        {
            switch (Node)
            {
                case "NodeC":
                case "NodeC1":
                case "NodeC2":
                case "NodeC3":
                case "NodeC4":
                    {
                        webBrowser1.Navigate(Const.Folderstring + @"\Excel\Cons.pdf");
                    }
                    break;

                case "NodeU":
                case "NodeU1":
                case "NodeU2":
                case "NodeU3":
                case "NodeU4":
                    {
                        webBrowser1.Navigate(Const.Folderstring + @"\Excel\ULS.pdf");
                    }
                    break;

                case "NodeS":
                case "NodeS1":
                case "NodeS2":
                case "NodeS3":
                
                    {
                        webBrowser1.Navigate(Const.Folderstring + @"\Excel\SLS.pdf");
                    }
                    break;
            }
            
               
        }
    }
}
