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
    public partial class AgregarAgente : Form
    {
        Boolean modificarAgente = false;
        TomarAgentes ta = null;
        int indiceAModificar = -1;
        public AgregarAgente()
        {
            InitializeComponent();
            reiniciarGridView();
            this.CenterToScreen();
        }

        private bool validarTextBox()
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox1.Text != " " 
                && textBox2.Text != "." && textBox3.Text != "." && textBox5.Text != " "
                && textBox5.Text != "." && textBox5.Text != "")
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
                return false;
            }
        }

        private void reiniciarTextBox()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            textBox1.BackColor = Color.White;
            textBox2.BackColor = Color.White;
            textBox3.BackColor = Color.White;
            textBox5.BackColor = Color.White;
            button1.Text = "Alta Cliente";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (validarTextBox())
            {
                Agente ag = new Agente();
                ag.codigo = textBox5.Text;
                ag.nombre = textBox1.Text;
                ag.telefono = textBox2.Text;
                ag.correo = textBox3.Text;
                agregarAgente(ag);
            }
        }

        List<GVAgente> mandarAgentesGV(List<Agente> ags)
        {
            List<GVAgente> c = new List<GVAgente>();
            if (ags != null)
            {
                for (int i = 0; i < ags.Count; i++)
                {
                    c.Add(new GVAgente()
                    {
                        Codigo = ags.ElementAt(i).codigo,
                        Nombre = ags.ElementAt(i).nombre,
                        Telefono = ags.ElementAt(i).telefono,
                        Correo = ags.ElementAt(i).correo
                    });
                }
            }

            return c;
        }

        private void agregarAgente(Agente ag)
        {
            if (!modificarAgente)
            {
                if (StaticsFunctions.enviarAgente(ag) == 1)
                {
                    MessageBox.Show("Agregado", "Cliente");
                }
                else
                {
                    MessageBox.Show("Hubo un problema", "Warning");
                }
                ta = StaticsFunctions.tomarAgentes();
                var list = new BindingList<GVAgente>(mandarAgentesGV(ta.agentes));
                dataGridView2.DataSource = list;
                if (ta.estado == 1)
                {
                    reiniciarTextBox();
                }
            }
            else
            {
                if (validarTextBox())
                {
                    ag.idAgente = this.ta.agentes.ElementAt(indiceAModificar).idAgente;
                    if (StaticsFunctions.modificarAgente(ag) == 1)
                    {
                        //insertarBaseDatos(p);
                        reiniciarTextBox();
                        reiniciarGridView();
                        modificarAgente = false;
                        MessageBox.Show("Agente Modificado", "Agente");
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
            ta = StaticsFunctions.tomarAgentes();
            var list = new BindingList<GVAgente>(mandarAgentesGV(ta.agentes));
            dataGridView2.DataSource = list;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            reiniciarTextBox();
            if (e.RowIndex >= 0)
            {
                textBox1.Text = ta.agentes.ElementAt(e.RowIndex).nombre;
                textBox2.Text = ta.agentes.ElementAt(e.RowIndex).telefono;
                textBox3.Text = ta.agentes.ElementAt(e.RowIndex).correo;
                textBox5.Text = ta.agentes.ElementAt(e.RowIndex).codigo;
                button1.Text = "Modificar";
                modificarAgente = true;
                indiceAModificar = e.RowIndex;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            modificarAgente = false;
            reiniciarGridView();
            reiniciarTextBox();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (modificarAgente)
            {
                if (StaticsFunctions.lanzarDialogYesNo("Eliminar", "Esta Seguro"))
                {
                    Agente a = new Agente();
                    a.idAgente = ta.agentes.ElementAt(indiceAModificar).idAgente;
                    if (StaticsFunctions.eliminarAgente(a) == 1)
                    {
                        reiniciarGridView();
                        reiniciarTextBox();
                        modificarAgente = false;
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
                ta = StaticsFunctions.buscarAgentes(textBox4.Text);
                var list = new BindingList<GVAgente>(mandarAgentesGV(ta.agentes));
                dataGridView2.DataSource = list;
                reiniciarTextBox();
                modificarAgente = false;
            }
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

        private void dataGridView2_KeyPress(object sender, KeyPressEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            StaticsFunctions.manejarEventos(e, this);
        }
    }
}
