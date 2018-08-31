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
    public partial class AgregarProveedor : Form
    {
        TomarProveedores tp = null;
        Boolean modificarProveedor = false;
        int indiceAModificar = -1;

        public AgregarProveedor()
        {
            InitializeComponent();
            tp = StaticsFunctions.tomarProv();
            var list = new BindingList<GVProveedor>(mandarProvGV(tp.proveedores));
            dataGridView2.DataSource = list;
            //comboBox1.SelectedIndex = 0;
            reiniciarGridView();
            this.CenterToScreen();
        }

        private void reiniciarGridView()
        {
            tp = StaticsFunctions.tomarProv();
            var list = new BindingList<GVProveedor>(mandarProvGV(tp.proveedores));
            dataGridView2.DataSource = list;
        }


        private bool validarTextBox()
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox1.Text != " " && textBox2.Text != "." && textBox3.Text != "."
                && textBox5.Text != "" && textBox5.Text != " " && textBox5.Text != ".")
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

        List<GVProveedor> mandarProvGV(List<Proveedor> pro)
        {
            List<GVProveedor> c = new List<GVProveedor>();
            if (pro != null)
            {
                for (int i = 0; i < pro.Count; i++)
                {
                    c.Add(new GVProveedor()
                    {
                        Codigo = pro.ElementAt(i).codigo,
                        DenCom = pro.ElementAt(i).denCom,
                        RFC = pro.ElementAt(i).rfc,
                        RepLeg = pro.ElementAt(i).repLeg
                    });
                }
            }

            return c;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (validarTextBox())
            {
                Proveedor pro = new Proveedor();
                pro.codigo = textBox5.Text;
                pro.denCom = this.textBox1.Text;
                pro.rfc = this.textBox2.Text;
                pro.repLeg = this.textBox3.Text;
                agregarProv(pro);
            }
        }

        private void agregarProv(Proveedor pro)
        {
            if (!modificarProveedor)
            {
                if (StaticsFunctions.enviarProv(pro) == 1)
                {
                    MessageBox.Show("Agregado", "Proveedor");
                }
                else
                {
                    MessageBox.Show("Hubo un problema", "Warning");
                }
                tp = StaticsFunctions.tomarProv();
                var list = new BindingList<GVProveedor>(mandarProvGV(tp.proveedores));
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
                    pro.idProveedor = this.tp.proveedores.ElementAt(this.indiceAModificar).idProveedor;
                    if (StaticsFunctions.modificarProv(pro) == 1)
                    {
                        //insertarBaseDatos(p);
                        reiniciarTextBox();
                        reiniciarGridView();
                        modificarProveedor = false;
                        MessageBox.Show("Proveedor Modificado", "Proveedor");
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
                textBox1.Text = tp.proveedores.ElementAt(e.RowIndex).denCom;
                textBox2.Text = tp.proveedores.ElementAt(e.RowIndex).rfc;
                textBox3.Text = tp.proveedores.ElementAt(e.RowIndex).repLeg;
                textBox5.Text = tp.proveedores.ElementAt(e.RowIndex).codigo;
                //comboBox1.SelectedIndex = tc.clientes.ElementAt(e.RowIndex).idClasificacion;
                button1.Text = "Modificar";
                modificarProveedor = true;
                indiceAModificar = e.RowIndex;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            reiniciarTextBox();
            modificarProveedor = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (modificarProveedor)
            {
                if (StaticsFunctions.lanzarDialogYesNo("Eliminar", "Esta Seguro"))
                {
                    Proveedor pro = new Proveedor();
                    pro.idProveedor = tp.proveedores.ElementAt(indiceAModificar).idProveedor;
                    if (StaticsFunctions.eliminarProv(pro) == 1)
                    {
                        reiniciarGridView();
                        reiniciarTextBox();
                        MessageBox.Show("Eliminado", "Proveedor");
                        modificarProveedor = false;
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
                tp = StaticsFunctions.buscarProv(textBox4.Text);
                var list = new BindingList<GVProveedor>(mandarProvGV(tp.proveedores));
                dataGridView2.DataSource = list;
                reiniciarTextBox();
                modificarProveedor = false;
            }
        }

        private void dataGridView2_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
