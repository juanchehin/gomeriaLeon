// Agregados
using MySql.Data.MySqlClient;
using System;
using System.Data;
// using System.Data.MySqlClient;


namespace CapaDatos
{
    public class CD_Productos
    {
        private int _IdProducto;
        private string _Producto;
        private string _Descripcion;
        private string _Stock;
        private string _EstadoProd;

        private string _TextoBuscar;



        public int IdProducto { get => _IdProducto; set => _IdProducto = value; }
        public string Producto { get => _Producto; set => _Producto = value; }
        public string Descripcion { get => _Descripcion; set => _Descripcion = value; }
        public string Stock { get => _Stock; set => _Stock = value; }
        public string EstadoProd { get => _EstadoProd; set => _EstadoProd = value; }
        public string TextoBuscar { get => _TextoBuscar; set => _TextoBuscar = value; }

        //Constructores
        public CD_Productos()
        {

        }

        public CD_Productos(int IdProducto, string Producto, string Descripcion, string Stock, string EstadoProd, string textobuscar)
        {
            this.IdProducto = IdProducto;
            this.Producto = Producto;
            this.Descripcion = Descripcion;
            this.Stock = Stock;
            this.EstadoProd = EstadoProd;
            this.TextoBuscar = textobuscar;

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
            comando.CommandText = "bsp_dame_produtos";

            leer = comando.ExecuteReader();
            tabla.Load(leer);
            // conexion.CerrarConexion();
            return tabla;

        }

        //Métodos
        //Insertar
        public string Insertar(CD_Productos Producto)
        {
            string rpta = "";
            // SqlConnection SqlCon = new SqlConnection();
            try
            {

                Console.WriteLine("Producto es : " + Producto.Producto);

                //Código
                /*SqlCon.ConnectionString = Conexion.Cn;
                SqlCon.Open();
                //Establecer el Comando
                SqlCommand SqlCmd = new SqlCommand(); */

                comando.Connection = conexion.AbrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "bsp_alta_producto";

                /*SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spinsertar_articulo";
                SqlCmd.CommandType = CommandType.StoredProcedure; */


                MySqlParameter pNombre = new MySqlParameter();
                pNombre.ParameterName = "@pProducto";
                pNombre.MySqlDbType = MySqlDbType.VarChar;
                pNombre.Size = 60;
                pNombre.Value = Producto.Producto;
                comando.Parameters.Add(pNombre);

                Console.WriteLine("pNombre es : " + pNombre.Value);

                MySqlParameter pDescripcion = new MySqlParameter();
                pDescripcion.ParameterName = "@pDescripcion";
                pDescripcion.MySqlDbType = MySqlDbType.VarChar;
                pDescripcion.Size = 60;
                pDescripcion.Value = Producto.Descripcion;
                comando.Parameters.Add(pDescripcion);

                Console.WriteLine("pDescripcion es : " + pDescripcion.Value);

                MySqlParameter pStock = new MySqlParameter();
                pStock.ParameterName = "@pStock";
                pStock.MySqlDbType = MySqlDbType.Int16;
                pStock.Size = 40;
                pStock.Value = Producto.Stock;

                Console.WriteLine("pStock es : " + pStock.Value);

                comando.Parameters.Add(pStock);


                // Console.WriteLine("el comando es : " + comando.CommandText[0]);
                //Ejecutamos nuestro comando

                rpta = comando.ExecuteNonQuery() == 1 ? "OK" : "NO se Ingreso el Registro";


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
    }
}
