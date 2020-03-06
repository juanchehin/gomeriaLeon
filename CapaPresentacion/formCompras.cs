using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class formCompras : Form
    {
        public formCompras()
        {
            InitializeComponent();
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
    }
}
