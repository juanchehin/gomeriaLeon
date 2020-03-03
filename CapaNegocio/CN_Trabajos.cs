using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;

namespace CapaNegocio
{
    public class CN_Trabajos
    {
        private CD_Trabajos objetoCD = new CD_Trabajos();

        //Método Insertar que llama al método Insertar de la clase
        //de la CapaDatos
        public static string Insertar(string Trabajo, string PrecioUnitario)
        {
            // Console.WriteLine("En insertar , nombre es " + nombre);

            CD_Trabajos Obj = new CD_Trabajos();
            Obj.Trabajo = Trabajo;
            Obj.PrecioUnitario = PrecioUnitario;

            return Obj.Insertar(Obj);
        }

        public DataTable MostrarTrabajos()
        {

            DataTable tabla = new DataTable();
            tabla = objetoCD.Mostrar();
            return tabla;
        }
        public static string Eliminar(int IdTrabajo)
        {
            CD_Trabajos Obj = new CD_Trabajos();
            Obj.IdTrabajo = IdTrabajo;
            return Obj.Eliminar(Obj);
        }
    }
}
