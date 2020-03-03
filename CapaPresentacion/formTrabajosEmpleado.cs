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
        private string parametroTE;
        public formTrabajosEmpleado(string parametro)
        {
            this.parametroTE = parametro;
            Console.WriteLine("this.parametroTE es : " + this.parametroTE);
            InitializeComponent();
        }

        CN_TrabajosEmpleado objetoCN = new CN_TrabajosEmpleado();

        private bool IsNuevo = false;
        private bool IsEditar = false;

        private void MostrarTrabajosEmpleado(string IdEmpleado)
        {
            dataListadoTrabajosEmpleado.DataSource = objetoCN.MostrarTrabajosEmpleado(IdEmpleado);
            // Oculto el IdProducto. Lo puedo seguir usando como parametro de eliminacion
            lblApellidoNombre.Text = dataListadoTrabajosEmpleado.Rows[0].Cells[1].Value.ToString() + "  " + dataListadoTrabajosEmpleado.Rows[0].Cells[0].Value.ToString();
            lblDireccion.Text = dataListadoTrabajosEmpleado.Rows[0].Cells[2].Value.ToString();
            lblTelefono.Text = dataListadoTrabajosEmpleado.Rows[0].Cells[3].Value.ToString();

            dataListadoTrabajosEmpleado.Columns[0].Visible = false;
            dataListadoTrabajosEmpleado.Columns[1].Visible = false;
            dataListadoTrabajosEmpleado.Columns[2].Visible = false;
            dataListadoTrabajosEmpleado.Columns[3].Visible = false;

            


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

        }
        //Limpiar todos los controles del formulario
        private void Limpiar()
        {
            /*this.comboBoxTrabajos.Text = string.Empty;
            this.txtCantidad.Text = string.Empty;*/
            
        }
        
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.IsNuevo = true;
            this.IsEditar = false;
            this.Botones();
            this.Limpiar();
            this.Habilitar(true);
            //this.comboBoxTrabajos.Focus();
        }
        //Habilitar los botones
        private void Botones()
        {
            if (this.IsNuevo || this.IsEditar) //Alt + 124
            {
                this.Habilitar(true);
                //this.btnNuevo.Enabled = false;
                //this.btnGuardar.Enabled = true;
                //this.btnEditar.Enabled = false;
                //this.btnCancelar.Enabled = true;
            }
            else
            {
                this.Habilitar(false);
                //this.btnNuevo.Enabled = true;
                //this.btnGuardar.Enabled = false;
                //this.btnEditar.Enabled = true;
                //this.btnCancelar.Enabled = false;
            }

        }
        //Habilitar los controles del formulario
        private void Habilitar(bool valor)
        {
            // this.txtId.ReadOnly = !valor;
            //this.comboBoxTrabajos.Enabled = !valor;
            //this.txtCantidad.ReadOnly = !valor;
            //this.txtStock.Enabled = valor;
        }
        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            /*try
            {
                string rpta = "";
                if (this.comboBoxTrabajos.SelectedIndex == -1 || this.txtCantidad.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos");
                    /*errorIcono.SetError(txtNombre, "Ingrese un Valor");
                    errorIcono.SetError(txtStock, "Ingrese un Valor");
                    errorIcono.SetError(txtDescripcion, "Ingrese un Valor"); */
            /*}
            else
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();

                if (this.IsNuevo)
                {
                    // rpta = CN_TrabajosEmpleado.Insertar(this.comboBoxTrabajos.SelectedItem, this.Empl .Text.Trim(), this.txtDescripcion.Text.Trim(),
                    //     this.txtStock.Text.Trim());
                }
                else
                {
                    /*rpta = CN_Productos.Editar(Convert.ToInt32(this.txtIdarticulo.Text),
                        this.txtCodigo.Text, this.txtNombre.Text.Trim().ToUpper(),
                        this.txtDescripcion.Text.Trim(), imagen, Convert.ToInt32(this.txtIdcategoria.Text),
                        Convert.ToInt32(this.cbIdpresentacion.SelectedValue)); */
            /*}

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
            // Enviar parametro recibido desde el otro formulario
            this.MostrarTrabajosEmpleado(this.parametroTE);
            }
        }
        catch (Exception ex)
        {
        MessageBox.Show(ex.Message + ex.StackTrace);
        }*/

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

        private void dataListadoProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataListadoTrabajosEmpleado.Columns["Marcar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)dataListadoTrabajosEmpleado.Rows[e.RowIndex].Cells["Marcar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
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
                this.MostrarTrabajosEmpleado(this.parametroTE);
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

        private void formTrabajosEmpleado_Load(object sender, EventArgs e)
        {
            MostrarTrabajosEmpleado(this.parametroTE);
            // this.lblApellidoNombre = dataListadoTrabajosEmpleado.;
        }
    }
}
