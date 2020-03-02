using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Agregados
using CapaNegocio;
// using System.DateTime;

namespace CapaPresentacion
{
    public partial class formEmpleados : Form
    {
        CN_Empleados objetoCN = new CN_Empleados();

        private bool IsNuevo = false;
        private bool IsEditar = false;
        public formEmpleados()
        {
            InitializeComponent();
        }

        private void formEmpleados_Load(object sender, EventArgs e)
        {
            MostrarEmpleados();
        }
        private void MostrarEmpleados()
        {
            dataListadoEmpleados.DataSource = objetoCN.MostrarEmp();
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {

        }
        //Limpiar todos los controles del formulario
        private void Limpiar()
        {
            this.txtNombre.Text = string.Empty;
            this.txtApellidos.Text = string.Empty;
            this.txtDNI.Text = string.Empty;
            this.txtTelefono.Text = string.Empty;
            this.txtDireccion.Text = string.Empty;
            //this.monthCalendarFechaNac = monthCalendarFechaNac.TodayDate();
        }

        //Habilitar los botones
        private void Botones()
        {
            if (this.IsNuevo || this.IsEditar) //Alt + 124
            {
                this.Habilitar(true);
                this.btnNuevo.Enabled = false;
                this.btnGuardar.Enabled = true;
                // this.btnEditar.Enabled = false;
                // this.btnCancelar.Enabled = true;
            }
            else
            {
                this.Habilitar(false);
                this.btnNuevo.Enabled = true;
                this.btnGuardar.Enabled = false;
                // this.btnEditar.Enabled = true;
                // this.btnCancelar.Enabled = false;
            }

        }
        //Habilitar los controles del formulario
        private void Habilitar(bool valor)
        {
            // this.txtId.ReadOnly = !valor;
            this.txtNombre.ReadOnly = !valor;
            this.txtApellidos.ReadOnly = !valor;
            this.txtDNI.Enabled = valor;
            this.txtDireccion.Enabled = valor;
            this.txtTelefono.Enabled = valor;
            this.monthCalendarFechaNac.Enabled = valor;
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (this.txtNombre.Text == string.Empty || this.txtApellidos.Text == string.Empty || this.txtDNI.Text == string.Empty || this.txtDireccion.Text == string.Empty || this.txtTelefono.Text == string.Empty || this.monthCalendarFechaNac.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos");
                    /*errorIcono.SetError(txtNombre, "Ingrese un Valor");
                    errorIcono.SetError(txtStock, "Ingrese un Valor");
                    errorIcono.SetError(txtDescripcion, "Ingrese un Valor"); */
                }
                else
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();

                    if (this.IsNuevo)
                    {
                        rpta = CN_Empleados.Insertar(this.txtNombre.Text.Trim(), this.txtApellidos.Text.Trim(),
                            this.txtDNI.Text.Trim(),this.txtDireccion.Text.Trim(), this.txtTelefono.Text.Trim(),
                            this.monthCalendarFechaNac.Text.Trim());
                    }
                    else
                    {
                        /*rpta = CN_Productos.Editar(Convert.ToInt32(this.txtIdarticulo.Text),
                            this.txtCodigo.Text, this.txtNombre.Text.Trim().ToUpper(),
                            this.txtDescripcion.Text.Trim(), imagen, Convert.ToInt32(this.txtIdcategoria.Text),
                            Convert.ToInt32(this.cbIdpresentacion.SelectedValue)); */
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

                    this.IsNuevo = false;
                    this.IsEditar = false;
                    this.Botones();
                    this.Limpiar();
                    this.MostrarEmpleados();


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnNuevo_Click_1(object sender, EventArgs e)
        {
            this.IsNuevo = true;
            this.IsEditar = false;
            this.Botones();
            this.Limpiar();
            this.Habilitar(true);
            this.txtNombre.Focus();
        }
    }
}
