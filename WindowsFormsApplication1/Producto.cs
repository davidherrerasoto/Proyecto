using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Producto
    {
        public int id { get; set; }
        public String nombre { get; set; }
        public float comision { get; set; }
        public float precio { get; set; }
        public String claveSat { get; set; }
        public int idUnidadMedida { get; set; }
        public float IVA { get; set; }
        public String codigo { get; set; }
        public int idProveedor { get; set; }
        public override string ToString()
        {
            return "Producto: " + nombre + "Precio" + precio;
        }

    }
}
