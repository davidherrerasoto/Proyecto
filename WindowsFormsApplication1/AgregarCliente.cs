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
    public partial class AgregarCliente : Form
    {
        TomarClientes tc = null;
        Boolean modificarCliente = false;
        int indiceAModificar = -1;
        TomarAgentes ta = null;
        public AgregarCliente()
        {
            InitializeComponent();
            tc = StaticsFunctions.tomarClientes();
            ta = StaticsFunctions.tomarAgentes();
            var list = new BindingList<GVCliente>(mandarClientesGV(tc.clientes));
            dataGridView2.DataSource = list;
            agregarAgentes();
            //comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            this.CenterToScreen();
        }

        private void agregarAgentes()
        {
            for (int i = 0; i < ta.agentes.Count; i++)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = ta.agentes.ElementAt(i).nombre;
                item.Value = ta.agentes.ElementAt(i).idAgente;
                comboBox2.Items.Add(item);
            }
        }

        String buscarNombreAgente(int id)
        {
            for (int i = 0; i < ta.agentes.Count; i++)
                if (ta.agentes.ElementAt(i).idAgente == id)
                    return ta.agentes.ElementAt(i).nombre;
            return "";
        }

        private int buscarIndiceAgente(int id)
        {
            for (int i = 0; i <ta.agentes.Count; i++)
                if (ta.agentes.ElementAt(i).idAgente == id)
                    return i;
            return -1;
        }

        private bool validarTextBox()
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox1.Text != " " && textBox2.Text != "." && textBox3.Text != "." &&
                 comboBox2.SelectedIndex!=0 && textBox2.Text.Length==13 && textBox5.Text != "" && textBox3.Text != "."
                 && textBox6.Text != "" && textBox7.Text != "" && textBox8.Text != "" && textBox9.Text != ""
                 && textBox6.Text.Length == 3 && textBox7.Text != " " && textBox8.Text.Length == 3 && textBox9.Text.Length == 4)
            {
                return true;
            }
            else
            {
                if (textBox1.Text == "" || textBox1.Text == " " || textBox1.Text == ".")
                {
                    textBox1.BackColor = Color.Red;
                }
                else
                {
                    textBox1.BackColor = Color.White;
                }
                if (textBox2.Text == "" || textBox2.Text == "." || textBox2.Text.Length != 13)
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
                if (textBox6.Text == "" || textBox6.Text.Length != 3)
                {
                    textBox6.BackColor = Color.Red;
                }
                else
                {
                    textBox6.BackColor = Color.White;
                }
                if (textBox7.Text == "" || textBox7.Text==".")
                {
                    textBox7.BackColor = Color.Red;
                }
                else
                {
                    textBox7.BackColor = Color.White;
                }
                if (textBox8.Text == "" || textBox8.Text.Length != 3)
                {
                    textBox8.BackColor = Color.Red;
                }
                else
                {
                    textBox8.BackColor = Color.White;
                }
                if (textBox9.Text == "" || textBox9.Text.Length != 4)
                {
                    textBox9.BackColor = Color.Red;
                }
                else
                {
                    textBox9.BackColor = Color.White;
                }
                return false;
            }
        }

        private void dataGridView2_KeyPress(object sender, KeyPressEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (validarTextBox())
            {
                Cliente cl = new Cliente();
                cl.denCom = this.textBox1.Text;
                cl.rfc = this.textBox2.Text;
                cl.repLeg = this.textBox3.Text;
                cl.idClasificacion = 1;
                cl.idAgente = (int)(comboBox2.SelectedItem as ComboBoxItem).Value;
                cl.codigo = this.textBox5.Text;
                cl.direccion = textBox7.Text;
                cl.telefono = "(" + textBox6.Text + ") " + textBox8.Text + " - " + textBox9.Text;
                agregarCliente(cl);
            }
        }

        private void agregarCliente(Cliente cl)
        {
            if (!modificarCliente)
            {
                if (StaticsFunctions.enviarCliente(cl) == 1)
                {
                    MessageBox.Show("Agregado", "Cliente");
                }
                else
                {
                    MessageBox.Show("Hubo un problema", "Warning");
                }
                tc = StaticsFunctions.tomarClientes();
                var list = new BindingList<GVCliente>(mandarClientesGV(tc.clientes));
                dataGridView2.DataSource = list;
                if (tc.estado == 1)
                {
                    reiniciarTextBox();
                }
            }
            else
            {
                if (validarTextBox())
                {
                    cl.idCliente = this.tc.clientes.ElementAt(this.indiceAModificar).idCliente;
                    if (StaticsFunctions.modificarCliente(cl) == 1)
                    {
                        //insertarBaseDatos(p);
                        reiniciarTextBox();
                        reiniciarGridView();
                        modificarCliente = false;
                        MessageBox.Show("Cliente Modificado", "Cliente");
                    }
                    else
                    {
                        MessageBox.Show("Hubo un problema", "Warning");
                    }
                }
            }
        }

        private void reiniciarGridView()
        {
            tc = StaticsFunctions.tomarClientes();
            var list = new BindingList<GVCliente>(mandarClientesGV(tc.clientes));
            dataGridView2.DataSource = list;
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
        }

        private void reiniciarTextBox()
        {
            textBox1.Text = "";
            textBox2.Text = "XAXX010101000";
            textBox3.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox1.BackColor = Color.White;
            textBox2.BackColor = Color.White;
            textBox3.BackColor = Color.White;
            textBox5.BackColor = Color.White;
            textBox6.BackColor = Color.White;
            textBox7.BackColor = Color.White;
            textBox8.BackColor = Color.White;
            textBox9.BackColor = Color.White;
            button1.Text = "Alta Cliente";
            comboBox2.SelectedIndex = 0;

        }

        List<GVCliente> mandarClientesGV(List<Cliente> cls)
        {
            List<GVCliente> c = new List<GVCliente>();
            if (cls != null)
            {
                for (int i = 0; i < cls.Count; i++)
                {
                    c.Add(new GVCliente()
                    {
                        Codigo = cls.ElementAt(i).codigo,
                        DenCom = cls.ElementAt(i).denCom,
                        RFC = cls.ElementAt(i).rfc,
                        RepLeg = cls.ElementAt(i).repLeg,
                        Agente = this.buscarNombreAgente(cls.ElementAt(i).idAgente),
                        Telefono = cls.ElementAt(i).telefono,
                        Direccion = cls.ElementAt(i).direccion
                    });
                }
            }

            return c;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            reiniciarTextBox();
            modificarCliente = false;
        }

        
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            reiniciarTextBox();
            if (e.RowIndex >= 0)
            {
                textBox1.Text = tc.clientes.ElementAt(e.RowIndex).denCom;
                textBox2.Text = tc.clientes.ElementAt(e.RowIndex).rfc;
                textBox3.Text = tc.clientes.ElementAt(e.RowIndex).repLeg;
                textBox5.Text = tc.clientes.ElementAt(e.RowIndex).codigo;
                //comboBox1.SelectedIndex = tc.clientes.ElementAt(e.RowIndex).idClasificacion;
                comboBox2.SelectedIndex = this.buscarIndiceAgente(tc.clientes.ElementAt(e.RowIndex).idAgente)+1;
                textBox7.Text = tc.clientes.ElementAt(e.RowIndex).direccion;
                textBox6.Text = tc.clientes.ElementAt(e.RowIndex).telefono.Substring(1, 3);
                textBox8.Text = tc.clientes.ElementAt(e.RowIndex).telefono.Substring(6, 3);
                textBox9.Text = tc.clientes.ElementAt(e.RowIndex).telefono.Substring(12, 4);
                button1.Text = "Modificar";
                modificarCliente = true;
                indiceAModificar = e.RowIndex;
            }
        }

        private Cliente crearClienteParaEliminar()
        {
            Cliente cl = new Cliente();
            
            return cl;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(modificarCliente)
            {
                if (StaticsFunctions.lanzarDialogYesNo("Eliminar", "Esta Seguro"))
                {
                    Cliente cl = new Cliente();
                    cl.idCliente = tc.clientes.ElementAt(indiceAModificar).idCliente;
                    if (StaticsFunctions.eliminarCliente(cl) == 1)
                    {
                        reiniciarGridView();
                        reiniciarTextBox();
                        MessageBox.Show("Eliminado", "Producto");
                        modificarCliente = false;
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
                tc = StaticsFunctions.buscarClientes(textBox4.Text);
                var list = new BindingList<GVCliente>(mandarClientesGV(tc.clientes));
                dataGridView2.DataSource = list;
                reiniciarTextBox();
                modificarCliente = false;
            }
        }

        private void textBox5_KeyUp(object sender, KeyEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
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
    }
}
