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
    public partial class formNuevoEditarProducto : Form
    {
        CN_Productos objetoCN = new CN_Productos();
        DataTable respuesta;
        // int parametroActual;
        bool bandera;
        bool IsNuevo = false;
        bool IsEditar = false;

        private int IdProducto;
        private string Producto;
        private string Codigo;
        private string PrecioCompra;
        private string PrecioVenta;
        private string Stock;
        private string Descripcion;

        public formNuevoEditarProducto(int parametro,bool IsNuevoEditar)
        {
            Console.WriteLine("El parametro es : " + parametro);
            InitializeComponent();
            this.IdProducto = parametro;
            this.bandera = IsNuevoEditar;
            // this.ActiveControl = txtNombre;
            // this.txtNombre.Focus();

        }

        private void formNuevoEditarProducto_Load(object sender, EventArgs e)
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
                this.MostrarProducto(this.IdProducto);
            }

            
        }

        // Carga los valores en los campos de texto del formulario para que se modifiquen los que se desean
        private void MostrarProducto(int IdProducto)
        {
            respuesta = objetoCN.MostrarProducto(IdProducto);

            Console.WriteLine("Respuesta es ; " + respuesta.Rows.Count );
            foreach (DataRow row in respuesta.Rows)
            {
                Console.WriteLine("row es :" + row["Producto"]);

                IdProducto = Convert.ToInt32(row["IdProducto"]);
                Producto = Convert.ToString(row["Producto"]);
                Codigo = Convert.ToString(row["Codigo"]);
                PrecioCompra = Convert.ToString(row["PrecioCompra"]);
                PrecioVenta = Convert.ToString(row["PrecioVenta"]);
                Stock = Convert.ToString(row["Stock"]);
                Descripcion = Convert.ToString(row["Descripcion"]);

                //if (Convert.ToBoolean(row.Cells[0].Value))
                //{

                /*this.IdProducto = Convert.ToInt32(row);
                    Console.WriteLine("this.IdProducto es :" + this.IdProducto);
                    IdProducto = Convert.ToInt32(row.Cells[1].Value);
                    Producto = Convert.ToString(row.Cells[2].Value);
                    Codigo = Convert.ToString(row.Cells[3].Value);
                    PrecioCompra = Convert.ToString(row.Cells[4].Value);
                    PrecioVenta = Convert.ToString(row.Cells[5].Value);
                    Stock = Convert.ToString(row.Cells[6].Value);
                    Descripcion = Convert.ToString(row.Cells[7].Value);*/

                    txtNombre.Text = Producto;
                    /// blTelefono.Text = dataListadoTrabajosEmpleado.Rows[0].Cells[3].Value.ToString();
                    txtCodigo.Text = Codigo;
                    txtStock.Text = Stock;
                    txtPrecioCompra.Text = PrecioCompra;
                    txtPrecioVenta.Text = PrecioVenta;
                    txtDescripcion.Text = Descripcion; 

                Console.WriteLine("IdProducto es : " + IdProducto);
                    Console.WriteLine("Codigo es : " + Codigo);
                    Console.WriteLine("PrecioVenta es : " + PrecioVenta);
                    Console.WriteLine("Descripcion es : " + Descripcion);



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
                //}
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
                try
                {
                    string rpta = "";
                    if (this.txtNombre.Text == string.Empty || this.txtStock.Text == string.Empty || this.txtPrecioCompra.Text == string.Empty || this.txtPrecioVenta.Text == string.Empty)
                    {
                        MensajeError("Falta ingresar algunos datos");
                        /*errorIcono.SetError(txtNombre, "Ingrese un Valor");
                        errorIcono.SetError(txtStock, "Ingrese un Valor");
                        errorIcono.SetError(txtDescripcion, "Ingrese un Valor"); */
                    }
                    else
                    {
                        // System.IO.MemoryStream ms = new System.IO.MemoryStream();

                        if (this.IsNuevo)
                        {
                            rpta = CN_Productos.Insertar(this.txtNombre.Text.Trim(), this.txtCodigo.Text.Trim(), this.txtPrecioCompra.Text.Trim(),
                                this.txtPrecioVenta.Text.Trim(), this.txtDescripcion.Text.Trim(),
                                this.txtStock.Text.Trim());
                        }
                        else
                        {
                            rpta = CN_Productos.Editar(this.IdProducto, this.txtNombre.Text.Trim(), this.txtCodigo.Text.Trim(),
                                this.txtPrecioCompra.Text.Trim(), this.txtPrecioVenta.Text.Trim(), this.txtDescripcion.Text.Trim()
                                , this.txtStock.Text.Trim());

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

                        // this.IsNuevo = false;
                        // this.IsEditar = false;
                        /*this.Botones();
                        this.Limpiar();
                        this.MostrarProductos();*/

                        // this.contador = 0;
                        // this.limpiarParametrosMySQL();
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPrecioCompra_KeyPress(object sender, KeyPressEventArgs e)
        {

            Char chr = e.KeyChar;

            if(!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Debe ingresar valores numericos ");
            }
        }

        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;

            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Debe ingresar valores numericos ");
            }
        }

        private void txtPrecioVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;

            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Debe ingresar valores numericos ");
            }
        }
    }
}
