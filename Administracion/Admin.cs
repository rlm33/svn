using ClasesAlicanTeam.EN;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Administracion
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
            initializeComboBox();
        }

        private void loguin_show(object sender, FormClosedEventArgs e)
        {
            Loguin app = new Loguin();
            app.Show();
        }

        private void initializeComboBox()
        {
            comboBoxNuevoLibroCurso.Items.Clear();
            ENCourse course = new ENCourse();
            List<ENCourse> lcourse = course.ReadAll();
            foreach (ENCourse c in lcourse)
            {
                comboBoxNuevoLibroCurso.Items.Add(c.Courses);
            }

            comboBoxNuevoLibroEditorial.Items.Clear();
            ENPublisher publisher = new ENPublisher();
            List<ENPublisher> lpublisher = publisher.readAll();
            foreach (ENPublisher p in lpublisher)
            {
                comboBoxNuevoLibroEditorial.Items.Add(p.Cif);
            }
        }

        //PADRES
        private void buttonMostrarPadres_Click(object sender, EventArgs e)
        {
            ENCustomer customer = new ENCustomer();
            List<ENCustomer> list = customer.readAll();
            dataGridViewMostrarPadres.Rows.Clear();
            foreach (ENCustomer c in list)
            {
                dataGridViewMostrarPadres.Rows.Add(c.IdCustomers,c.Name,c.Surname,c.Telephone,c.Adress);
            }
        }


        private void buttonGuardarPadre_Click(object sender, EventArgs e)
        {
            ENCustomer customer = new ENCustomer();

            
        }

        //LIBROS
        private void buttonNuevoLibroBuscaImagen_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            openFileDialogLibroImagen.InitialDirectory = "c:\\";
            openFileDialogLibroImagen.Filter = "jpg files (*.jpg)|*.jpg|All files (*.*)|*.*";
            openFileDialogLibroImagen.FilterIndex = 1;
            openFileDialogLibroImagen.RestoreDirectory = true;

            if (openFileDialogLibroImagen.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialogLibroImagen.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            pictureBoxNuevoLibro.Image = new Bitmap(openFileDialogLibroImagen.FileName);
                            //textBoxRutaImagen.Text = openFileDialogLibroImagen.FileName;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }

        }

        private void buttonMostrarLibros_Click(object sender, EventArgs e)
        {
            ENBook libro = new ENBook();
            List<ENBook> lista = libro.readAll();
            dataGridViewMostrarLibros.Rows.Clear();
            foreach (ENBook b in lista)
            {
                dataGridViewMostrarLibros.Rows.Add(b.IDBook,b.Subject.Name,b.Course.Courses,b.CIF,b.Years,b.Name,b.Description);
            }
        }

        private void buttonBuscarLibro_Click(object sender, EventArgs e)
        {
            if (textBoxIdLibroBuscar.Text != "")
            {
                //try
                //{
                    ENBook book = new ENBook();
                    book = book.read(textBoxIdLibroBuscar.Text);
                    textBoxAsignaturasLibroBuscar.Text = book.Subject.Name;
                    textBoxCursosLibroBuscar.Text = book.Course.Courses;
                    textBoxNombreLibroBuscar.Text = book.Name;
                    textBoxEditorialBuscaLibro.Text = book.CIF;
                    textBoxDescripcionBuscaLibro.Text = book.Description;
                    pictureBoxImagenLibro.Image = Image.FromFile(book.Image);
                    buttonEliminarLibro.Visible = true;
               /* }
                catch
                {
                    MessageBox.Show("Inserte un libro correcto.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }*/
            }
            else
                MessageBox.Show("Inserte un curso.", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void buttonEliminarLibro_Click(object sender, EventArgs e)
        {
            ENBook book = new ENBook();
            book = book.read(textBoxIdLibroBuscar.Text);
            book.delete();

            buttonEliminarLibro.Visible = false;
        }

        private void buttonGuardarNuevoLibro_Click(object sender, EventArgs e)
        {
            if (textBoxNuevoLibroIdBook.Text != "")
            {
               // try
                //{
                    ENCourse course = new ENCourse();
                    course = course.Read(comboBoxNuevoLibroCurso.Text);
                    ENPublisher publisher = new ENPublisher();
                    publisher = publisher.read(comboBoxNuevoLibroEditorial.Text);
                    ENSubject subject = new ENSubject();
                    //subject = subject.Course
                    ENBook book = new ENBook(textBoxNuevoLibroIdBook.Text,subject,course,publisher.Cif, null,textBoxNuevoLibroNombre.Text, 1,textBoxNuevoLibroDescripcion.Text);
                    book.insert();
               /* }
                catch
                {
                    MessageBox.Show("Inserte un libro correcto.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }*/
            }
            else
                MessageBox.Show("Inserte un ISBN/EAN13.", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        //ANUNCIOS

        private void buttonMostrarAnuncios_Click(object sender, EventArgs e)
        {
            ENAdvertisement advertisement = new ENAdvertisement();
            List<ENAdvertisement> list = advertisement.readAll();
            dataGridViewMostrarAnuncio.Rows.Clear();
            foreach (ENAdvertisement a in list)
            {
                dataGridViewMostrarAnuncio.Rows.Add(a.IdAdvertisement, a.Customer.Account, a.Description);
            }
        }

        private void buttonBuscarAnuncio_Click(object sender, EventArgs e)
        {
            if (textBoxBuscaIdAnuncio.Text != "")
            {
                try
                {
                    ENAdvertisement advertisement = new ENAdvertisement();
                    advertisement = advertisement.read(int.Parse(textBoxBuscaIdAnuncio.Text));
                    textBoxBuscaAnuncioCliente.Text = advertisement.Customer.IdCustomers;
                    richTextBoxBuscaAnuncioDescripcion.Text = advertisement.Description;
                    buttonEliminarAnuncio.Visible = true;
                }
                catch
                {
                    MessageBox.Show("Inserte un anuncio correcto.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Inserte un identificador de anuncio a buscar.", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        //ALARMAS
        private void initializeDataGridAlarmas()
        {
            ENAlarm alarma = new ENAlarm();
            System.Collections.Generic.IList<ENAlarm> lista = alarma.readAll();
            dataGridViewMostrarAlarmas.Rows.Clear();
            foreach (ENAlarm a in lista)
            {
                dataGridViewMostrarAlarmas.Rows.Add(a.IdAlarms, a.Message);
            }
        }

        private void buttonMostrarAlarmas_Click(object sender, EventArgs e)
        {
            initializeDataGridAlarmas();
        }

        private void buttonBuscarAlarm_Click(object sender, EventArgs e)
        {
            //dataGridViewMostrarAlarmas.cell

            if (textBoxBuscarIdAlarm.Text != "")
            {
                try
                {
                    ENAlarm alarma = new ENAlarm();
                    alarma = alarma.read(Convert.ToInt32(textBoxBuscarIdAlarm.Text));
                    richTextBoxBuscarAlarmaDescripcion.Text = alarma.Message;
                    buttonEliminarAlarm.Visible = true;
                }
                catch
                {
                    MessageBox.Show("Inserte una alarma correcta.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Inserte un identificador de alarma a buscar.", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void buttonEliminarAlarm_Click(object sender, EventArgs e)
        {
            ENAlarm alarma = new ENAlarm();
            alarma = alarma.read(Convert.ToInt32(textBoxBuscarIdAlarm.Text));
            alarma.delete();
            buttonEliminarAlarm.Visible = false;
            initializeDataGridAlarmas();
            textBoxBuscarIdAlarm.Clear();
            richTextBoxBuscarAlarmaDescripcion.Clear();
        }

        private void buttonGuardarAlarma_Click(object sender, EventArgs e)
        {
            if (richTextBoxNuevaAlarmaDescripcion.Text != "")
            {
                ENAlarm alarma = new ENAlarm(richTextBoxNuevaAlarmaDescripcion.Text);
                alarma.insert();
                richTextBoxNuevaAlarmaDescripcion.Clear();
                initializeDataGridAlarmas();
            }
            else
                MessageBox.Show("Inserte una descripcion de alarma", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        //PEDIDOS CLIENTES
        private void buttonMostrarPedidosPadres_Click(object sender, EventArgs e)
        {
            ENCustomerOrder customerorder = new ENCustomerOrder();
           // List<ENCustomerOrder> list = customerorder.
        }

        //PEDIDOS DISTRIBUIDORES

        //CURSOS
        private void initializeDataGridCursos()
        {
            ENCourse course = new ENCourse();
            List<ENCourse> list = course.ReadAll();
            dataGridViewMostrarCursos.Rows.Clear();
            foreach (ENCourse c in list)
            {
                dataGridViewMostrarCursos.Rows.Add(c.Courses);
            }
        }

        private void buttonMostrarCursos_Click(object sender, EventArgs e)
        {
            initializeDataGridCursos();
        }

        private void buttonGuardarCurso_Click(object sender, EventArgs e)
        {
            if (textBoxNuevoCurso.Text != "")
            {
                try
                {
                    ENCourse course = new ENCourse(textBoxNuevoCurso.Text);
                    course.insert();
                    textBoxNuevoCurso.Clear();
                    initializeDataGridCursos();
                }
                catch
                {
                    MessageBox.Show("El curso que desea añadir ya existe.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
                MessageBox.Show("Inserte un curso.", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void buttonEliminarCurso_Click(object sender, EventArgs e)
        {
            if (textBoxEliminarCurso.Text != "")
            {
                try
                {
                    ENCourse course = new ENCourse();
                    course = course.Read(textBoxEliminarCurso.Text);
                    course.delete();
                    textBoxEliminarCurso.Clear();
                    initializeDataGridCursos();
                }
                catch
                {
                    MessageBox.Show("El curso que desea eliminar no existe.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
                MessageBox.Show("Inserte un curso.", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
