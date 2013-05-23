using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Administracion
{
    public partial class Loguin : Form
    {
        public Loguin()
        {
            InitializeComponent();
        }

        private Admin app;

        private void buttonEntrar_Click(object sender, EventArgs e)
        {
            if (textBoxUsuario.Text == "admin" && textBoxPassword.Text == "admin")
            {
                app = new Admin();
                app.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuario y contraseña incorrectos","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loguin_close(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

    }
}
