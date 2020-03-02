﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Empleados
    {
        private int _IdEmpleado;
        private string _Nombre;
        private string _Apellidos;
        private string _DNI;
        private string _EstadoEmp;
        private string _Direccion;
        private string _Telefono;
        private string _FechaNac;

        private string _TextoBuscar;



        public int IdEmpleado { get => _IdEmpleado; set => _IdEmpleado = value; }
        public string Nombre { get => _Nombre; set => _Nombre = value; }
        public string Apellidos { get => _Apellidos; set => _Apellidos = value; }
        public string DNI { get => _DNI; set => _DNI = value; }
        public string EstadoEmp { get => _EstadoEmp; set => _EstadoEmp = value; }
        public string Direccion { get => _Direccion; set => _Direccion = value; }
        public string Telefono { get => _Telefono; set => _Telefono = value; }
        public string FechaNac { get => _FechaNac; set => _FechaNac = value; }

        //Constructores
        public CD_Empleados()
        {

        }

        public CD_Empleados(int IdEmpleado, string Nombre, string Apellidos, string DNI, string EstadoEmp, string Direccion,string Telefono,string FechaNac)
        {
            this.IdEmpleado = IdEmpleado;
            this.Nombre = Nombre;
            this.Apellidos = Apellidos;
            this.Apellidos = Apellidos;
            this.DNI = DNI;
            this.EstadoEmp = EstadoEmp;
            this.Direccion = Direccion;
            this.Telefono = Telefono;
            this.FechaNac = FechaNac;

        }

        // ==================================================
        //  Permite devolver todos los empleados activos de la BD
        // ==================================================
        private CD_Conexion conexion = new CD_Conexion();

        MySqlDataReader leer;
        DataTable tabla = new DataTable();
        MySqlCommand comando = new MySqlCommand();


        public DataTable Mostrar()
        {

            comando.Connection = conexion.AbrirConexion();
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "bsp_dame_empleados";

            leer = comando.ExecuteReader();
            tabla.Load(leer);
            // conexion.CerrarConexion();
            return tabla;

        }

        //Métodos
        //Insertar
        public string Insertar(CD_Empleados Empleado)
        {
            string rpta = "";
            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "bsp_alta_empleado";

                MySqlParameter pNombre = new MySqlParameter();
                pNombre.ParameterName = "@pNombre";
                pNombre.MySqlDbType = MySqlDbType.VarChar;
                pNombre.Size = 60;
                pNombre.Value = Empleado.Nombre;
                comando.Parameters.Add(pNombre);

                // Console.WriteLine("pNombre es : " + pNombre.Value);

                MySqlParameter pApellidos = new MySqlParameter();
                pApellidos.ParameterName = "@pApellidos";
                pApellidos.MySqlDbType = MySqlDbType.VarChar;
                pApellidos.Size = 60;
                pApellidos.Value = Empleado.Apellidos;
                comando.Parameters.Add(pApellidos);

                MySqlParameter pDNI = new MySqlParameter();
                pDNI.ParameterName = "@pDNI";
                pDNI.MySqlDbType = MySqlDbType.VarChar;
                pDNI.Size = 45;
                pDNI.Value = Empleado.DNI;
                comando.Parameters.Add(pDNI);

                MySqlParameter pDireccion = new MySqlParameter();
                pDireccion.ParameterName = "@pDireccion";
                pDireccion.MySqlDbType = MySqlDbType.VarChar;
                pDireccion.Size = 40;
                pDireccion.Value = Empleado.Direccion;
                comando.Parameters.Add(pDireccion);

                MySqlParameter pTelefono = new MySqlParameter();
                pTelefono.ParameterName = "@pTelefono";
                pTelefono.MySqlDbType = MySqlDbType.VarChar;
                pTelefono.Size = 15;
                pTelefono.Value = Empleado.Telefono;
                comando.Parameters.Add(pTelefono);

                MySqlParameter pFechaNac = new MySqlParameter();
                pFechaNac.ParameterName = "@pFechaNac";
                pFechaNac.MySqlDbType = MySqlDbType.Date;
                // pFechaNac.Size = 40;
                pFechaNac.Value = Empleado.FechaNac;
                comando.Parameters.Add(pFechaNac);


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
