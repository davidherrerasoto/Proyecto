using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Documento
    {
        public int idDocumento { get; set; }
        public int idCliente { get; set; }
        public int idConcepto { get; set; }
        public double subTotal { get; set; }
        public double IVA { get; set; }
        public double total { get; set; }
        public int afectado { get; set; }
        public int idUsuario { get; set; }
        public int idAgente { get; set; }
        public DateTime fecha { get; set; }
    }
}
