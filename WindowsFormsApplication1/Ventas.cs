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
    public partial class Ventas : Form
    {
        TomarProductos tp = StaticsFunctions.tomarProds();
        TomarClientes tc = StaticsFunctions.tomarClientes();
        TomarAgentes ta = StaticsFunctions.tomarAgentes();
        TomarUnidadMedidas tum = StaticsFunctions.tomarUnidadMedidas();
        TomarListaPrecio tlp = null;
        List<Button> clientes;
        List<Button> agentes;
        List<Button> productos;
        Cliente cl;
        Agente ag;
        Producto pr;
        List<Producto> prs = new List<Producto>();
        double subTotal = 0;
        double iva = 0;
        double total = 0;

        public Ventas()
        {
            InitializeComponent();
            this.CenterToScreen();
            agregarClientes();
            agregarAgentes();
            agregarUnidadesMedida();
            agregarAutocompleteClientes();
            agregarAutocompleteAgentes();
            agregarAutocompleteProductos();
            comboBox1.SelectedIndex = 1;
            cambioFiltro();
        }

        private void agregarUnidadesMedida()
        {
            for (int i = 0; i < tum.unidadMedidas.Count; i++)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = tum.unidadMedidas.ElementAt(i).nombreFiltro;
                item.Value = tum.unidadMedidas.ElementAt(i).idUnidad;
                comboBox1.Items.Add(item);
            }

        }

        private void buscarAgentes()
        {
            ta = StaticsFunctions.buscarAgentes(textBox6.Text);
            if(ta.agentes!=null)
                if (ta.agentes.Count > 0)
                    agregarAgentes();
        }

        private void buscarClientes()
        {
            tc = StaticsFunctions.buscarClientes(textBox5.Text);
            if(tc.clientes!=null)
                if (tc.clientes.Count > 0)
                    agregarClientes();
        }

        private void buscarProductos()
        {
            tp = StaticsFunctions.buscarProds(textBox1.Text, (int)(comboBox1.SelectedItem as ComboBoxItem).Value);
            if (tp.productos != null)
                if (tp.productos.Count > 0)
                {
                    agregarProductos();
                    modificarProductos();
                }
        }

        private void agregarAutocompleteProductos()
        {
            AutoCompleteStringCollection col = new AutoCompleteStringCollection();
            col = new AutoCompleteStringCollection();
            for (int i = 0; i < tp.productos.Count; i++)
            {
                col.Add(tp.productos.ElementAt(i).nombre);

            }
            textBox1.AutoCompleteCustomSource = col;
        }

        private void agregarAutocompleteAgentes()
        {
            AutoCompleteStringCollection col = new AutoCompleteStringCollection();
            col = new AutoCompleteStringCollection();
            for (int i = 0; i < ta.agentes.Count; i++)
            {
                col.Add(ta.agentes.ElementAt(i).nombre);

            }
            textBox6.AutoCompleteCustomSource = col;
        }

        private void agregarAutocompleteClientes()
        {
            AutoCompleteStringCollection col = new AutoCompleteStringCollection();
            
            for (int i = 0; i < tc.clientes.Count; i++)
            {
                col.Add(tc.clientes.ElementAt(i).denCom);
            }
            textBox5.AutoCompleteCustomSource = col;
        }

        private void agregarProductos()
        {
            productos = new List<Button>();
            int x = 10, y = 10;
            for (int i = 0; i < tp.productos.Count; i++)
            {

                Button button = new Button();
                button.BackColor = Color.Black;
                button.Location = new Point(x+(210*(i%4)), y+(190*(i/4)));
                button.Size = new Size(180, 180);
                button.Font = new Font(button.Font.Name, 16,
                    button.Font.Style, button.Font.Unit);
                button.ForeColor = Color.White;
                button.Click += showIndex_Click;
                button.Text = tp.productos.ElementAt(i).nombre;
                panel3.Controls.Add(button);
                productos.Add(button);
            }

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
                button.Click += showIndex_Click2;
                button.Text = ta.agentes.ElementAt(i).nombre;
                panel4.Controls.Add(button);
                agentes.Add(button);
            }

        }

        private void agregarClientes()
        {
            clientes = new List<Button>();
            int x = 10, y = 10;
            for (int i = 0; i < tc.clientes.Count; i++)
            {

                Button button = new Button();
                button.BackColor = Color.Black;
                button.Location = new Point(x + (100 * i ), y );
                button.Size = new Size(80, 80);
                button.Font = new Font(button.Font.Name, 12,
                    button.Font.Style, button.Font.Unit);
                button.ForeColor = Color.White;
                button.Click += showIndex_Click3;
                button.Text = tc.clientes.ElementAt(i).denCom;
                panel1.Controls.Add(button);
                clientes.Add(button);
            }

        }
       
        private void modificarProductosListaPrecio(TomarProductos tomarP)
        {
            TomarListaPrecio tomarListaPrecio = StaticsFunctions.tomarListaPrecios(ag);
            if (tomarListaPrecio.listaPrecio != null)
            {
                for (int i = 0; i < tomarListaPrecio.listaPrecio.Count; i++)
                {
                    for (int j = 0; j < tomarP.productos.Count; j++)
                    {
                        if (tomarP.productos.ElementAt(j).id == tomarListaPrecio.listaPrecio.ElementAt(i).idProducto)
                        {
                            tomarP.productos.ElementAt(j).precio = (float)tomarListaPrecio.listaPrecio.ElementAt(i).precio;
                        }
                    }

                }
            }
        }

        private void modificarPrecios()
        {
            TomarProductos tomarP = StaticsFunctions.tomarProdsModificarPrecio();
            modificarProductosListaPrecio(tomarP);
            if (prs != null)
            {
                for (int i = 0; i < prs.Count; i++)
                {
                    for (int j = 0; j < tomarP.productos.Count; j++)
                    {
                        if (tomarP.productos.ElementAt(j).id == prs.ElementAt(i).id)
                        {
                           prs.ElementAt(i).precio = tomarP.productos.ElementAt(j).precio;
                        }
                    }

                }
            }
        }

        private void showIndex_Click2(object sender, EventArgs e)
        {
            var button = sender as Button;
            var index = agentes.IndexOf(button);
            if (ag != null)
            {
                MessageBox.Show("Se Cambiaron los precios", "Mensaje");
            }
            ag = ta.agentes.ElementAt(index);
            modificarPrecios();
            modificarProductos();
            reiniciarVentas();
            textBox8.Text = ag.nombre;
            //MessageBox.Show("Menssage", ta.agentes.ElementAt(index).nombre);
        }

        private void modificarProductos()
        {
            if (ag != null)
            {
                tlp = StaticsFunctions.tomarListaPrecios(ag);
                if (tlp.listaPrecio != null)
                {
                    for (int i = 0; i < tlp.listaPrecio.Count; i++)
                    {
                        for (int j = 0; j < tp.productos.Count; j++)
                        {
                            if (tp.productos.ElementAt(j).id == tlp.listaPrecio.ElementAt(i).idProducto)
                            {
                                tp.productos.ElementAt(j).precio = (float)tlp.listaPrecio.ElementAt(i).precio;
                            }
                        }

                    }
                }
            }
           
        }

        private void showIndex_Click3(object sender, EventArgs e)
        {
            var button = sender as Button;
            var index = clientes.IndexOf(button);
            cl = tc.clientes.ElementAt(index);
            textBox7.Text = cl.denCom;
            int indexAgente = buscarIndexAgente();
            ag = ta.agentes.ElementAt(indexAgente);
            modificarPrecios();
            modificarProductos();
            reiniciarVentas();
            textBox8.Text = ag.nombre;
            //MessageBox.Show("Menssage", tc.clientes.ElementAt(index).denCom);
        }

        private int buscarIndexAgente()
        {
            for (int i=0;i < ta.agentes.Count;i++)
            {
                if (ta.agentes.ElementAt(i).idAgente == cl.idAgente)
                    return i;
            }
            return -1;
        }

        private void showIndex_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            var index = productos.IndexOf(button);
            pr = tp.productos.ElementAt(index);
            agregarProducto();
            //MessageBox.Show("Menssage", tp.productos.ElementAt(index).nombre);
        }

        private void agregarProducto()
        {
            prs.Add(pr);
            reiniciarVentas();
        }

        private void reiniciarVentas()
        {
            var list = new BindingList<GVProductoVenta>(mandarProductosGV(prs));
            dataGridView1.DataSource = list;
        }

        private List<GVProductoVenta> mandarProductosGV(List<Producto> productos)
        {
            List<GVProductoVenta> p = new List<GVProductoVenta>();
            subTotal = 0;
            iva = 0;
            total = 0;
            if (productos != null)
            {
                for (int i = 0; i < productos.Count; i++)
                {
                    GVProductoVenta gvP= new GVProductoVenta()
                    {
                        Nombre = productos.ElementAt(i).nombre,
                        Precio = productos.ElementAt(i).precio
                    };
                    total += gvP.Precio;
                    p.Add(gvP);
                }
            }
           
            textBox4.Text = total + "";
            return p;
        }

        private int buscarEnListaProductos()
        {
            for (int i = 0; i < prs.Count; i++)
                if (prs.ElementAt(i).id == pr.id)
                    return i;
            return -1;
        }

        private void manejarEvento(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tc = StaticsFunctions.buscarClientes(textBox1.Text);
                if (tc.clientes.Count == 1)
                {
                    textBox1.Text = tc.clientes.ElementAt(0).denCom;
                    textBox1.Text = tc.clientes.ElementAt(0).codigo;
                }
                else
                {
                    Seleccionar s = new Seleccionar();

                    if (s.ShowDialog() == DialogResult.OK)
                    {
                        s.ini(tc);
                    }
                }
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
            if (e.KeyCode == Keys.Enter)
            {
                panel3.Controls.Clear();
                buscarProductos();
            }
        }

        private void textBox5_KeyUp(object sender, KeyEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
            if (e.KeyCode == Keys.Enter)
            {
                panel1.Controls.Clear();
                buscarClientes();
            }
        }

        private void textBox6_KeyUp(object sender, KeyEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
            if (e.KeyCode == Keys.Enter)
            {
                panel4.Controls.Clear();
                buscarAgentes();
            }
        }

        private bool calcularCambio()
        {
            double total = Convert.ToDouble(textBox4.Text);
            double totalDls = 0;
            double totalPes = 0;
            if (textBox10.Text != ""&&textBox10.Text!=".")
                totalDls = Convert.ToDouble(textBox10.Text) * Convert.ToDouble(textBox11.Text);

            if (textBox9.Text != ""&&textBox9.Text!=".")
                totalPes = Convert.ToDouble(textBox9.Text);

            double cambio = total - totalPes - totalDls;
            if (cambio <= 0)
            {
                if (StaticsFunctions.lanzarDialogYesNo("Cambio", Math.Abs(cambio) + ""))
                    return true;
                else
                    return false;
            }
            else
            {
                MessageBox.Show("Falta " + cambio+"", "Mensaje");
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (validarEnvioDocumento())
            {
                if (calcularCambio())
                {
                    if (enviarVentaBaseDatos())
                        reiniciarGridView();
                }
            }

        }

        private void reiniciarGridView()
        {
           
            prs = new List<Producto>();
            var list = new BindingList<GVProductoVenta>(mandarProductosGV(prs));
            dataGridView1.DataSource = list;
            textBox4.Text = "0";
            textBox7.Text = "";
            textBox8.Text = "";
            cl = null;
            ag = null;
            panel3.Controls.Clear();
            tp = StaticsFunctions.tomarServicios();
            comboBox1.SelectedIndex = 1;
            tlp = null;
            agregarProductos();

        }

        private bool enviarVentaBaseDatos()
        {
            Documento d = new Documento();
            d.idCliente = cl.idCliente;
            d.idUsuario = 1;
            d.idConcepto = 4;
            d.afectado = 0;
            if (ag == null)
                d.idAgente = cl.idAgente;
            else
                d.idAgente = ag.idAgente;
            d.subTotal = subTotal;
            d.IVA = iva;
            d.total = total;
            int idDocumento = StaticsFunctions.enviarDocumento(d);
            if (idDocumento != -1)
            {
                List<Movimiento> movs = new List<Movimiento>();
                for (int i = 0; i < prs.Count; i++)
                {
                    Movimiento mov = new Movimiento();
                    mov.idDocumento = idDocumento;
                    mov.idProducto = prs.ElementAt(i).id;
                    mov.subTotal = prs.ElementAt(i).precio / 1.16;
                    mov.IVA = mov.subTotal * (prs.ElementAt(i).IVA / 100);
                    mov.total = prs.ElementAt(i).precio; 
                    mov.unidades = 1;
                    mov.idConcepto = 4;
                    mov.idAgente = d.idAgente;
                    movs.Add(mov);

                }
                if (StaticsFunctions.enviarMovimientos(movs) == 1)
                {
                    return true;
                }else
                {
                    return false;
                }

            }
            else
            {
                MessageBox.Show("No puedo introducir venta", "Warning");
                return false;
            }

        }

        private bool validarEnvioDocumento()
        {
            if (cl != null && prs.Count != 0)
            {
                if (ag == null)
                    if (StaticsFunctions.lanzarDialogYesNo("Mensaje","No Selecciona el agente se agregara el agente de veenta del cliente"))
                        return true;
                    else
                        return false;
                return true;
            }else
            {
                if (cl == null)
                    MessageBox.Show("Favor de seleccionar Cliente","Mensaje");
                else if (prs.Count==0)
                    MessageBox.Show("No hay productos en la cuenta", "Mensaje");
                return false;
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    if (StaticsFunctions.lanzarDialogYesNo("Eliminar " + prs.ElementAt(e.RowIndex).nombre, "Esta Seguro"))
                    {
                        prs.RemoveAt(e.RowIndex); 
                        reiniciarVentas();
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    
                }
            }

        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // Only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // Only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            reiniciarGridView();
            reiniciarVentas();
            
        }

        private void cambioFiltro()
        {
            panel3.Controls.Clear();
            buscarProductos();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cambioFiltro();
        }
    }
}
