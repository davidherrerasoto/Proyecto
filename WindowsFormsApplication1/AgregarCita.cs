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
    public partial class AgregarCita : Form
    {
        TomarClientes tc = StaticsFunctions.tomarClientes();
        TomarProductos tp = StaticsFunctions.tomarServicios();
        Producto pr = null;
        Cliente cl = null;
        List<Servicio> prods = new List<Servicio>();
        List<Button> clientes = null;
        List<Button> productos = null;
        public AgregarCita()
        {
            InitializeComponent();
            this.CenterToScreen();
            agregarClientes();
            agregarServicios();
        }

        private void agregarClientes()
        {
            clientes = new List<Button>();
            int x = 10, y = 10;
            if (tc.clientes != null)
            {
                for (int i = 0; i < tc.clientes.Count; i++)
                {

                    Button button = new Button();
                    button.BackColor = Color.Black;
                    button.Location = new Point(x + (100 * i), y);
                    button.Size = new Size(80, 80);
                    button.Font = new Font(button.Font.Name, 12,
                        button.Font.Style, button.Font.Unit);
                    button.ForeColor = Color.White;
                    button.Click += showIndex_Click;
                    button.Text = tc.clientes.ElementAt(i).denCom;
                    panel1.Controls.Add(button);
                    clientes.Add(button);
                }
            }

        }

        private void showIndex_Click(object sender, EventArgs e)
        {
          
            var button = sender as Button;
            var index = clientes.IndexOf(button);
            cl = tc.clientes.ElementAt(index);
            textBox1.Text = cl.denCom;
        }

        private void agregarServicios()
        {
            productos = new List<Button>();
            int x = 60, y = 10;
            for (int i = 0; i < tp.productos.Count; i++)
            {
                Button button = new Button();
                button.BackColor = Color.Black;
                button.Location = new Point(x + (210 * (i % 3)), y + (190 * (i / 3)));
                button.Size = new Size(180, 180);
                button.Font = new Font(button.Font.Name, 16,
                    button.Font.Style, button.Font.Unit);
                button.ForeColor = Color.White;
                button.Click += showIndex_Click2;
                button.Text = tp.productos.ElementAt(i).nombre;
                panel2.Controls.Add(button);
                productos.Add(button);
            }

        }

        private void showIndex_Click2(object sender, EventArgs e)
        {
            var button = sender as Button;
            var index = productos.IndexOf(button);
            pr = tp.productos.ElementAt(index);
            agregarServicio();
        }

        private void agregarServicio()
        {
            String tiempo = seleccionarTiempo();
            Servicio s = new Servicio();
            s.producto = pr;
            s.tiempo = tiempo;
            prods.Add(s);
            reiniciarGridView();
        }

        private void reiniciarGridView()
        {
            var list = new BindingList<GVCProductos>(mandarClientesGVC(prods));
            dataGridView1.DataSource = list;
        }

        private List<GVCProductos> mandarClientesGVC(List<Servicio> servicios)
        {
            List<GVCProductos> lista = new List<GVCProductos>();
            for (int i = 0; i < prods.Count; i++) {
                GVCProductos prs = new GVCProductos();
                prs.Nombre = servicios.ElementAt(i).producto.nombre;
                prs.Precio = servicios.ElementAt(i).producto.precio;
                prs.Tiempo = servicios.ElementAt(i).tiempo;
                if (!prs.Tiempo.Equals("N"))
                    lista.Add(prs);
            }
            return lista;
        }

        private String seleccionarTiempo()
        {
            Tiempo tiempo = new Tiempo();
            if (tiempo.ShowDialog() == DialogResult.OK)
            {
                if (tiempo.comboBox1.SelectedIndex!=0)
                {
                    //MessageBox.Show(tiempo.comboBox1.Text);
                    return tiempo.comboBox1.Text;
                }else
                {
                    return "N";
                }
            }
            return "N";
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text.Length == 3)
                textBox8.Focus();
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (textBox8.Text.Length == 3)
                textBox9.Focus();
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            StaticsFunctions.manejarEventos(e, this);
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if (textBox8.Text.Length == 3)
                textBox9.Focus();
            StaticsFunctions.manejarEventos(e, this);
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            StaticsFunctions.manejarEventos(e, this);
        }

        private void textBox9_KeyUp(object sender, KeyEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
            if (e.KeyCode == Keys.Enter)
            {
                tc = StaticsFunctions.buscarClientesPorTelefono(mandarTelefono());
                panel1.Controls.Clear();
                agregarClientes();
                
            }
            if(e.KeyCode == Keys.Back)
            {
                if(textBox9.Text.Length==0)
                        textBox8.Focus();
            }
        }

        private String mandarTelefono()
        {
            return "(" + textBox6.Text + ") " + textBox8.Text + " - " + textBox9.Text;
        }

        private void textBox8_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                if (textBox8.Text.Length == 0)
                    textBox6.Focus();
            }
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
            if (e.KeyCode == Keys.Enter)
            {
                tp = StaticsFunctions.buscarServicios(textBox2.Text);
                panel2.Controls.Clear();
                agregarServicios();

            }
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
