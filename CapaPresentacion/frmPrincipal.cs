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
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            formProductos frm = new formProductos();
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            formEmpleados frm = new formEmpleados();
        }

        private void txtSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
