using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Agregados
using CapaDatos;
using System.Data;
using MySql.Data.MySqlClient;


namespace CapaNegocio
{
    public class CN_Productos
    {
        private CD_Productos objetoCD = new CD_Productos();

        //Método Insertar que llama al método Insertar de la clase DArticulo
        //de la CapaDatos
        public static string Insertar(string nombre, string descripcion,string stock)
        {
            // Console.WriteLine("En insertar , nombre es " + nombre);

            CD_Productos Obj = new CD_Productos();
            Obj.Producto = nombre;
            Obj.Descripcion = descripcion;
            Obj.Stock = stock;

            return Obj.Insertar(Obj);
        }

        public DataTable MostrarProd()
        {

            DataTable tabla = new DataTable();
            tabla = objetoCD.Mostrar();
            return tabla;
        }
    }
}
