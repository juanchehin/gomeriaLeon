using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;

namespace CapaNegocio
{
    public class CN_TrabajosEmpleado
    {
        private CD_TrabajosEmpleado objetoCD = new CD_TrabajosEmpleado();

        //Método Insertar que llama al método Insertar de la clase
        //de la CapaDatos
        public static string Insertar(int IdTrabajo, int IdEmpleado,string Fecha,string Cantidad)
        {
            // Console.WriteLine("En insertar , nombre es " + nombre);

            CD_TrabajosEmpleado Obj = new CD_TrabajosEmpleado();
            Obj.IdTrabajo = IdTrabajo;
            Obj.IdEmpleado = IdEmpleado;
            Obj.Fecha = Fecha;
            Obj.Cantidad = Cantidad;

            return Obj.Insertar(Obj);
        }

        public DataTable MostrarTrabajosEmpleado(string IdEmpleado)
        {

            DataTable tabla = new DataTable();
            tabla = objetoCD.Mostrar(IdEmpleado);
            return tabla;
        }
        public static string Eliminar(int IdTrabajo,int IdEmpleado)
        {
            CD_TrabajosEmpleado Obj = new CD_TrabajosEmpleado();
            Obj.IdTrabajo = IdTrabajo;
            Obj.IdEmpleado = IdEmpleado;
            return Obj.Eliminar(Obj);
        }
    }
}
