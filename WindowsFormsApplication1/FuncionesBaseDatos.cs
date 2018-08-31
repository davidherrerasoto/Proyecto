using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class FuncionesBaseDatos
    {

        private void introducirBaseDatos(String tabla, Object datos)
        {
            try
            {
                Console.WriteLine("Directory : " + Directory.GetCurrentDirectory());
                SqlConnection myConnection = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\Thosiba\\Documents\\Visual Studio 2015\\Projects\\WindowsFormsApplication1\\WindowsFormsApplication1\\BaseDatos.mdf; Integrated Security = True; Connect Timeout = 30");
                myConnection.Open();
                SqlCommand cmd = new SqlCommand("insert into productos(Nombre,Precio,Comision) values (@Nombre,@Precio,@Comision)", myConnection);
                cmd.Parameters.AddWithValue("@id", 1);
                cmd.Parameters.AddWithValue("@Nombre", "Hola");
                cmd.Parameters.AddWithValue("@Precio", 10.5);
                cmd.Parameters.AddWithValue("@Comision", 3.5);

                cmd.ExecuteNonQuery();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {


                        //Console.WriteLine(documentosPC.ElementAt(i).idDocumento);
                    }
                    dr.Close();
                }
                myConnection.Close();
            }catch(Exception ex)
            {

            }
        }
    }
}
