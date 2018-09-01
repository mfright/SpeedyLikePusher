using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SpeedyLikeSender
{
    public partial class counterSetter : Form
    {
        public frmMain myFM;

        public counterSetter()
        {
            InitializeComponent();

            initialize();
        }

        //初期化
        private void initialize()
        {
            //myFM = (frmMain)this.Owner;

            txtLimitter.Text = myFM.limitter.ToString() ;
        }

        //Cancel
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // save
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                myFM.limitter = int.Parse(txtLimitter.Text);
                
                StreamWriter sw1 = new StreamWriter("limitter.ini", false, System.Text.Encoding.GetEncoding("shift_jis"));
                sw1.WriteLine(myFM.limitter);
                sw1.Close();

                this.Close();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
