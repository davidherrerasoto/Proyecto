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
    public partial class AgregarServicio : Form
    {
        TomarProductos tp = null;
        TomarUnidadMedidas tum = null;
        Boolean modificarServicio = false;
        int indiceAModificar = -1;
        public AgregarServicio()
        {
            InitializeComponent();
            this.CenterToScreen();
            tum = StaticsFunctions.tomarUnidadMedidas();
            agregarUnidadesMedida();
            reiniciarGridView();
            comboBox1.SelectedIndex = 2;
        }

        private void reiniciarGridView()
        {
            tp = StaticsFunctions.tomarServicios();
            var list = new BindingList<GVServicio>(mandarServiciosGV(tp.productos));
            dataGridView2.DataSource = list;
        }

        private List<GVServicio> mandarServiciosGV(List<Producto> servicios)
        {
            List<GVServicio> p = new List<GVServicio>();
            if (servicios != null)
            {
                for (int i = 0; i < servicios.Count; i++)
                {
                    p.Add(new GVServicio()
                    {
                        Codigo = servicios.ElementAt(i).codigo,
                        Nombre = servicios.ElementAt(i).nombre,
                        Precio = servicios.ElementAt(i).precio,
                        Comision = servicios.ElementAt(i).comision,
                        ClaveSat = servicios.ElementAt(i).claveSat,
                        UnidadMedida = buscarUnidadMedida(servicios.ElementAt(i).idUnidadMedida)
                    });
                }
            }
            return p;
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
                && comboBox1.SelectedIndex != 0 && textBox6.Text != "" && textBox6.Text != " " && textBox6.Text != ".")
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
                return false;
            }
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

        private void agregarUnidadesMedida()
        {
            for (int i = 0; i < tum.unidadMedidas.Count; i++)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = tum.unidadMedidas.ElementAt(i).nombre;
                item.Value = tum.unidadMedidas.ElementAt(i).idUnidad;
                comboBox1.Items.Add(item);
            }

        }

        private Producto crearServicio()
        {
            Producto s = new Producto();
            s.precio = (float)Convert.ToDouble(this.textBox2.Text);
            s.comision = (float)Convert.ToDouble(this.textBox3.Text);
            s.nombre = this.textBox1.Text;
            s.claveSat = textBox5.Text;
            s.idUnidadMedida = (int)(comboBox1.SelectedItem as ComboBoxItem).Value;
            s.codigo = textBox6.Text;
            s.idProveedor = 0;
            return s;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (validarTextBox())
            {
                agregarServicio(crearServicio());
            }
        }

        private void agregarServicio(Producto ser)
        {
            if (!modificarServicio)
            {
                if (StaticsFunctions.enviarServicio(ser) == 1)
                {
                    MessageBox.Show("Agregado", "Servicio");
                }
                else
                {
                    MessageBox.Show("Hubo un problema", "Warning");
                }
                tp = StaticsFunctions.tomarServicios();
                var list = new BindingList<GVServicio>(mandarServiciosGV(tp.productos));
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
                    ser.id = this.tp.productos.ElementAt(this.indiceAModificar).id;
                    if (StaticsFunctions.modificarServicio(ser) == 1)
                    {
                        //insertarBaseDatos(p);
                        reiniciarTextBox();
                        reiniciarGridView();
                        modificarServicio = false;
                        MessageBox.Show("Servicio Modificado", "Servicio");
                    }
                    else
                    {
                        MessageBox.Show("Hubo un problema", "Warning");
                    }
                }
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            reiniciarTextBox();
            if (e.RowIndex >= 0)
            {
                textBox1.Text = tp.productos.ElementAt(e.RowIndex).nombre;
                textBox2.Text = tp.productos.ElementAt(e.RowIndex).precio + "";
                textBox3.Text = tp.productos.ElementAt(e.RowIndex).comision + "";
                textBox5.Text = tp.productos.ElementAt(e.RowIndex).claveSat;
                textBox6.Text = tp.productos.ElementAt(e.RowIndex).codigo;
                comboBox1.SelectedIndex = buscarIndiceUnidadMedida(tp.productos.ElementAt(e.RowIndex).idUnidadMedida) + 1;
                button1.Text = "Modificar";
                modificarServicio = true;
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

        private void button2_Click(object sender, EventArgs e)
        {
            reiniciarTextBox();
            modificarServicio = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (modificarServicio)
            {
                if (StaticsFunctions.lanzarDialogYesNo("Eliminar", "Esta Seguro"))
                {
                    Producto s = new Producto(); 
                    s.id = tp.productos.ElementAt(indiceAModificar).id;
                    if (StaticsFunctions.eliminarServicio(s) == 1)
                    {
                        reiniciarGridView();
                        reiniciarTextBox();
                        modificarServicio = false;
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
                tp = StaticsFunctions.buscarServicios(textBox4.Text);
                var list = new BindingList<GVServicio>(mandarServiciosGV(tp.productos));
                dataGridView2.DataSource = list;
                reiniciarTextBox();
                modificarServicio = false;
            }
        }

        private void dataGridView2_KeyPress(object sender, KeyPressEventArgs e)
        {
            StaticsFunctions.manejarEventos(e,this);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
        }

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
        }

        private void textBox5_KeyUp(object sender, KeyEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
        }

        private void comboBox1_KeyUp(object sender, KeyEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
        }

        private void button2_KeyPress(object sender, KeyPressEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
        }

        private void button3_KeyPress(object sender, KeyPressEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
        }

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
        }

        private void textBox6_KeyUp(object sender, KeyEventArgs e)
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
    }
}
