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
    public partial class AgregarCompra : Form
    {
        TomarProveedores todosProveedores = StaticsFunctions.tomarProv();
        TomarProveedores tProv = StaticsFunctions.tomarProv();
        Proveedor prov = null;
        Producto pr = null;
        TomarProductos tp = null;
        List<Button> proveedores = null;
        List<Button> productos = null;
        List<Producto> prods = new List<Producto>();
        List<Movimiento> movimientos = new List<Movimiento>();
        public AgregarCompra()
        {
            InitializeComponent();
            this.CenterToScreen();
            agregarProveedores();
        }

        private void agregarProveedores()
        {
            proveedores = new List<Button>();
            int x = 10, y = 10;
            for (int i = 0; i < tProv.proveedores.Count; i++)
            {

                Button button = new Button();
                button.BackColor = Color.Black;
                button.Location = new Point(x + (100 * i), y);
                button.Size = new Size(80, 80);
                button.Font = new Font(button.Font.Name, 12,
                    button.Font.Style, button.Font.Unit);
                button.ForeColor = Color.White;
                button.Click += showIndex_Click;
                button.Text = tProv.proveedores.ElementAt(i).denCom;
                panel1.Controls.Add(button);
                proveedores.Add(button);
            }

        }

        private void showIndex_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            var index = proveedores.IndexOf(button);
            prov = tProv.proveedores.ElementAt(index);
            textBox8.Text = prov.denCom;
            cargarProductosDistribuidor();
            //MessageBox.Show("Menssage", tp.productos.ElementAt(index).nombre);
        }

        private void cargarProductosDistribuidor()
        {
            tp = StaticsFunctions.tomarProductosProveedor(prov.idProveedor);
            agregarProductos();
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
                panel3.Controls.Add(button);
                productos.Add(button);
            }
        }

        private void showIndex_Click2(object sender, EventArgs e)
        {
            var button = sender as Button;
            var index = productos.IndexOf(button);
            pr = tp.productos.ElementAt(index);
            Unidades unidades = new Unidades();
            Movimiento mov = new Movimiento();
            mov.idAgente = prov.idProveedor;
            mov.idProducto = pr.id;
            mov.IVA = 0;
            mov.subTotal = 0;
            mov.total = 0;
            unidades.ActiveControl = unidades.textBox2;
            if (unidades.ShowDialog() == DialogResult.OK)
            {
                if (!unidades.textBox2.Text.Equals(""))
                    mov.unidades = Convert.ToInt32(unidades.textBox2.Text);
                else
                    mov.unidades = 1;
                prods.Add(pr);
                movimientos.Add(mov);
                reiniciarVentas();
            }
        }

        private void reiniciarVentas()
        {
            var list = new BindingList<GVProductoCompra>(mandarProductosGV(prods,movimientos));
            dataGridView1.DataSource = list;
        }

        private List<GVProductoCompra> mandarProductosGV(List<Producto> productos,List<Movimiento> movs)
        {
            List<GVProductoCompra> p = new List<GVProductoCompra>();
            if (productos != null)
            {
                for (int i = 0; i < productos.Count; i++)
                {
                    GVProductoCompra gvP = new GVProductoCompra()
                    {
                        Unidades = movs.ElementAt(i).unidades,
                        Nombre = productos.ElementAt(i).nombre,
                        Proveedor = buscarProveedor(movs.ElementAt(i).idAgente)
                        
                    };
                    p.Add(gvP);
                }
            }
            return p;
        }

        private String buscarProveedor(int idAgente)
        {
            for (int i = 0; i < todosProveedores.proveedores.Count; i++)
                if (todosProveedores.proveedores.ElementAt(i).idProveedor == idAgente)
                    return todosProveedores.proveedores.ElementAt(i).denCom;
            return "";
        }

        private void textBox5_KeyUp(object sender, KeyEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
            if (e.KeyCode == Keys.Enter)
            {
                panel1.Controls.Clear();
                buscarProveedores();
            }
        }

        private void buscarProveedores()
        {
            tProv = StaticsFunctions.buscarProv(textBox5.Text);
            agregarProveedores();
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

        private void buscarProductos()
        {
            tp = StaticsFunctions.buscarProdsProveedor(textBox1.Text,prov.idProveedor);
            agregarProductos();
        }

        private bool enviarCompra()
        {
            Documento d = new Documento();
            d.idCliente = 0;
            d.idUsuario = 1;
            d.idConcepto = 12;
            d.afectado = 0;
            d.idAgente = prov.idProveedor;
            d.subTotal = 0;
            d.IVA = 0;
            d.total = 0;
            int idDocumento = StaticsFunctions.enviarDocumento(d);
            if (idDocumento != -1)
            {
                List<Movimiento> movs = new List<Movimiento>();
                for (int i = 0; i < movimientos.Count; i++)
                {
                    Movimiento mov = new Movimiento();
                    mov.idDocumento = idDocumento;
                    mov.idProducto = movimientos.ElementAt(i).idProducto;
                    mov.subTotal = movimientos.ElementAt(i).total / 1.16;
                    mov.IVA = mov.subTotal * (movimientos.ElementAt(i).IVA / 100);
                    mov.total = movimientos.ElementAt(i).total;
                    mov.unidades = movimientos.ElementAt(i).unidades;
                    mov.idConcepto = 12;
                    mov.idAgente = d.idAgente;
                    movs.Add(mov);

                }
                if (StaticsFunctions.enviarMovimientos(movs) == 1)
                {
                    return true;
                }
                else
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

        private void reiniciarGridView()
        {

            prods = new List<Producto>();
            movimientos = new List<Movimiento>();
            var list = new BindingList<GVProductoCompra>(mandarProductosGV(prods,movimientos));
            dataGridView1.DataSource = list;
            textBox5.Text = "";
            textBox8.Text = "";
            panel1.Controls.Clear();
            panel3.Controls.Clear();
            tProv = StaticsFunctions.tomarProv();
            agregarProveedores();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (prods.Count > 0)
            {
                if (enviarCompra())
                {
                    reiniciarGridView();
                    MessageBox.Show("Compra Realizada", "Mensaje");
                }
            }else
            {
                MessageBox.Show("No hay productos en la lista", "Mensaje");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            reiniciarGridView();
        }
    }
}
