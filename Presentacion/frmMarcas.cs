using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace Gestion_de_Catalogo_de_Productos
{
    public partial class frmMarcas : Form
    {
        private Marca marcaSeleccionada = null;
        private bool modificando = false;

        public frmMarcas()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frmMarcas_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            MarcaNegocio negocio = new MarcaNegocio();

            try
            {
                dgvMarcas.DataSource = negocio.listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            modificando = false;
            marcaSeleccionada = null;

            txtDescripcion.Enabled = true;
            txtDescripcion.Clear();
            txtDescripcion.Focus();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvMarcas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una marca para modificar.");
                return;
            }

            marcaSeleccionada = (Marca)dgvMarcas.SelectedRows[0].DataBoundItem;

            txtDescripcion.Enabled = true;
            txtDescripcion.Text = marcaSeleccionada.Descripcion;
            txtDescripcion.Focus();

            modificando = true;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvMarcas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una marca para eliminar.");
                return;
            }

            Marca seleccionada = (Marca)dgvMarcas.SelectedRows[0].DataBoundItem;
            MarcaNegocio negocio = new MarcaNegocio();

            try
            {
                if (negocio.tieneArticulosAsociados(seleccionada.Id))
                {
                    MessageBox.Show(
                        "No se puede eliminar la marca porque tiene artículos asociados.",
                        "Marca en uso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                DialogResult respuesta = MessageBox.Show(
                    "¿Está seguro de que desea eliminar la marca " + seleccionada.Descripcion + "?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (respuesta == DialogResult.Yes)
                {
                    negocio.eliminar(seleccionada.Id);

                    MessageBox.Show("Marca eliminada correctamente.");
                    cargar();

                    txtDescripcion.Clear();
                    txtDescripcion.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtDescripcion.Text == "")
            {
                MessageBox.Show("Ingrese una descripción.");
                return;
            }

            try
            {
                MarcaNegocio negocio = new MarcaNegocio();

                if (modificando)
                {
                    marcaSeleccionada.Descripcion = txtDescripcion.Text;
                    negocio.modificar(marcaSeleccionada);

                    MessageBox.Show("Marca modificada correctamente.");
                }
                else
                {
                    Marca nueva = new Marca();
                    nueva.Descripcion = txtDescripcion.Text;

                    negocio.agregar(nueva);

                    MessageBox.Show("Marca agregada correctamente.");
                }

                cargar();

                txtDescripcion.Clear();
                txtDescripcion.Enabled = false;

                modificando = false;
                marcaSeleccionada = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
