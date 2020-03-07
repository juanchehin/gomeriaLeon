using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_TrabajosEmpleado
    {
        private string _Trabajo;
        private int _IdEmpleado;
        private string _Fecha;
        private string _Cantidad;

        private string _TextoBuscar;

        public int IdEmpleado { get => _IdEmpleado; set => _IdEmpleado = value; }
        public string Fecha { get => _Fecha; set => _Fecha = value; }
        public string Cantidad { get => _Cantidad; set => _Cantidad = value; }
        public string Trabajo { get => _Trabajo; set => _Trabajo = value; }

        //Constructores
        public CD_TrabajosEmpleado()
        {

        }

        public CD_TrabajosEmpleado(string Trabajo, int IdEmpleado, string Fecha,string Cantidad)
        {
            this.Trabajo = Trabajo;
            this.IdEmpleado = IdEmpleado;
            this.Fecha = Fecha;
            this.Cantidad = Cantidad;
        }

        // ==================================================
        //  Permite devolver todos los trabajos de un empleado de la BD
        // ==================================================
        private CD_Conexion conexion = new CD_Conexion();

        string respuesta;
        MySqlDataReader leer;
        DataTable tabla = new DataTable();
        MySqlCommand comando = new MySqlCommand();



        public DataTable Mostrar(int IdEmpleado)
        {

            comando.Connection = conexion.AbrirConexion();
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "bsp_dame_trabajos_empleado";

            MySqlParameter pIdEmpleado = new MySqlParameter();
            pIdEmpleado.ParameterName = "@pIdEmpleado";
            pIdEmpleado.MySqlDbType = MySqlDbType.Int32;
            // pIdTrabajo.Size = 60;
            pIdEmpleado.Value = IdEmpleado;
            comando.Parameters.Add(pIdEmpleado);

            leer = comando.ExecuteReader();
            tabla.Load(leer);

            respuesta = (string)comando.ExecuteScalar();
            Console.WriteLine("EL valor devuelto por la BD ExecuteScalar() : " + comando.ExecuteScalar());

            if (respuesta == "El empleado no posee trabajos")
            {
                tabla = null;
                return tabla;
            }
                        
            conexion.CerrarConexion();
            return tabla;

        }

        //Métodos
        //Insertar
        public string Insertar(CD_TrabajosEmpleado TrabajosEmpleado)
        {
            string rpta = "";
            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "bsp_alta_trabajo_empleado";

                MySqlParameter pTrabajo = new MySqlParameter();
                pTrabajo.ParameterName = "@pTrabajo";
                pTrabajo.MySqlDbType = MySqlDbType.VarChar;
                pTrabajo.Size = 60;
                pTrabajo.Value = TrabajosEmpleado.Trabajo;
                comando.Parameters.Add(pTrabajo);

                // Console.WriteLine("pNombre es : " + pNombre.Value);

                MySqlParameter pIdEmpleado = new MySqlParameter();
                pIdEmpleado.ParameterName = "@pIdEmpleado";
                pIdEmpleado.MySqlDbType = MySqlDbType.Int32;
                // pIdTrabajo.Size = 60;
                pIdEmpleado.Value = TrabajosEmpleado.IdEmpleado;
                comando.Parameters.Add(pIdEmpleado);

                MySqlParameter pFecha = new MySqlParameter();
                pFecha.ParameterName = "@pFecha";
                pFecha.MySqlDbType = MySqlDbType.String;
                // pIdTrabajo.Size = 60;
                pFecha.Value = TrabajosEmpleado.Fecha;
                comando.Parameters.Add(pFecha);

                MySqlParameter pCantidad = new MySqlParameter();
                pCantidad.ParameterName = "@pCantidad";
                pCantidad.MySqlDbType = MySqlDbType.Int32;
                // pIdTrabajo.Size = 60;
                pCantidad.Value = TrabajosEmpleado.Cantidad;
                comando.Parameters.Add(pCantidad);

                rpta = comando.ExecuteNonQuery() == 1 ? "OK" : "NO se Ingreso el trabajo del empleado";


            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                conexion.CerrarConexion();
            }
            return rpta;

        }
        // Metodo ELIMINAR un trabajo de un Empleado (da de baja)
        public string Eliminar(CD_TrabajosEmpleado TrabajosEmpleado)
        {
            string rpta = "";
            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "bsp_eliminar_trabajo_empleado";

                MySqlParameter pIdTrabajo = new MySqlParameter();
                pIdTrabajo.ParameterName = "@pIdTrabajo";
                pIdTrabajo.MySqlDbType = MySqlDbType.Int32;
                // pIdEmpleado.Size = 60;
                pIdTrabajo.Value = TrabajosEmpleado.IdTrabajo;
                comando.Parameters.Add(pIdTrabajo);

                MySqlParameter pIdEmpleado = new MySqlParameter();
                pIdEmpleado.ParameterName = "@pIdEmpleado";
                pIdEmpleado.MySqlDbType = MySqlDbType.Int32;
                // pIdEmpleado.Size = 60;
                pIdEmpleado.Value = TrabajosEmpleado.IdEmpleado;
                comando.Parameters.Add(pIdEmpleado);

                //Ejecutamos nuestro comando

                rpta = comando.ExecuteNonQuery() == 1 ? "OK" : "NO se Elimino el Trabajo del empleado";

            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                //if (conexion. == ConnectionState.Open) 
                conexion.CerrarConexion();
            }
            return rpta;
        }
    }
}
