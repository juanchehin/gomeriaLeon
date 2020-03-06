using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;

namespace CapaNegocio
{
    public class CN_Compras
    {
        private CD_Compras objetoCD = new CD_Compras();

        //Método Insertar que llama al método Insertar de la clase DArticulo
        //de la CapaDatos
        public static string Insertar(string Producto, string Codigo, string PrecioCompra, string PrecioVenta, string Descripcion, string Cantidad,string Proveedor)
        {
            // No olvidar sumar al stock de un producto si ya existe
            // Console.WriteLine("En insertar , nombre es " + nombre);

            CD_Productos Obj = new CD_Productos();
            Obj.Producto = nombre;
            Obj.Descripcion = Descripcion;
            Obj.Codigo = Codigo;
            Obj.PrecioCompra = PrecioCompra;
            Obj.PrecioVenta = PrecioVenta;
            Obj.Stock = Stock;

            return Obj.Insertar(Obj);
        }

        public DataTable MostrarProd()
        {

            DataTable tabla = new DataTable();
            tabla = objetoCD.Mostrar();
            return tabla;
        }
        // Devuelve solo un producto
        public DataTable MostrarProducto(int IdProducto)
        {

            DataTable tabla = new DataTable();
            tabla = objetoCD.MostrarProducto(IdProducto);
            Console.WriteLine("tabla TableName en capa negocio es : " + tabla.TableName);
            Console.WriteLine("tabla Rows en capa negocio es : " + tabla.Rows);
            return tabla;
        }
        public static string Eliminar(int IdProducto)
        {
            CD_Productos Obj = new CD_Productos();
            Obj.IdProducto = IdProducto;
            return Obj.Eliminar(Obj);
        }


        public static string Editar(int IdProducto, string Producto, string Codigo, string PrecioCompra, string PrecioVenta, string Descripcion, string Stock)
        {
            // Console.WriteLine("Produco.IdProducto es 2 : " + IdProducto);
            CD_Productos Obj = new CD_Productos();
            Obj.IdProducto = IdProducto;

            Obj.Producto = Producto;
            Obj.Codigo = Codigo;
            Obj.PrecioCompra = PrecioCompra;
            Obj.PrecioVenta = PrecioVenta;
            Obj.Descripcion = Descripcion;
            Obj.Stock = Stock;

            // Console.WriteLine("Produco.IdProducto es 3 : " + IdProducto);

            return Obj.Editar(Obj);
        }

        public DataTable BuscarProducto(string textobuscar)
        {
            Console.WriteLine("textobuscar en capa negocio es : " + textobuscar);
            CD_Productos Obj = new CD_Productos();
            Obj.TextoBuscar = textobuscar;
            return Obj.BuscarProducto(Obj);
        }
    }
}
