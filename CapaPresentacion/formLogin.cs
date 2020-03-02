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
    public partial class formLogin : Form
    {
        public formLogin()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string Datos = CapaNegocio.CN_Usuarios.Login(this.txtUsuario.Text, this.txtPassword.Text);
            Console.WriteLine("Datos es : " + Datos);
            //Evaluar si existe el Usuario
            if (Datos != "Ok")
            {
                MessageBox.Show("Error de login", "Gomeria Leon", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                frmPrincipal frm = new frmPrincipal();
                /* frm.Idtrabajador = Datos.Rows[0][0].ToString();
                frm.Apellidos = Datos.Rows[0][1].ToString();
                frm.Nombre = Datos.Rows[0][2].ToString();
                frm.Acceso = Datos.Rows[0][3].ToString(); */

                frm.Show();
                this.Hide();

            }
        }

        private void formLogin_Load(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
