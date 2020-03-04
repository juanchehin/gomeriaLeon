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

        private int  IdProducto;
        private int contador = 0;

        public formProductos()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MostrarProductos();

            this.botonEditarListado.Enabled = false;
            this.btnEliminar.Enabled = false;
        }
        private void MostrarProductos()
        {
            dataListadoProductos.DataSource = objetoCN.MostrarProd();
            dataListadoProductos.Columns[1].Visible = false;
            lblTotalProductos.Text = "Total de Registros: " + Convert.ToString(dataListadoProductos.Rows.Count);

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
            // private int checkCounter;
            if (e.ColumnIndex == dataListadoProductos.Columns["Marcar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)dataListadoProductos.Rows[e.RowIndex].Cells["Marcar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
                Console.WriteLine("dataListadoProductos.Rows[e.RowIndex].Cells luego de tildar es :" + dataListadoProductos.Rows[e.RowIndex].Cells["Marcar"].Value);
                Console.WriteLine("El IdPRoducto deberia ser :" + dataListadoProductos.Rows[e.RowIndex].Cells["IdProducto"].Value);
                this.IdProducto = (int)dataListadoProductos.Rows[e.RowIndex].Cells["IdProducto"].Value;
            }
            
            // Este IF sirve para contar la cantidad de check presionados, recordemos que para la edicion solo puede estar tildado solo uno
            if (!(bool)dataListadoProductos.Rows[e.RowIndex].Cells["Marcar"].Value)
            {
                this.contador--;
            }

            else
            {
                this.contador++;
            }
            // IF que sirve para habilitar los botones EDITAR y ELIMINAR solo si esta tildado un producto
            if (this.contador != 1)
            {
                this.botonEditarListado.Enabled = false;
            }
            else
            {
                this.botonEditarListado.Enabled = true;
            }
            Console.WriteLine("El contador es : " + this.contador);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // hacer el IF con el contador
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

        private void btnNuevoProducto_Click(object sender, EventArgs e)
        {
            Console.WriteLine("this.IdProducto en click nuevo es  : " + this.IdProducto);
            formNuevoEditarProducto frm = new formNuevoEditarProducto(this.IdProducto,true);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void botonEditarListado_Click(object sender, EventArgs e)
        {
            Console.WriteLine("this.IdProducto en click editar es  : " + this.IdProducto);
            formNuevoEditarProducto frm = new formNuevoEditarProducto(this.IdProducto,false);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }
    }
}
