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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            /*this.button1.Location = new Point(100, 50);
            this.button2.Location = new Point(this.button1.Location.X + this.Size.Width / 3 + 10, this.button1.Location.Y);
            this.button3.Location = new Point(100, (this.Height / 2 + 10));
            this.button4.Location = new Point(this.button3.Location.X + this.Size.Width / 3 + 10, this.button3.Location.Y);

            this.button1.SetBounds(this.button1.Location.X, this.button1.Location.Y, this.Size.Width / 3, this.Size.Height / 3);
            this.button2.SetBounds(this.button2.Location.X, this.button2.Location.Y, this.Size.Width / 3, this.Size.Height / 3);
            this.button3.SetBounds(this.button3.Location.X, this.button3.Location.Y, this.Size.Width / 3, this.Size.Height / 3);
            this.button4.SetBounds(this.button4.Location.X, this.button4.Location.Y, this.Size.Width / 3, this.Size.Height / 3);
           */ this.CenterToScreen();
        }

        AgregarProducto agregarProducto = null;
        private void button2_Click(object sender, EventArgs e)
        {
            if (agregarProducto == null)
            {
                agregarProducto = new AgregarProducto();
                agregarProducto.Show();
                agregarProducto.FormClosed += instanceHasBeenClosed;
                PBarForm pb = new PBarForm();
                /*pb.Show();
                pb.iniciar();*/
            }
            else
            {
                agregarProducto.Focus();
            }
        }

        private void instanceHasBeenClosed(object sender, FormClosedEventArgs e)
        {
            agregarProducto = null;
            agregarCliente = null;
            agregarAgente = null;
            agregarServicio = null;
            agregarProveedor = null;
            agregarListaPreciosEstilista = null;
        }

        AgregarCliente agregarCliente = null;
        private void button1_Click(object sender, EventArgs e)
        {
            if (agregarCliente == null)
            {
                agregarCliente = new AgregarCliente();
                agregarCliente.Show();
                agregarCliente.FormClosed += instanceHasBeenClosed;
                PBarForm pb = new PBarForm();
                /*pb.Show();
                pb.iniciar();*/
            }
            else
            {
                agregarCliente.Focus();
            }
        }

        private void button2_KeyPress(object sender, KeyPressEventArgs e)
        {
            StaticsFunctions.manejarEventos(e,this);
        }

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
        }

        private void button3_KeyPress(object sender, KeyPressEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
        }

        private void button4_KeyPress(object sender, KeyPressEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
        }

        AgregarAgente agregarAgente = null;
        private void button5_Click(object sender, EventArgs e)
        {
            if (agregarAgente == null)
            {
                agregarAgente = new AgregarAgente();
                agregarAgente.Show();
                agregarAgente.FormClosed += instanceHasBeenClosed;
                PBarForm pb = new PBarForm();
                /*pb.Show();
                pb.iniciar();*/
            }
            else
            {
                agregarAgente.Focus();
            }
        }

        private void button5_KeyPress(object sender, KeyPressEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
        }

        AgregarProveedor agregarProveedor = null;
        private void button4_Click(object sender, EventArgs e)
        {

            if (agregarProveedor == null)
            {
                agregarProveedor = new AgregarProveedor();
                agregarProveedor.Show();
                agregarProveedor.FormClosed += instanceHasBeenClosed;
                PBarForm pb = new PBarForm();
                /*pb.Show();
                pb.iniciar();*/
            }
            else
            {
                agregarProveedor.Focus();
            }
        }

        AgregarListaPreciosEstilista agregarListaPreciosEstilista = null;
        private void button3_Click(object sender, EventArgs e)
        {
            if (agregarListaPreciosEstilista == null)
            {
                agregarListaPreciosEstilista = new AgregarListaPreciosEstilista();
                agregarListaPreciosEstilista.Show();
                agregarListaPreciosEstilista.FormClosed += instanceHasBeenClosed;
                PBarForm pb = new PBarForm();
                /*pb.Show();
                pb.iniciar();*/
            }
            else
            {
                agregarListaPreciosEstilista.Focus();
            }
        }

        AgregarServicio agregarServicio = null;
        private void button6_Click(object sender, EventArgs e)
        {
            if (agregarServicio == null)
            {
                agregarServicio = new AgregarServicio();
                agregarServicio.Show();
                agregarServicio.FormClosed += instanceHasBeenClosed;
                PBarForm pb = new PBarForm();
                /*pb.Show();
                pb.iniciar();*/
            }
            else
            {
                agregarServicio.Focus();
            }
        }
    }
}
