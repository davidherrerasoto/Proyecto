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
    public partial class Seleccionar : Form
    {
        TomarAgentes ta = StaticsFunctions.tomarAgentes();
        public Cliente cl {get;set;}
        public Seleccionar()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        public void ini(TomarClientes tc)
        {
            var list = new BindingList<GVCliente>(mandarClientesGV(tc.clientes));
            dataGridView1.DataSource = list;
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
                        RFC = cls.ElementAt(i).repLeg,
                        RepLeg = cls.ElementAt(i).repLeg,
                        Agente = this.buscarNombreAgente(cls.ElementAt(i).idAgente)
                    });
                }
            }

            return c;
        }

        String buscarNombreAgente(int id)
        {
            for (int i = 0; i < ta.agentes.Count; i++)
                if (ta.agentes.ElementAt(i).idAgente == id)
                    return ta.agentes.ElementAt(i).nombre;
            return "";
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                
            }
        }

        public Cliente tomarCliente()
        {
            return cl;
        }
    }
}
