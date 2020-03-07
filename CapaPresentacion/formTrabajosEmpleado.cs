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
    public partial class formTrabajosEmpleado : Form
    {
        private int parametroTE;    // IdEmpleado
        CN_TrabajosEmpleado objetoCN = new CN_TrabajosEmpleado();
        private DateTime FechaFin = DateTime.Today; // Fecha actual
        private DateTime FechaInicio = DateTime.Today.AddDays(-7);// Una semana antes

        

        public formTrabajosEmpleado(int parametro)
        {
            InitializeComponent();
            this.parametroTE = parametro;
            dtFechaFin.Value = DateTime.Today;
            dtFechaInicio.Value = DateTime.Today.AddDays(-7);
        }
        private void formTrabajosEmpleado_Load(object sender, EventArgs e)
        {
            this.FechaInicio = dtFechaInicio.Value;
            this.FechaFin = dtFechaFin.Value;
            MostrarTrabajosEmpleado(this.parametroTE, this.FechaInicio, this.FechaFin);
        }

        // =========================================================
        //  MOSTRAR EMPLEADOS - Carga los trabajos en el DataGridView
        // =========================================================

        private void MostrarTrabajosEmpleado(int IdEmpleado, DateTime FechaInicio,DateTime FechaFin)
        {
            var añoInicio = FechaInicio.Year;
            var mesInicio = FechaInicio.Month;
            var diaInicio = FechaInicio.Day;
            var fechaInicio = añoInicio + "-" + mesInicio + "-" + diaInicio;


            var añoFin = FechaFin.Year;
            var mesFin = FechaFin.Month;
            var diaFin = FechaFin.Day;
            var fechaFin = añoFin + "-" + mesFin + "-" + diaFin;

            dataListadoTrabajosEmpleado.DataSource = objetoCN.MostrarTrabajosEmpleado(IdEmpleado, fechaInicio, fechaFin);
            if(dataListadoTrabajosEmpleado.DataSource == null)
            {
                MensajeError("El empleado no posee trabajos");
                this.Close();
                return;
            }
            Console.WriteLine("dataListadoTrabajosEmpleado.RowCount : " + dataListadoTrabajosEmpleado.RowCount);
            if (dataListadoTrabajosEmpleado.RowCount <= 0)
            {
                dataListadoTrabajosEmpleado.DataSource = null;
                MensajeOk("El empleado no posee trabajos en este rango de fechas");
                return;
            }

            // Multiplico cantidad * PrecioUnitario
            foreach (DataGridViewRow row in dataListadoTrabajosEmpleado.Rows)
            {
                // Console.WriteLine("Entro en el forech");
                row.Cells[0].Value = Convert.ToInt32(row.Cells[6].Value) * Convert.ToInt32(row.Cells[7].Value);
                // Convert.ToInt32(row.Cells[6].Value) * Convert.ToInt32(row.Cells[7].Value);
            }

            // Oculto el IdProducto. Lo puedo seguir usando como parametro de eliminacion
            lblApellidoNombre.Text = dataListadoTrabajosEmpleado.Rows[0].Cells[2].Value.ToString() + " , " + dataListadoTrabajosEmpleado.Rows[0].Cells[1].Value.ToString();
            lblDireccion.Text = dataListadoTrabajosEmpleado.Rows[0].Cells[3].Value.ToString();
            lblTelefono.Text = dataListadoTrabajosEmpleado.Rows[0].Cells[4].Value.ToString();

            dataListadoTrabajosEmpleado.Columns[1].Visible = false;
            dataListadoTrabajosEmpleado.Columns[2].Visible = false;
            dataListadoTrabajosEmpleado.Columns[3].Visible = false;
            dataListadoTrabajosEmpleado.Columns[4].Visible = false;

            
            // Nota : Nombre es row.Cells[1].Value
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            this.FechaInicio = dtFechaInicio.Value;
            this.FechaFin = dtFechaFin.Value;
            MostrarTrabajosEmpleado(this.parametroTE, this.FechaInicio,this.FechaFin);

        }

        private void btnEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            DialogResult Opcion;
            Opcion = MessageBox.Show("Realmente Desea Eliminar el/los productos", "Gomeria Leon", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (Opcion == DialogResult.OK)
            {
                string Codigo;
                string Rpta = "";

                foreach (DataGridViewRow row in dataListadoTrabajosEmpleado.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[0].Value))
                    {
                        Codigo = Convert.ToString(row.Cells[1].Value);
                        Rpta = CN_Productos.Eliminar(Convert.ToInt32(Codigo));

                        if (Rpta.Equals("OK"))
                        {
                            this.MensajeOk("Se Eliminó Correctamente el/los trabajo del empleado");
                        }
                        else
                        {
                            this.MensajeError(Rpta);
                        }

                    }
                }
                // this.MostrarTrabajosEmpleado(this.parametroTE);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message + ex.StackTrace);
        }
    }

        private void lblApellidoNombre_Click(object sender, EventArgs e)
        {

        }

        //Mostrar Mensaje de Confirmación
        private void MensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje, "Gomeria Leon", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }


        //Mostrar Mensaje de Error
        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Gomeria Leon", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


    }
}
