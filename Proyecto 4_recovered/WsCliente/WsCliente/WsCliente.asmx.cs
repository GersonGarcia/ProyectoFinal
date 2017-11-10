using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WsCliente
{
    /// <summary>
    /// Descripción breve de WsCliente
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WsCliente : System.Web.Services.WebService
    {
        ClsConexion conexion = ClsConexion.getInstancia();
        [WebMethod]
        public string registrarCliente(String nit, String nombre, String apellido, String telefono, String direccion, String correo)
        {
            if (!CompararExistenteCliente(nit)) {
                try
                {
                    conexion.ejecutarQuery(" insert into ClienteSistema (nitCliente, nombreCliente, apellidoCliente, telefonoCliente, direccionCliente, correoCliente) " +
                                           " VALUES ('" + nit + "', '" + nombre + "','" + apellido + "', '" + telefono + "', '" + direccion + "', '" + correo + "') ");
                    return "0000 - Cliente registrado con exito";
                }
                catch (Exception ex)
                {
                    return "0001 - Ocurrio un error : " + ex.Message;
                }
            }
            else {
                return "nit Existente";  
            }
           
        }

       
        public bool CompararExistenteCliente(String nit) {
            try
            {
                DataTable tabla = new DataTable();                
                tabla = conexion.hacerConsulta("select top 1 * from ClienteSistema where nitCliente = '" + nit + "'");
                if (tabla.Rows.Count > 0)
                {
                    return true;
                }
                else { return false; }
                
            }
            catch (Exception ex)
            {
                Console.Write("0002 - Ocurrio un error : " + ex.Message);
                return false;
            }
        }

        [WebMethod]
        public DataTable ObtenerClientes() {
            
                DataTable tabla = new DataTable();                
                tabla = conexion.hacerConsulta("select * from ClienteSistema ");
                return tabla;

        }

        [WebMethod]
        public string verificarExistenciaCliente(String nit)
        {
            try
            {
                DataTable tabla = new DataTable();
                String resultado = "0000 - No hay informacion del cliente";
                tabla = conexion.hacerConsulta("select top 1 * from ClienteSistema where nitCliente = '" + nit + "'");
                if (tabla.Rows.Count > 0)
                {
                    foreach (DataRow row in tabla.Rows)
                    {
                        resultado = row["codigoCliente"].ToString() + ";" + row["nitCliente"].ToString() + ";" + row["nombreCliente"].ToString() + ";" + row["apellidoCliente"].ToString() + ";"+  row["telefonoCliente"].ToString() + ";"  + row["DireccionCliente"].ToString() + ";"+ row["correoCliente"].ToString() ;
                    }
                }
                return resultado;
            }
            catch (Exception ex)
            {
                return "0001 - Ocurrio un error : " + ex.Message;
            }
        }


    }
}
