﻿using System;
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
    public partial class formCompras : Form
    {
        CN_Compras objetoCN = new CN_Compras();
        private int IdCompra;
        public formCompras()
        {
            InitializeComponent();
            MostrarCompras();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscar_Click(this, new EventArgs());
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

        }

        private void btnNuevaCompra_Click(object sender, EventArgs e)
        {
            Console.WriteLine("this.IdProducto en click nuevo es  : " + this.IdCompra);
            formNuevoEditarCompra frm = new formNuevoEditarCompra(this.IdCompra, true);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void dataListadoProductos_SelectionChanged(object sender, EventArgs e)
        {
            if (dataListadoCompras.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataListadoCompras.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataListadoCompras.Rows[selectedrowindex];
                this.IdCompra = Convert.ToInt32(selectedRow.Cells["IdCompra"].Value);
                Console.WriteLine("El id producto es " + this.IdCompra);
            }
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            this.MostrarCompras();
            this.Refresh();
        }

        public void MostrarCompras()
        {
            // Console.WriteLine("Ahora va el mostrar productos");
            dataListadoCompras.DataSource = objetoCN.MostrarCompras();
            dataListadoCompras.Columns[0].Visible = false;
            lblTotalCompras.Text = "Total de Registros: " + Convert.ToString(dataListadoCompras.Rows.Count);
            // this.banderaFormularioHijo = false;
        }

        private void botonEditarListado_Click(object sender, EventArgs e)
        {
            formNuevoEditarCompra frm = new formNuevoEditarCompra(this.IdCompra, false);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void formCompras_Load(object sender, EventArgs e)
        {
            MostrarCompras();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Realmente Desea Eliminar la compra", "Gomeria Leon", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (Opcion == DialogResult.OK)
                {
                    CN_Compras.Eliminar(this.IdCompra);
                    // this.MostrarProductos();
                }
                this.MensajeOk("Se elimino de forma correcta el registro");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
            this.Close();
        }

        //Mostrar Mensaje de Confirmación generico
        private void MensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje, "Gomeria Leon", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
