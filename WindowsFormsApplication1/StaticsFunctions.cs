using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class StaticsFunctions
    {
        private static String pagina = "https://sgipm.com.mx/5f280ec383b4ba88308dd92f3ac27250/";
        public static void manejarEventos(KeyPressEventArgs e, Form form)
        {
            if (e.KeyChar == 27)
                form.Close();
        }
        
        public static Boolean lanzarDialogYesNo(String titulo, String msg)
        {
            DialogResult dialogResult = MessageBox.Show(msg, titulo, MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                return true;
            }
            else if (dialogResult == DialogResult.No)
            {
                return false;
            }
            return false;
        }

        public static TomarAgentes tomarAgentes()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "tomarAgentes.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
           
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new Producto()
                {
                });
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result,"Resultado");
                TomarAgentes ta = JsonConvert.DeserializeObject<TomarAgentes>(result);
                //MessageBox.Show(rj.estado + "", rj.msg);
                return ta;
            }
        }

        

        public static int enviarProducto(Producto p)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "introducirProducto.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(p);
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result, "Resultado");
                ResultadoJson rj = JsonConvert.DeserializeObject<ResultadoJson>(result);
                //MessageBox.Show(rj.estado + "", rj.msg);
                return rj.estado;
                
            }
        }

        public static int enviarDocumento(Documento d)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "insertDocumento.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(d);
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result, "Resultado");
                ResultadoJson rj = JsonConvert.DeserializeObject<ResultadoJson>(result);
                //MessageBox.Show(rj.estado + "", rj.msg);
                return rj.estado;
            }
        }

        public static int enviarListaPrecio(ListaPrecio listaPrecio)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "introducirListaPrecio.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(listaPrecio);
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result, "Resultado");
                ResultadoJson rj = JsonConvert.DeserializeObject<ResultadoJson>(result);
                //MessageBox.Show(rj.estado + "", rj.msg);
                return rj.estado;
            }
        }

        public static int enviarMovimientos(List<Movimiento> movs)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "insertMovimientos.php");
            var movimientos = new List<Movimiento>();
            for (int i = 0; i < movs.Count; i++)
                movimientos.Add(movs.ElementAt(i));
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(movimientos);
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result, "Resultado");
                ResultadoJson rj = JsonConvert.DeserializeObject<ResultadoJson>(result);
                //MessageBox.Show(rj.estado + "", rj.msg);
                return rj.estado;
            }
        }

       

        public static int enviarServicio(Producto s)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "introducirServicio.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(s);
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result, "Resultado");
                ResultadoJson rj = JsonConvert.DeserializeObject<ResultadoJson>(result);
                //MessageBox.Show(rj.estado + "", rj.msg);
                return rj.estado;

            }
        }


        public static int enviarAgente(Agente a)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "introducirAgente.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(a);
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result, "Resultado");
                ResultadoJson rj = JsonConvert.DeserializeObject<ResultadoJson>(result);
                //MessageBox.Show(rj.estado + "", rj.msg);
                return rj.estado;

            }
        }

        public static int enviarCliente(Cliente c)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "introducirCliente.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
          
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(c);
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                ResultadoJson rj = JsonConvert.DeserializeObject<ResultadoJson>(result);
                //MessageBox.Show(rj.estado + "", rj.msg);
                return rj.estado;
            }
        }

        public static int enviarProv(Proveedor pro)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "introducirProv.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(pro);
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result, "REsultado");
                ResultadoJson rj = JsonConvert.DeserializeObject<ResultadoJson>(result);
                //MessageBox.Show(rj.estado + "", rj.msg);
                return rj.estado;
            }
        }

        public static int modificarProducto(Producto p)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "modificarProducto.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(p);
                //MessageBox.Show(json, "Resultado");
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result, "Resultado");
                ResultadoJson rj = JsonConvert.DeserializeObject<ResultadoJson>(result);
                return rj.estado;
            }
        }

        public static int modificarListaPrecio(ListaPrecio lp)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "modificarListaPrecio.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(lp);
                //MessageBox.Show(json, "Resultado");
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
               // MessageBox.Show(result, "Resultado");
                ResultadoJson rj = JsonConvert.DeserializeObject<ResultadoJson>(result);
                return rj.estado;
            }
        }

        public static int modificarProv(Proveedor pro)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "modificarProv.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(pro);
                //MessageBox.Show(json, "Resultado");
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result, "Resultado");
                ResultadoJson rj = JsonConvert.DeserializeObject<ResultadoJson>(result);
                return rj.estado;
            }
        }

        public static int modificarServicio(Producto s)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "modificarServicio.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(s);
                //MessageBox.Show(json, "Resultado");
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result, "Resultado");
                ResultadoJson rj = JsonConvert.DeserializeObject<ResultadoJson>(result);
                return rj.estado;
            }
        }

        public static int modificarAgente(Agente a)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "modificarAgente.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(a);
                //MessageBox.Show(json, "Resultado");
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result, "Resultado");
                ResultadoJson rj = JsonConvert.DeserializeObject<ResultadoJson>(result);
                return rj.estado;
            }
        }


        public static int modificarCliente(Cliente cl)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "modificarCliente.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new

            StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(cl);
                //MessageBox.Show(json, "Resultado");
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result, "Resultado");
                ResultadoJson rj = JsonConvert.DeserializeObject<ResultadoJson>(result);
                return rj.estado;
            }
        }

        public static int eliminarProducto(Producto p)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "eliminarProducto.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(p);
                //MessageBox.Show(json, "Resultado");
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result, "Resultado");
                ResultadoJson rj = JsonConvert.DeserializeObject<ResultadoJson>(result);
                return rj.estado;
            }
        }

        public static int eliminarServicio(Producto s)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "eliminarProducto.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(s);
                //MessageBox.Show(json, "Resultado");
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result, "Resultado");
                ResultadoJson rj = JsonConvert.DeserializeObject<ResultadoJson>(result);
                return rj.estado;
            }
        }

        public static int eliminarAgente(Agente a)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "eliminarAgente.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(a);
                //MessageBox.Show(json, "Resultado");
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result, "Resultado");
                ResultadoJson rj = JsonConvert.DeserializeObject<ResultadoJson>(result);
                return rj.estado;
            }
        }

        public static int eliminarCliente(Cliente cl)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "eliminarCliente.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(cl);
                //MessageBox.Show(json, "Resultado");
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result, "Resultado");
                ResultadoJson rj = JsonConvert.DeserializeObject<ResultadoJson>(result);
                return rj.estado;
            }
        }

        public static int eliminarProv(Proveedor pro)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "eliminarProv.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(pro);
                //MessageBox.Show(json, "Resultado");
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result, "Resultado");
                ResultadoJson rj = JsonConvert.DeserializeObject<ResultadoJson>(result);
                return rj.estado;
            }
        }


        public static TomarProductos tomarProductosProveedor(int idProveedor)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "tomarProductosPorProveedor.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            Producto p = new Producto();
            p.idProveedor = idProveedor;
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(p);
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result,"Resultado");
                TomarProductos tp = JsonConvert.DeserializeObject<TomarProductos>(result);
                //MessageBox.Show(rj.estado + "", rj.msg);
                return tp;
            }
        }

        public static TomarExistencias tomarExistencias()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "tomarExistencias.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new Producto());
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result,"Resultado");
                TomarExistencias te = JsonConvert.DeserializeObject<TomarExistencias>(result);
                //MessageBox.Show(rj.estado + "", rj.msg);
                return te;
            }
        }

        public static TomarProductos tomarProdsModificarPrecio()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "tomarProductosModificarPrecio.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            /*var RegisteredUsers = new List<Producto>();
            RegisteredUsers.Add(new Producto() { nombre = "David", precio = 1, comision = 1 });
            RegisteredUsers.Add(new Producto() { nombre = "David", precio = 1, comision = 1 });
            RegisteredUsers.Add(new Producto() { nombre = "David", precio = 1, comision = 1 });
            RegisteredUsers.Add(new Producto() { nombre = "David", precio = 1, comision = 1 });
            */
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new Producto()
                {
                });
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result,"Resultado");
                TomarProductos tp = JsonConvert.DeserializeObject<TomarProductos>(result);
                //MessageBox.Show(rj.estado + "", rj.msg);
                return tp;
            }
        }

        public static TomarProductos tomarProds()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "tomarProductos.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            /*var RegisteredUsers = new List<Producto>();
            RegisteredUsers.Add(new Producto() { nombre = "David", precio = 1, comision = 1 });
            RegisteredUsers.Add(new Producto() { nombre = "David", precio = 1, comision = 1 });
            RegisteredUsers.Add(new Producto() { nombre = "David", precio = 1, comision = 1 });
            RegisteredUsers.Add(new Producto() { nombre = "David", precio = 1, comision = 1 });
            */
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new Producto()
                {
                });
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result,"Resultado");
                TomarProductos tp = JsonConvert.DeserializeObject<TomarProductos>(result);
                //MessageBox.Show(rj.estado + "", rj.msg);
                return tp;
            }
        }

        public static TomarProductos tomarServicios()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "tomarServicios.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new Producto()
                {
                });
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result,"Resultado");
                TomarProductos ts = JsonConvert.DeserializeObject<TomarProductos>(result);
                //MessageBox.Show(rj.estado + "", rj.msg);
                return ts;
            }
        }

        public static TomarListaPrecio tomarListaPrecios(Agente ag)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "tomarListaPrecioIdAgente.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(ag);
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result,"Resultado");
                TomarListaPrecio tlp = JsonConvert.DeserializeObject<TomarListaPrecio>(result);
                //MessageBox.Show(rj.estado + "", rj.msg);
                return tlp;
            }
        }

        public static TomarListaPrecio tomarListaPrecios()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "tomarListaPrecio.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new Producto()
                {
                });
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result,"Resultado");
                TomarListaPrecio tlp = JsonConvert.DeserializeObject<TomarListaPrecio>(result);
                //MessageBox.Show(rj.estado + "", rj.msg);
                return tlp;
            }
        }

        public static TomarUnidadMedidas tomarUnidadMedidas()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "tomarUnidadMedidas.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new Producto()
                {
                });
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
               // MessageBox.Show(result,"Resultado");
                TomarUnidadMedidas tum = JsonConvert.DeserializeObject<TomarUnidadMedidas>(result);
                //MessageBox.Show(rj.estado + "", rj.msg);
                return tum;
            }
        }

        public static TomarClientes tomarClientes()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "tomarClientes.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new Cliente()
                {
                });
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result,"Resultado");

                TomarClientes tc = JsonConvert.DeserializeObject<TomarClientes>(result);
                //MessageBox.Show(rj.estado + "", rj.msg);
               // MessageBox.Show(result, tc.ToString());
                return tc;
            }
        }

        public static TomarProveedores tomarProv()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "tomarProv.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new Cliente()
                {
                });
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result,"Resultado");

                TomarProveedores tp = JsonConvert.DeserializeObject<TomarProveedores>(result);
                //MessageBox.Show(rj.estado + "", rj.msg);
                // MessageBox.Show(result, tc.ToString());
                return tp;
            }
        }

        public static TomarProductos buscarProds(String name)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "buscarProductos.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            Producto p = new Producto();
            p.nombre = name;
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(p);
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result,"Resultado");
                TomarProductos tp = new TomarProductos();
                ResultadoJson rj = JsonConvert.DeserializeObject<ResultadoJson>(result);
                if (rj.estado == 1)
                    tp = JsonConvert.DeserializeObject<TomarProductos>(result);
                //MessageBox.Show(rj.estado + "", rj.msg);
                return tp;
            }
        }

        public static TomarProductos buscarProdsProveedor(String name,int idProveedor)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "buscarProductosProveedor.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            Producto p = new Producto();
            p.nombre = name;
            p.idProveedor = idProveedor;
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(p);
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result,"Resultado");
                TomarProductos tp = new TomarProductos();
                ResultadoJson rj = JsonConvert.DeserializeObject<ResultadoJson>(result);
                if (rj.estado == 1)
                    tp = JsonConvert.DeserializeObject<TomarProductos>(result);
                //MessageBox.Show(rj.estado + "", rj.msg);
                return tp;
            }
        }


        public static TomarProductos buscarProds(String name,int idUnidadMedida)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "buscarProductosIdMedida.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            Producto p = new Producto();
            p.nombre = name;
            p.idUnidadMedida = idUnidadMedida;
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(p);
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result,"Resultado");
                TomarProductos tp = new TomarProductos();
                ResultadoJson rj = JsonConvert.DeserializeObject<ResultadoJson>(result);
                if(rj.estado==1)
                    tp = JsonConvert.DeserializeObject<TomarProductos>(result);
                //MessageBox.Show(rj.estado + "", rj.msg);
                return tp;
            }
        }

        public static String buscarProvPorId(int id)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "buscarProvPorId.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            Proveedor p = new Proveedor();
            p.idProveedor = id;
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(p);
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result,"Resultado");
                TomarProveedores tp = new TomarProveedores();
                ResultadoJson rj = JsonConvert.DeserializeObject<ResultadoJson>(result);
                if (rj.estado == 1)
                    tp = JsonConvert.DeserializeObject<TomarProveedores>(result);
                //MessageBox.Show(rj.estado + "", rj.msg);
                return tp.proveedores.ElementAt(0).denCom;
            }
        }

        public static TomarProveedores buscarProv(String name)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "buscarProv.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            Proveedor p = new Proveedor();
            p.denCom = name;
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(p);
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result,"Resultado");
                TomarProveedores tp = new TomarProveedores();
                ResultadoJson rj = JsonConvert.DeserializeObject<ResultadoJson>(result);
                if (rj.estado == 1)
                    tp = JsonConvert.DeserializeObject<TomarProveedores>(result);
                //MessageBox.Show(rj.estado + "", rj.msg);
                return tp;
            }
        }

        public static TomarProductos buscarServicios(String name)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "buscarServicios.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            Producto s = new Producto();
            s.nombre = name;
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(s);
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result,"Resultado");
                TomarProductos ts = new TomarProductos();
                ResultadoJson rj = JsonConvert.DeserializeObject<ResultadoJson>(result);
                if (rj.estado == 1)
                    ts = JsonConvert.DeserializeObject<TomarProductos>(result);
                //MessageBox.Show(rj.estado + "", rj.msg);
                return ts;
            }
        }

        public static TomarClientes buscarClientes(String name)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "buscarClientes.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            Cliente cl = new Cliente();
            cl.denCom = name;
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(cl);
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result,"Resultado");
                TomarClientes tc = new TomarClientes();
                ResultadoJson rj = JsonConvert.DeserializeObject<ResultadoJson>(result);
                if (rj.estado == 1)
                    tc = JsonConvert.DeserializeObject<TomarClientes>(result);
                //MessageBox.Show(rj.estado + "", rj.msg);
                return tc;
            }
        }

        public static TomarClientes buscarClientesPorTelefono(String telefono)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "buscarClientesPorTelefono.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            Cliente cl = new Cliente();
            cl.telefono = telefono;
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(cl);
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result,"Resultado");
                TomarClientes tc = new TomarClientes();
                ResultadoJson rj = JsonConvert.DeserializeObject<ResultadoJson>(result);
                if (rj.estado == 1)
                    tc = JsonConvert.DeserializeObject<TomarClientes>(result);
                //MessageBox.Show(rj.estado + "", rj.msg);
                return tc;
            }
        }

        public static TomarTiempos tomarTiempos()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "tomarTiempos.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new Cliente());
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result,"Resultado");
                TomarTiempos tt = new TomarTiempos();
                ResultadoJson rj = JsonConvert.DeserializeObject<ResultadoJson>(result);
                if (rj.estado == 1)
                    tt = JsonConvert.DeserializeObject<TomarTiempos>(result);
                //MessageBox.Show(rj.estado + "", rj.msg);
                return tt;
            }
        }

        public static TomarAgentes buscarAgentes(String name)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pagina + "buscarAgentes.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            NetworkCredential ClientCredentials = new NetworkCredential();
            Agente ag = new Agente();
            ag.nombre = name;
            using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(ag);
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //MessageBox.Show(result,"Resultado");
                TomarAgentes ta = new TomarAgentes();
                ResultadoJson rj = JsonConvert.DeserializeObject<ResultadoJson>(result);
                if (rj.estado == 1)
                    ta = JsonConvert.DeserializeObject<TomarAgentes>(result);
                //MessageBox.Show(rj.estado + "", rj.msg);
                return ta;
            }
        }

        public static void manejarEventos(KeyEventArgs e,Form form)
        {
            if (e.KeyCode == Keys.Escape)
                    form.Close();
        }
    }
}
