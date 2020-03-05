using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CapaNegocio;

namespace CapaPresentacion
{
    public partial class formAgregarTrabajoEmpleado : Form
    {
        CN_Empleados objetoCN = new CN_Empleados();
        CN_Trabajos objetoCN_trabajos = new CN_Trabajos();

        private int IdEmpleado;
        private DataTable respuesta;
        private DataTable respuesta_trabajos;


        private string Nombre;
        private string Apellidos;


        public formAgregarTrabajoEmpleado(int parametro)
        {
            Console.WriteLine("EL parametro recibido en agregar trabajo es : " + parametro);
            this.IdEmpleado = parametro;
            InitializeComponent();

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void formAgregarTrabajoEmpleado_Load(object sender, EventArgs e)
        {
            this.ActiveControl = cbTrabajos;
            this.MostrarEmpleado(this.IdEmpleado);
            this.CargarTrabajos();
        }
        private void CargarTrabajos()
        {
            respuesta_trabajos = objetoCN_trabajos.MostrarTrabajos();
            cbTrabajos.DataSource = respuesta_trabajos;

            // cbTrabajos.DataSource = Reviewers_table
            cbTrabajos.DisplayMember = "Trabajo";
            cbTrabajos.ValueMember = "IdTrabajo";
        }

        private void MostrarEmpleado(int IdEmpleado)
        {
            Console.WriteLine("EL IdEmpleado recibido en agregar trabajo mostrar emp es : " + IdEmpleado);
            respuesta = objetoCN.MostrarEmpleado(IdEmpleado);

            Console.WriteLine("Respuesta es ; " + respuesta.Rows.Count);
            foreach (DataRow row in respuesta.Rows)
            {
                // Console.WriteLine("row es :" + row["Producto"]);

                // IdProducto = Convert.ToInt32(row["IdProducto"]);
                Nombre = Convert.ToString(row["Nombre"]);
                Apellidos = Convert.ToString(row["Apellidos"]);
                

                lblNombreEmpleado.Text = Nombre + "  " + Apellidos;


            }
        }
    }
}
