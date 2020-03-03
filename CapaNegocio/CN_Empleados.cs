using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Agregados
using CapaDatos;

namespace CapaNegocio
{
    public class CN_Empleados
    {
        private CD_Empleados objetoCD = new CD_Empleados();

        //Método Insertar que llama al método Insertar de la clase DArticulo
        //de la CapaDatos
        public static string Insertar(string Nombre, string Apellidos, string DNI,string Direccion,string Telefono,string FechaNac)
        {
            // Console.WriteLine("En insertar , nombre es " + nombre);

            CD_Empleados Obj = new CD_Empleados();
            Obj.Nombre = Nombre;
            Obj.Apellidos = Apellidos;
            Obj.DNI = DNI;
            Obj.Direccion = Direccion;
            Obj.Telefono = Telefono;
            Obj.FechaNac = FechaNac;

            return Obj.Insertar(Obj);
        }

        public DataTable MostrarEmp()
        {

            DataTable tabla = new DataTable();
            tabla = objetoCD.Mostrar();
            return tabla;
        }
        public static string Eliminar(int IdEmpleado)
        {
            CD_Empleados Obj = new CD_Empleados();
            Obj.IdEmpleado = IdEmpleado;
            return Obj.Eliminar(Obj);
        }


    }
}
