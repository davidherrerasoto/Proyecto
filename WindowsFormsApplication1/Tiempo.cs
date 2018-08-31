using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{

    public partial class Tiempo : Form
    {
        //TomarTiempos tomarTiempos = StaticsFunctions.tomarTiempos();
        public Tiempo()
        {
            InitializeComponent();
            this.CenterToScreen();
            comboBox1.SelectedIndex = 0;
            this.ActiveControl = this.comboBox1;
        }

        private void comboBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
