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
    public partial class formNuevoEditarCompra : Form
    {
        CN_Compras objetoCN = new CN_Compras();

        DataTable respuesta;
        // int parametroActual;
        bool bandera;
        bool IsNuevo = false;
        bool IsEditar = false;

        private int IdCompra;
        private string IdProveedor;

        private string Proveedor;

        private string Producto;
        private string PrecioCompra;
        private string PrecioVenta;
        private string Cantidad;
        private string Codigo;
        private string Descripcion;

        public formNuevoEditarCompra(int parametro, bool IsNuevoEditar)
        {
            InitializeComponent();
            this.IdCompra = parametro;
            this.bandera = IsNuevoEditar;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void formNuevoEditarCompra_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtProducto;
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
                this.MostrarCompra(this.IdCompra);
            }
        }

        // Carga los valores en los campos de texto del formulario para que se modifiquen los que se desean
        private void MostrarCompra(int IdCompra)
        {
            respuesta = objetoCN.MostrarCompra(IdCompra);

            Console.WriteLine("Respuesta es ; " + respuesta.Rows.Count);
            foreach (DataRow row in respuesta.Rows)
            {
                IdCompra = Convert.ToInt32(row["IdCompra"]);

                Producto = Convert.ToString(row["Producto"]);
                Codigo = Convert.ToString(row["Codigo"]);
                PrecioCompra = Convert.ToString(row["PrecioCompra"]);
                PrecioVenta = Convert.ToString(row["PrecioVenta"]);
                Cantidad = Convert.ToString(row["Cantidad"]);  // cantidad que se realizo en ese momento
                Descripcion = Convert.ToString(row["Descripcion"]);
                Proveedor = Convert.ToString(row["Proveedor"]);


                txtProducto.Text = Producto;

                txtCodigo.Text = Codigo;
                txtCantidad.Text = Cantidad;
                txtPrecioCompra.Text = PrecioCompra;
                txtPrecioVenta.Text = PrecioVenta;
                txtDescripcion.Text = Descripcion;

                cbProveedor.Text = Proveedor;

            }
        }
    }
}
