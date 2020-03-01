using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Agregados
using MySql.Data.MySqlClient;
using System.Data;
// using System.Data.MySqlClient;


namespace CapaDatos
{
        public class CD_Productos
        {
            private CD_Conexion conexion = new CD_Conexion();

            MySqlDataReader leer;
            DataTable tabla = new DataTable();
            MySqlCommand comando = new MySqlCommand();

            public DataTable Mostrar()
            {

                comando.Connection = conexion.AbrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "bsp_dame_produtos";
                
                leer = comando.ExecuteReader();
                tabla.Load(leer);
                conexion.CerrarConexion();
                return tabla;

            }
    }
}
