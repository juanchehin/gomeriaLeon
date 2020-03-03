using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Trabajos
    {
        private int _IdTrabajo;
        private string _Trabajo;
        private string _PrecioUnitario;

        private string _TextoBuscar;


        public int IdTrabajo { get => _IdTrabajo; set => _IdTrabajo = value; }
        public string Trabajo { get => _Trabajo; set => _Trabajo = value; }
        public string PrecioUnitario { get => _PrecioUnitario; set => _PrecioUnitario = value; }

        //Constructores
        public CD_Trabajos()
        {

        }

        public CD_Trabajos(int IdTrabajo, string Trabajo, string PrecioUnitario)
        {
            this.IdTrabajo = IdTrabajo;
            this.Trabajo = Trabajo;
            this.PrecioUnitario = PrecioUnitario;
        }

        // ==================================================
        //  Permite devolver todos los productos de la BD
        // ==================================================
        private CD_Conexion conexion = new CD_Conexion();

        MySqlDataReader leer;
        DataTable tabla = new DataTable();
        MySqlCommand comando = new MySqlCommand();


        public DataTable Mostrar()
        {

            comando.Connection = conexion.AbrirConexion();
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "bsp_dame_trabajos";

            leer = comando.ExecuteReader();
            tabla.Load(leer);
            // conexion.CerrarConexion();
            return tabla;

        }

        //Métodos
        //Insertar
        public string Insertar(CD_Trabajos Trabajo)
        {
            string rpta = "";
            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "bsp_alta_trabajo";

                MySqlParameter pTrabajo = new MySqlParameter();
                pTrabajo.ParameterName = "@pTrabajo";
                pTrabajo.MySqlDbType = MySqlDbType.VarChar;
                pTrabajo.Size = 60;
                pTrabajo.Value = Trabajo.Trabajo;
                comando.Parameters.Add(pTrabajo);

                // Console.WriteLine("pNombre es : " + pNombre.Value);

                MySqlParameter pPrecioUnitario = new MySqlParameter();
                pPrecioUnitario.ParameterName = "@pPrecioUnitario";
                pPrecioUnitario.MySqlDbType = MySqlDbType.VarChar;
                pPrecioUnitario.Size = 60;
                pPrecioUnitario.Value = Trabajo.PrecioUnitario;
                comando.Parameters.Add(pPrecioUnitario);

                rpta = comando.ExecuteNonQuery() == 1 ? "OK" : "NO se Ingreso el trabajo";


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
        // Metodo ELIMINAR Empleado (da de baja)
        public string Eliminar(CD_Trabajos Trabajo)
        {
            string rpta = "";
            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "bsp_eliminar_trabajo";

                MySqlParameter pIdTrabajo = new MySqlParameter();
                pIdTrabajo.ParameterName = "@pIdTrabajo";
                pIdTrabajo.MySqlDbType = MySqlDbType.Int32;
                // pIdEmpleado.Size = 60;
                pIdTrabajo.Value = Trabajo.IdTrabajo;
                comando.Parameters.Add(pIdTrabajo);

                //Ejecutamos nuestro comando

                rpta = comando.ExecuteNonQuery() == 1 ? "OK" : "NO se Elimino el Trabajo";

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
