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
    public partial class formNuevoEditarEmpleado : Form
    {
        CN_Empleados objetoCN = new CN_Empleados();
        DataTable respuesta;
        // int parametroActual;
        bool bandera;
        bool IsNuevo = false;
        bool IsEditar = false;

        private int IdEmpleado;
        private string Nombre;
        private string Apellidos;
        private string DNI;
        private string Direccion;
        private string Telefono;
        private DateTime FechaNac;

        public formNuevoEditarEmpleado(int parametro, bool IsNuevoEditar)
        {
            InitializeComponent();
            this.IdEmpleado = parametro;
            this.bandera = IsNuevoEditar;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MostrarEmpleado(int IdEmpleado)
        {
            respuesta = objetoCN.MostrarEmpleado(IdEmpleado);

            Console.WriteLine("Respuesta es ; " + respuesta.Rows.Count);
            foreach (DataRow row in respuesta.Rows)
            {
                Console.WriteLine("row es :" + row["Nombre"]);

                IdEmpleado = Convert.ToInt32(row["IdEmpleado"]);
                Nombre = Convert.ToString(row["Nombre"]);
                Apellidos = Convert.ToString(row["Apellidos"]);
                DNI = Convert.ToString(row["DNI"]);
                Direccion = Convert.ToString(row["Direccion"]);
                Telefono = Convert.ToString(row["Telefono"]);
                FechaNac = Convert.ToDateTime(row["Fecha de nacimiento"]);


                txtNombre.Text = Nombre;

                txtApellidos.Text = Apellidos;
                txtDNI.Text = DNI;
                txtDireccion.Text = Direccion;
                txtTelefono.Text = Telefono;
                dtFechaNac.Value = FechaNac;

            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (this.txtNombre.Text == string.Empty || this.txtApellidos.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos");
                }
                else
                {
                    if (this.IsNuevo)
                    {
                        rpta = CN_Empleados.Insertar(this.txtNombre.Text.Trim(), this.txtApellidos.Text.Trim(), this.txtDNI.Text.Trim(),
                            this.txtDireccion.Text.Trim(),this.txtTelefono.Text.Trim(),this.dtFechaNac.Value);
                    }
                    else
                    {
                        rpta = CN_Empleados.Editar(this.IdEmpleado, this.txtNombre.Text.Trim(), this.txtApellidos.Text.Trim(),
                            this.txtDNI.Text.Trim(),this.txtDireccion.Text.Trim(), this.txtTelefono.Text.Trim(), this.dtFechaNac.Value);
                    }

                    if (rpta.Equals("OK"))
                    {
                        if (this.IsNuevo)
                        {
                            this.MensajeOk("Se Insertó de forma correcta el registro");
                        }
                        else
                        {
                            this.MensajeOk("Se Actualizó de forma correcta el registro");
                        }
                    }
                    else
                    {
                        this.MensajeError(rpta);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
            this.Close();
        }


        private void MensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje, "Gomeria Leon", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }


        //Mostrar Mensaje de Error
        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Gomeria Leon", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void formNuevoEditarEmpleado_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtNombre;
            if (this.bandera)
            {
                lblEditarNuevo.Text = "Nuevo";
                // this.MostrarProducto(this.IdProducto);
                this.IsNuevo = true;
                this.IsEditar = false;
            }
            else
            {
                lblEditarNuevo.Text = "Editar";
                this.IsNuevo = false;
                this.IsEditar = true;
                this.MostrarEmpleado(this.IdEmpleado);
            }
        }
    }
}
