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

    
    public partial class Cobrar : Form
    {
        
        public Cobrar()
        {
            InitializeComponent();
            button2.DialogResult = DialogResult.OK;
            button1.DialogResult = DialogResult.Cancel;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }else
            {
                calcularCambio();
            }

            // Only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

            StaticsFunctions.manejarEventos(e, this);
        }

        public bool bandera = false;
        private void calcularCambio()
        {
            double total = Convert.ToDouble(textBox1.Text);
            double totalDls = 0;
            double totalPes = 0;
            if (textBox4.Text != "")
                totalDls = Convert.ToDouble(textBox4.Text) * Convert.ToDouble(textBox5.Text);

            if (textBox3.Text != "")
                totalPes = Convert.ToDouble(textBox3.Text);

            double cambio = total - totalPes - totalDls;
            if (cambio <= 0)
            {
                textBox2.Text = Math.Abs(cambio) + "";
                bandera = true;
            }else
            {
                bandera = false;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }else
            {
                calcularCambio();
            }

            // Only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

            StaticsFunctions.manejarEventos(e, this);
        }
    }
}
