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
            // Oculto el IdEmpleado. Lo puedo seguir usando como parametro de eliminacion
            dataListadoEmpleados.Columns[1].Visible = false;

            // dataListadoEmpleados.Columns[0].ReadOnly = true;
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Realmente Desea Eliminar los Registros", "Gomeria Leon", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (Opcion == DialogResult.OK)
                {
                    string Codigo;
                    string Rpta = "";

                    foreach (DataGridViewRow row in dataListadoEmpleados.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Codigo = Convert.ToString(row.Cells[1].Value);
                            Rpta = CN_Empleados.Eliminar(Convert.ToInt32(Codigo));

                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se Eliminó Correctamente el registro");
                            }
                            else
                            {
                                this.MensajeError(Rpta);
                            }

                        }
                    }
                    this.MostrarEmpleados();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void dataListadoEmpleados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine("e.ColumnIndex " + e.ColumnIndex);    // Dice que columna se hizo click
            if (e.ColumnIndex == dataListadoEmpleados.Columns["Marcar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)dataListadoEmpleados.Rows[e.RowIndex].Cells["Marcar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
        }

        private void btnAgregarTrabajo_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Realmente Desea agregar un nuevo trabajo", "Gomeria Leon", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                // Si el usuario ingreso que 'si'
                if (Opcion == DialogResult.OK)
                {
                    string Codigo;
                    // string Rpta = "";

                    foreach (DataGridViewRow row in dataListadoEmpleados.Rows)
                    {
                        // Pregunta si esta tildada , Celda = 1
                        if (Convert.ToBoolean(row.Cells[0].Value))  // Convierte a boleano la columna con los checkbox
                        {
                            /* dataListadoEmpleados.Columns["Marcar"].ReadOnly = true;

                            dataListadoEmpleados.Columns["Marcar"].DefaultCellStyle.BackColor = Color.LightGray;

                            dataListadoEmpleados.Columns["Marcar"].DefaultCellStyle.ForeColor = Color.DarkGray;

                            dataListadoEmpleados.Columns["Marcar"].DefaultCellStyle.SelectionBackColor = Color.LightGray;

                            dataListadoEmpleados.Columns["Marcar"].DefaultCellStyle.SelectionForeColor = Color.DarkGray; */

                            Codigo = Convert.ToString(row.Cells[1].Value);  // Transforma a string el valor de la celda 1 (IdEmpleado o el nombre VER BIEN)

                            Console.WriteLine("Codigo es : " + Codigo);

                            // En 'Codigo' tengo el valor de la celda tildada
                            formTrabajosEmpleado frm = new formTrabajosEmpleado(Codigo);
                            frm.MdiParent = this.MdiParent;
                            frm.Show();

                            // Rpta = CN_Empleados.AgregarTrabajo(Convert.ToInt32(Codigo));  // Pasa al metodo ELIMINAR el valor de la columna 1

                            /*if (Rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se Eliminó Correctamente el registro");
                            }
                            else
                            {
                                this.MensajeError(Rpta);
                            }*/

                        }
                    }
                    // this.MostrarEmpleados();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

    }
}
