using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// Agregados
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class formProductos : Form
    {
        CN_Productos objetoCN = new CN_Productos();

        private bool IsNuevo = false;
        private bool IsEditar = false;
        private int IdProducto;
        public formProductos()
        {
            InitializeComponent();
            // lblTotalProductos.Text = "Total de Productos: " + Convert.ToString(dataListadoProductos.Rows.Count);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MostrarProductos();
        }
        private void MostrarProductos()
        {
            dataListadoProductos.DataSource = objetoCN.MostrarProd();
            // this.IdProducto = dataListadoProductos.Rows[0].Cells[1].Value.ToString();

            // Console.WriteLine("IdProducto es : " +  this.IdProducto);
            // Oculto el IdProducto. Lo puedo seguir usando como parametro de eliminacion
            dataListadoProductos.Columns[1].Visible = false;
        }


        // Carga los valores en el formulario para que se modifiquen los que se desean
        private void MostrarProducto(int IdProducto)
        {
            Console.WriteLine("El IDproducto recibido es : " + IdProducto);
            dataListadoProductos.DataSource = objetoCN.MostrarProducto(IdProducto);
            Console.WriteLine("El dataListadoProductos.Rows[0].Cells[6].Value.ToString() recibido es : " + dataListadoProductos.Rows[0].Cells[1]);
            // Oculto el IdProducto. Lo puedo seguir usando como parametro de eliminacion
            // dataListadoProductos.Columns[1].Visible = false;
            /*
            txtNombre.Text = dataListadoProductos.Rows[0].Cells[6].Value.ToString();
            /// blTelefono.Text = dataListadoTrabajosEmpleado.Rows[0].Cells[3].Value.ToString();
            txtCodigo.Text = dataListadoProductos.Rows[0].Cells[1].Value.ToString();
            txtStock.Text = dataListadoProductos.Rows[0].Cells[1].Value.ToString();
            txtPrecioCompra.Text = dataListadoProductos.Rows[0].Cells[1].Value.ToString();
            txtPrecioVenta.Text = dataListadoProductos.Rows[3].Cells[1].Value.ToString();*/
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
            this.txtNombre.Text = string.Empty;
            this.txtDescripcion.Text = string.Empty;
            this.txtStock.Text = string.Empty;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.IsNuevo = true;
            this.IsEditar = false;
            this.Botones();
            this.Limpiar();
            this.Habilitar(true);
            this.txtNombre.Focus();
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
                this.btnCancelar.Enabled = true;
            }
            else
            {
                this.Habilitar(false);
                this.btnNuevo.Enabled = true;
                this.btnGuardar.Enabled = false;
                // this.btnEditar.Enabled = true;
                this.btnCancelar.Enabled = false;
            }

        }
        //Habilitar los controles del formulario
        private void Habilitar(bool valor)
        {
            // this.txtId.ReadOnly = !valor;
            this.txtNombre.ReadOnly = !valor;
            this.txtDescripcion.ReadOnly = !valor;
            this.txtStock.Enabled = valor;
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (this.txtNombre.Text == string.Empty || this.txtStock.Text == string.Empty || this.txtPrecioCompra.Text == string.Empty || this.txtPrecioVenta.Text == string.Empty || this.txtCodigo.Text == string.Empty)
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
                        rpta = CN_Productos.Insertar(this.txtNombre.Text.Trim(), this.txtCodigo.Text.Trim(), this.txtPrecioCompra.Text.Trim(), 
                            this.txtPrecioVenta.Text.Trim(), this.txtDescripcion.Text.Trim(),
                            this.txtStock.Text.Trim());
                    }
                    else
                    {
                        rpta = CN_Productos.Editar(this.IdProducto,this.txtNombre.Text.Trim(),this.txtCodigo.Text.Trim(),
                            this.txtPrecioCompra.Text.Trim(),this.txtPrecioVenta.Text.Trim(),this.txtDescripcion.Text.Trim()
                            ,this.txtStock.Text.Trim());

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
                            this.tabListado.SelectedTab = tabPage1;
                            this.IsEditar = false;
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
                    this.MostrarProductos();


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
            if (e.ColumnIndex == dataListadoProductos.Columns["Marcar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)dataListadoProductos.Rows[e.RowIndex].Cells["Marcar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
                // Console.WriteLine("dataListadoProductos.Rows[e.RowIndex].Cells luego de tildar es :" + dataListadoProductos.Rows[e.RowIndex].Cells["Marcar"]);
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

                    foreach (DataGridViewRow row in dataListadoProductos.Rows)
                    {
                        // Console.WriteLine("El valor de row.Cells[1].Value " + row.Cells[1].Value);
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Codigo = Convert.ToString(row.Cells[1].Value);  // Id del producto en row.Cells[1].Value
                            Rpta = CN_Productos.Eliminar(Convert.ToInt32(Codigo));

                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se Eliminó Correctamente el/los productos");
                            }
                            else
                            {
                                this.MensajeError(Rpta);
                            }

                        }
                    }
                    this.MostrarProductos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void botonEditarListado_Click(object sender, EventArgs e)
        {


            this.IsEditar = true;
            try
            {
                    int IdProducto;
                    string Producto;
                    string Codigo;
                    string PrecioCompra;
                    string PrecioVenta;
                    string Stock;
                    string Descripcion;

                    // string Rpta = "";

                    foreach (DataGridViewRow row in dataListadoProductos.Rows)
                    {
                    
                    if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            
                            this.IdProducto = Convert.ToInt32(row.Cells[1].Value);
                            Console.WriteLine("this.IdProducto luego de tildar es :" + this.IdProducto);
                            IdProducto = Convert.ToInt32(row.Cells[1].Value);
                            Producto = Convert.ToString(row.Cells[2].Value);
                            Codigo = Convert.ToString(row.Cells[3].Value);
                            PrecioCompra = Convert.ToString(row.Cells[4].Value);
                            PrecioVenta = Convert.ToString(row.Cells[5].Value);
                            Stock = Convert.ToString(row.Cells[6].Value);
                            Descripcion = Convert.ToString(row.Cells[7].Value);

                            txtNombre.Text = Producto;
                        /// blTelefono.Text = dataListadoTrabajosEmpleado.Rows[0].Cells[3].Value.ToString();
                            txtCodigo.Text = Codigo;
                            txtStock.Text = Stock;
                            txtPrecioCompra.Text = PrecioCompra;
                            txtPrecioVenta.Text = PrecioVenta;

                            /* Console.WriteLine("IdProducto es : " + IdProducto);
                            Console.WriteLine("Codigo es : " + Codigo);
                            Console.WriteLine("PrecioVenta es : " + PrecioVenta);
                            Console.WriteLine("Descripcion es : " + Descripcion); */


                            
                            /*
                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se Edito Correctamente Correctamente el/los productos");
                                this.tabListado.SelectedTab = tabPage1;
                            }
                            else
                            {
                                this.MensajeError(Rpta);
                            }
                            */
                        }
                    }
                // Cambiar de pestaña
                this.tabListado.SelectedTab = tabPage2;
                this.btnNuevo.Enabled = false;

                Console.WriteLine("this.IdProducto enn editar  ..as   " + this.IdProducto);

                // Cargo el producto en el formulario
                this.MostrarProducto(this.IdProducto);
                

                // this.MostrarProductos();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Entro en el catch bb");
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
