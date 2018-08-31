using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Movimiento
    {
        public int idMovimiento { get; set; }
        public int idConcepto { get; set; }
        public int idProducto { get; set; }
        public int unidades { get; set; }
        public double subTotal { get; set; }
        public double IVA { get; set; }
        public double total { get; set; }
        public int idDocumento { get; set; }
        public int idAgente { get; set; }
        public DateTime fecha { get; set; }
    }
}
