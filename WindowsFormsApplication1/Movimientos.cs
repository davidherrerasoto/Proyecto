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
    public partial class Movimientos : Form
    {
        public Movimientos()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void instanceHasBeenClosed(object sender, FormClosedEventArgs e)
        {
            ventas = null;
            agregarCompra = null;
            salidaInventario = null;
            entradaInventario = null;
        }

        Ventas ventas = null;
        private void button1_Click(object sender, EventArgs e)
        {
            if (ventas == null)
            {
                ventas = new Ventas();
                ventas.Show();
                ventas.FormClosed += instanceHasBeenClosed;
            }
            else
            {
                ventas.Focus();
            }
        }

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {
            StaticsFunctions.manejarEventos(e,this);
        }

        private void button2_KeyPress(object sender, KeyPressEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
        }

        private void button4_KeyPress(object sender, KeyPressEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
        }

        private void button3_KeyPress(object sender, KeyPressEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
        }

        AgregarCompra agregarCompra = null;
        private void button2_Click(object sender, EventArgs e)
        {
            if (agregarCompra == null)
            {
                agregarCompra = new AgregarCompra();
                agregarCompra.Show();
                agregarCompra.FormClosed += instanceHasBeenClosed;
            }
            else
            {
                agregarCompra.Focus();
            }
        }

        SalidaInventario salidaInventario = null;
        private void button4_Click(object sender, EventArgs e)
        {
            if (agregarCompra == null)
            {
                salidaInventario = new SalidaInventario();
                salidaInventario.Show();
                salidaInventario.FormClosed += instanceHasBeenClosed;
            }
            else
            {
                salidaInventario.Focus();
            }
        }

        EntradaInventario entradaInventario = new EntradaInventario();
        private void button3_Click(object sender, EventArgs e)
        {
            if (agregarCompra == null)
            {
                entradaInventario = new EntradaInventario();
                entradaInventario.Show();
                entradaInventario.FormClosed += instanceHasBeenClosed;
            }
            else
            {
                entradaInventario.Focus();
            }
        }
    }
}
