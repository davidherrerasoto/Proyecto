using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class AgregarProducto : Form
    {
        TomarProductos tp = null;
        Boolean modificarProducto = false;
        int indiceAModificar = -1;
        TomarUnidadMedidas tum = null;
        TomarProveedores tProv = StaticsFunctions.tomarProv();
        public AgregarProducto()
        {
            InitializeComponent();
            /*this.WindowState = FormWindowState.Maximized;
            this.dataGridView2.SetBounds(dataGridView2.Bounds.X,dataGridView2.Bounds.Y+10, this.Bounds.Width,this.Bounds.Height-10);
            this.panel1.SetBounds(dataGridView2.Bounds.X, dataGridView2.Bounds.Y, this.Bounds.Width+15, this.Bounds.Height+50);
            this.panel2.Location = new Point(panel1.Width+10,panel1.Location.Y);
            this.textBox4.Location = new Point(this.panel1.Location.X + 50, this.panel1.Height - 30 );
            this.label5.Location = new Point(this.panel1.Location.X, this.panel1.Height - 30);*/
            tp = StaticsFunctions.tomarProds();
            tum = StaticsFunctions.tomarUnidadMedidas();
            var list = new BindingList<GVProducto>(mandarProductosGV(tp.productos));
            dataGridView2.DataSource = list;
            this.CenterToScreen();
            textBox5.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox5.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection col = new AutoCompleteStringCollection();
            col.Add("12171500");
            col.Add("53131600");
            col.Add("52141700");
            textBox5.AutoCompleteCustomSource = col;
            agregarUnidadesMedida();
            agregarProveedores();
            /*DataGridViewColumn column = dataGridView2.Columns[0];
            column.Width = 160;*/
        }

        private void agregarProveedores()
        {
            for (int i = 0; i < tProv.proveedores.Count; i++)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = tProv.proveedores.ElementAt(i).denCom;
                item.Value = tProv.proveedores.ElementAt(i).idProveedor;
                comboBox2.Items.Add(item);
            }
            comboBox2.SelectedIndex = 0;
        }

        private void agregarUnidadesMedida()
        {
            for (int i = 0; i < tum.unidadMedidas.Count; i++)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = tum.unidadMedidas.ElementAt(i).nombre;
                item.Value = tum.unidadMedidas.ElementAt(i).idUnidad;
                comboBox1.Items.Add(item);
            }
            comboBox1.SelectedIndex = 1;
        }

        private void AgregarProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
            //MessageBox.Show((int)e.KeyChar+"", "Evento");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
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

            StaticsFunctions.manejarEventos(e, this);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
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

            StaticsFunctions.manejarEventos(e, this);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
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

            StaticsFunctions.manejarEventos(e, this);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView2_KeyPress(object sender, KeyPressEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
        }

        private Producto crearProducto()
        {

            Producto p = new Producto();
            p.precio = (float)Convert.ToDouble(this.textBox2.Text);
            p.comision = (float)Convert.ToDouble(this.textBox3.Text);
            p.nombre = this.textBox1.Text;
            p.claveSat = textBox5.Text;
            p.idUnidadMedida = (int)(comboBox1.SelectedItem as ComboBoxItem).Value;
            p.codigo = this.textBox6.Text;
            p.IVA = 16;
            p.idProveedor = (int)(comboBox2.SelectedItem as ComboBoxItem).Value;
            if (modificarProducto)
            {
                p.id = tp.productos.ElementAt(indiceAModificar).id;
            }
            return p;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (validarTextBox())
            {
                Producto p = crearProducto();
                if (!modificarProducto)
                {
                    if (StaticsFunctions.enviarProducto(p) == 1)
                    {
                        MessageBox.Show("Agregado", "Producto");
                    }
                    else
                    {
                        MessageBox.Show("Hubo un problema", "Warning");
                    }
                    tp = StaticsFunctions.tomarProds();
                    var list = new BindingList<GVProducto>(mandarProductosGV(tp.productos));
                    dataGridView2.DataSource = list;
                    if (tp.estado == 1)
                    {
                        reiniciarTextBox();
                    }
                }
                else
                {
                    if (validarTextBox())
                    {
                        if (StaticsFunctions.modificarProducto(p) == 1)
                        {
                            //insertarBaseDatos(p);
                            reiniciarTextBox();
                            reiniciarGridView();
                            MessageBox.Show("Producto Modificado", "Producto");
                        }
                        else
                        {
                            MessageBox.Show("Hubo un problema", "Warning");
                        }
                    }
                }
            }
        }

        private void insertarBaseDatos(Producto p)
        {
            string connetionString = null;
            string sql = null;
            connetionString = @"Data Source=.\SQLEXPRESS;
                          AttachDbFilename=SampleDatabase.mdf;
                          Integrated Security=True;
                          Connect Timeout=30;
                          User Instance=True";
            using (SqlConnection cnn = new SqlConnection(connetionString))
            {
                sql = "insert into productos ([Nombre], [Precio],[Comision]) values(@n,@p,@c)";
                cnn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, cnn))
                {
                    cmd.Parameters.AddWithValue("@n", p.nombre);
                    cmd.Parameters.AddWithValue("@p", p.precio);
                    cmd.Parameters.AddWithValue("@c", p.comision);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Row inserted !! ");
                }
            }
        }

        private void reiniciarGridView()
        {
            tp = StaticsFunctions.tomarProds();
            var list = new BindingList<GVProducto>(mandarProductosGV(tp.productos));
            dataGridView2.DataSource = list;
        }

        private void reiniciarTextBox()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox1.BackColor = Color.White;
            textBox2.BackColor = Color.White;
            textBox3.BackColor = Color.White;
            textBox5.BackColor = Color.White;
            textBox6.BackColor = Color.White;
            button1.Text = "Agregar";
        }

        private List<GVProducto> mandarProductosGV(List<Producto> productos)
        {
            List<GVProducto> p = new List<GVProducto>();
            if (productos != null)
            {
                for (int i = 0; i < productos.Count; i++)
                {
                    p.Add(new GVProducto()
                    {
                        Codigo = productos.ElementAt(i).codigo,
                        Nombre = productos.ElementAt(i).nombre,
                        Precio = productos.ElementAt(i).precio,
                        Comision = productos.ElementAt(i).comision,
                        ClaveSat = productos.ElementAt(i).claveSat,
                        UnidadMedida = buscarUnidadMedida(productos.ElementAt(i).idUnidadMedida),
                        Proveedor = buscarProveedor(productos.ElementAt(i).idProveedor)
                    });
                }
            }
            return p;
        }

        private String buscarProveedor(int idProveedor)
        {
            for (int i = 0; i < tProv.proveedores.Count; i++)
                if (tProv.proveedores.ElementAt(i).idProveedor == idProveedor)
                    return tProv.proveedores.ElementAt(i).denCom;
            return "";
        }

        String buscarUnidadMedida(int id)
        {
            for (int i = 0; i < tum.unidadMedidas.Count; i++)
                if (tum.unidadMedidas.ElementAt(i).idUnidad == id)
                    return tum.unidadMedidas.ElementAt(i).nombre;
            return "";
        }

        private bool validarTextBox()
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox5.Text != "" && textBox1.Text != " " && textBox2.Text != "." && textBox3.Text != "." && textBox5.Text != "."
                && comboBox1.SelectedIndex != 0 && comboBox2.SelectedIndex != 0  && textBox6.Text != "" && textBox1.Text != ".")
            {
                return true;
            }
            else
            {
                if (textBox1.Text == "" || textBox1.Text == " ")
                {
                    textBox1.BackColor = Color.Red;
                }
                else
                {
                    textBox1.BackColor = Color.White;
                }
                if (textBox2.Text == "" || textBox2.Text == ".")
                {
                    textBox2.BackColor = Color.Red;
                }
                else
                {
                    textBox2.BackColor = Color.White;
                }
                if (textBox3.Text == "" || textBox3.Text == ".")
                {
                    textBox3.BackColor = Color.Red;
                }
                else
                {
                    textBox3.BackColor = Color.White;
                }
                if (textBox5.Text == "" || textBox5.Text == ".")
                {
                    textBox5.BackColor = Color.Red;
                }
                else
                {
                    textBox5.BackColor = Color.White;
                }
                if (textBox6.Text == "" || textBox6.Text == ".")
                {
                    textBox6.BackColor = Color.Red;
                }
                else
                {
                    textBox6.BackColor = Color.White;
                }
                if (comboBox2.Enabled == true)
                {
                    if (comboBox2.SelectedIndex == 0)
                    {
                        MessageBox.Show("No selecciono Proveedor");
                    }
                }
                return false;
            }
        }

        private void dataGridView2_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //dataGridView2.Columns[0].Width = 160;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // MessageBox.Show(e.RowIndex+"", e.ColumnIndex+"");
            reiniciarTextBox();
            if (e.RowIndex >= 0)
            {
                textBox1.Text = tp.productos.ElementAt(e.RowIndex).nombre;
                textBox2.Text = tp.productos.ElementAt(e.RowIndex).precio + "";
                textBox3.Text = tp.productos.ElementAt(e.RowIndex).comision + "";
                textBox5.Text = tp.productos.ElementAt(e.RowIndex).claveSat;
                textBox6.Text = tp.productos.ElementAt(e.RowIndex).codigo;
                comboBox1.SelectedIndex = buscarIndiceUnidadMedida(tp.productos.ElementAt(e.RowIndex).idUnidadMedida) + 1;
                comboBox2.SelectedIndex = buscarIndiceProveedor(tp.productos.ElementAt(e.RowIndex).idProveedor) + 1;
                button1.Text = "Modificar";
                modificarProducto = true;
                indiceAModificar = e.RowIndex;
            }
        }

        private int buscarIndiceUnidadMedida(int id)
        {
            for (int i = 0; i < tum.unidadMedidas.Count; i++)
                if (tum.unidadMedidas.ElementAt(i).idUnidad == id)
                    return i;
            return -1;
        }

        private int buscarIndiceProveedor(int id)
        {
            for (int i = 0; i < tProv.proveedores.Count; i++)
                if (tProv.proveedores.ElementAt(i).idProveedor == id)
                    return i;
            return -1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            reiniciarTextBox();
            modificarProducto = false;
        }

        private void AgregarProducto_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'baseDatosDataSet.productos' Puede moverla o quitarla según sea necesario.
            //this.productosTableAdapter.Fill(this.baseDatosDataSet.productos);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (modificarProducto)
            {
                if (StaticsFunctions.lanzarDialogYesNo("Eliminar", "Esta Seguro"))
                {
                    Producto p = crearProducto();
                    if (StaticsFunctions.eliminarProducto(p) == 1)
                    {
                        reiniciarGridView();
                        reiniciarTextBox();
                        modificarProducto = false;
                        MessageBox.Show("Eliminado", "Producto");
                    }
                    else
                    {
                        MessageBox.Show("Hubo un problema", "Warning");
                    }

                }
            }
        }

        private void textBox4_KeyUp(object sender, KeyEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
            if (e.KeyCode == Keys.Enter)
            {
                tp = StaticsFunctions.buscarProds(textBox4.Text);
                var list = new BindingList<GVProducto>(mandarProductosGV(tp.productos));
                dataGridView2.DataSource = list;
                reiniciarTextBox();
                modificarProducto = false;
            }
        }

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
        }

        private void button3_KeyPress(object sender, KeyPressEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
        }

        private void button2_KeyPress(object sender, KeyPressEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
        }

        private void textBox6_KeyUp(object sender, KeyEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
        }
    }
}
