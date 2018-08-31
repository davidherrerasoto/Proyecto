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

    public partial class AgregarListaPreciosEstilista : Form
    {
        TomarProductos tp = StaticsFunctions.tomarServicios();
        TomarAgentes ta = StaticsFunctions.tomarAgentes();
        TomarListaPrecio tlp = StaticsFunctions.tomarListaPrecios();
        List<Button> agentes;
        List<Button> productos;
        Agente ag;
        Producto pr;

        public AgregarListaPreciosEstilista()
        {
            InitializeComponent();
            this.CenterToScreen();
            agregarProductos();
            agregarAgentes();
            agregarAutocompleteAgentes();
            agregarAutocompleteProductos();
        }

        private void buscarAgentes()
        {
            ta = StaticsFunctions.buscarAgentes(textBox1.Text);
            if (ta.agentes != null)
                if (ta.agentes.Count > 0)
                    agregarAgentes();
        }


        private void buscarProductos()
        {
            tp = StaticsFunctions.buscarServicios(textBox2.Text);
            if (tp.productos != null)
                if (tp.productos.Count > 0)
                    agregarProductos();
        }

        private void agregarAutocompleteProductos()
        {
            AutoCompleteStringCollection col = new AutoCompleteStringCollection();
            col = new AutoCompleteStringCollection();
            for (int i = 0; i < tp.productos.Count; i++)
            {
                col.Add(tp.productos.ElementAt(i).nombre);

            }
            textBox2.AutoCompleteCustomSource = col;
        }

        private void agregarAutocompleteAgentes()
        {
            AutoCompleteStringCollection col = new AutoCompleteStringCollection();
            col = new AutoCompleteStringCollection();
            for (int i = 0; i < ta.agentes.Count; i++)
            {
                col.Add(ta.agentes.ElementAt(i).nombre);

            }
            textBox1.AutoCompleteCustomSource = col;
        }


        private void agregarProductos()
        {
            productos = new List<Button>();
            int x = 10, y = 10;
            for (int i = 0; i < tp.productos.Count; i++)
            {

                Button button = new Button();
                button.BackColor = Color.Black;
                button.Location = new Point(x + (210 * (i % 4)), y + (190 * (i / 4)));
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
            if (enviarProductoBaseDatos())
            {
                reiniciar();
            }
        }

        private void reiniciar()
        {
            pr = null;
            //MessageBox.Show("Menssage", "Producto correcto");
            tlp = StaticsFunctions.tomarListaPrecios();
        }

        private bool enviarProductoBaseDatos()
        {
            Precio precio = new Precio();
            if (ag != null)
            {
                int buscarPA = encontroProductoAgente();
                if (buscarPA==-1)
                {
                    if (precio.ShowDialog() == DialogResult.OK)
                    {
                        if (validarPrecio(precio.textBox2.Text))
                        {
                            ListaPrecio listaPrecio = crearListaPrecio(precio.textBox2.Text);
                            if (StaticsFunctions.enviarListaPrecio(listaPrecio) == 1)
                            {
                                MessageBox.Show("Menssage", "Envio del producto");
                                return true;
                            }
                            else
                            {
                                MessageBox.Show("Menssage", "Fallo el envio del producto");
                                return false;
                            }
                        }
                        else
                        {

                            MessageBox.Show("Precio no capturado o camturado incorrecto", "Menssage");
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }else
                {
                    modificarListaPrecio(buscarPA);
                }
            }
            else
                MessageBox.Show("Menssage", "Primero debe seleccionar el agente");
            return false;
        }

        private void modificarListaPrecio(int buscarPA)
        {
            Precio precio = new Precio();
            if (precio.ShowDialog() == DialogResult.OK)
            {
                if (validarPrecio(precio.textBox2.Text))
                {
                    ListaPrecio listaPrecio = crearListaPrecio(precio.textBox2.Text);
                    listaPrecio.idListaPrecio = tlp.listaPrecio.ElementAt(buscarPA).idListaPrecio;
                    if (StaticsFunctions.modificarListaPrecio(listaPrecio) == 1)
                    {
                        reiniciar();
                        MessageBox.Show("Menssage", "Modifico Producto");
                    }
                    else
                    {
                        MessageBox.Show("Menssage", "Fallo el envio del producto");
                    }
                }
                else
                {

                    MessageBox.Show("Precio no capturado o camturado incorrecto", "Menssage");
                }
            }
        }

        private int encontroProductoAgente()
        {
            if (tlp.listaPrecio != null)
                for (int i = 0; i < tlp.listaPrecio.Count; i++)
                {
                    if (tlp.listaPrecio.ElementAt(i).idAgente == ag.idAgente && tlp.listaPrecio.ElementAt(i).idProducto == pr.id)
                            return i;
                }
            return -1;
        }

        private ListaPrecio crearListaPrecio(string text)
        {
            ListaPrecio lp = new ListaPrecio();
            lp.idAgente = ag.idAgente;
            lp.idProducto = pr.id;
            lp.precio = Convert.ToDouble(text);
            return lp;
        }

        private bool validarPrecio(string text)
        {
            if (!text.Equals("") && !text.Equals("."))
                return true;
            return false;
        }

        private void agregarAgentes()
        {
            agentes = new List<Button>();
            int x = 10, y = 10;
            for (int i = 0; i < ta.agentes.Count; i++)
            {

                Button button = new Button();
                button.BackColor = Color.Black;
                button.Location = new Point(x + (100 * i), y);
                button.Size = new Size(80, 80);
                button.Font = new Font(button.Font.Name, 12,
                    button.Font.Style, button.Font.Unit);
                button.ForeColor = Color.White;
                button.Click += showIndex_Click;
                button.Text = ta.agentes.ElementAt(i).nombre;
                panel1.Controls.Add(button);
                agentes.Add(button);
            }

        }

        private void showIndex_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            var index = agentes.IndexOf(button);
            ag = ta.agentes.ElementAt(index);

           // MessageBox.Show("Menssage", ta.agentes.ElementAt(index).nombre);
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
            if (e.KeyCode == Keys.Enter)
            {
                panel2.Controls.Clear();
                buscarProductos();
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
            if (e.KeyCode == Keys.Enter)
            {
                panel1.Controls.Clear();
                buscarAgentes();
            }
        }
    }
}
