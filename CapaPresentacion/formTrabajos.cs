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
    public partial class formTrabajos : Form
    {
        CN_Trabajos objetoCN = new CN_Trabajos();

        private bool IsNuevo = false;
        private bool IsEditar = false;
        public formTrabajos()
        {
            InitializeComponent();
            // lblTotalProductos.Text = "Total de Productos: " + Convert.ToString(dataListadoProductos.Rows.Count);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MostrarTrabajos();
        }
        private void MostrarTrabajos()
        {
            dataListadoTrabajos.DataSource = objetoCN.MostrarTrabajos();
            // Oculto el IdProducto. Lo puedo seguir usando como parametro de eliminacion
            dataListadoTrabajos.Columns[1].Visible = false;
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
            this.txtNombreTrabajo.Text = string.Empty;
            this.txtPrecioUnitario.Text = string.Empty;
            
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.IsNuevo = true;
            this.IsEditar = false;
            this.Botones();
            this.Limpiar();
            this.Habilitar(true);
            this.txtNombreTrabajo.Focus();
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
            this.txtNombreTrabajo.ReadOnly = !valor;
            this.txtPrecioUnitario.ReadOnly = !valor;
            /*this.txtDescripcion.ReadOnly = !valor;
            this.txtStock.Enabled = valor; */
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (this.txtNombreTrabajo.Text == string.Empty || this.txtPrecioUnitario.Text == string.Empty)
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
                        rpta = CN_Trabajos.Insertar(this.txtNombreTrabajo.Text.Trim(), this.txtPrecioUnitario.Text.Trim());
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
                            this.MensajeOk("Se Insertó de forma correcta el trabajo");
                        }
                        else
                        {
                            this.MensajeOk("Se Actualizó de forma correcta el trabajo");
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
                    this.MostrarTrabajos();


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
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
            if (e.ColumnIndex == dataListadoTrabajos.Columns["Marcar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)dataListadoTrabajos.Rows[e.RowIndex].Cells["Marcar"];
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

                    foreach (DataGridViewRow row in dataListadoTrabajos.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Codigo = Convert.ToString(row.Cells[1].Value);
                            Rpta = CN_Productos.Eliminar(Convert.ToInt32(Codigo));

                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se Eliminó Correctamente el/los trabajos");
                            }
                            else
                            {
                                this.MensajeError(Rpta);
                            }

                        }
                    }
                    this.MostrarTrabajos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
    }
}
