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
    public partial class frmCategorias : Form
    {
        private Categoria categoriaSeleccionada = null;
        private bool modificando = false;

        public frmCategorias()
        {
            InitializeComponent();
        }

        private void frmCategorias_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();

            try
            {
                dgvCategorias.DataSource = negocio.listar();
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
            categoriaSeleccionada = null;

            txtDescripcion.Enabled = true;
            txtDescripcion.Clear();
            txtDescripcion.Focus();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvCategorias.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una categoría para modificar.");
                return;
            }

            categoriaSeleccionada =
                (Categoria)dgvCategorias.SelectedRows[0].DataBoundItem;

            txtDescripcion.Enabled = true;
            txtDescripcion.Text = categoriaSeleccionada.Descripcion;
            txtDescripcion.Focus();

            modificando = true;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvCategorias.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una categoría para eliminar.");
                return;
            }

            Categoria seleccionada =
                (Categoria)dgvCategorias.SelectedRows[0].DataBoundItem;

            CategoriaNegocio negocio = new CategoriaNegocio();

            try
            {
                if (negocio.tieneArticulosAsociados(seleccionada.Id))
                {
                    MessageBox.Show(
                        "No se puede eliminar la categoría porque tiene artículos asociados.",
                        "Categoría en uso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                DialogResult respuesta = MessageBox.Show(
                    "¿Está seguro de que desea eliminar la categoría " +
                    seleccionada.Descripcion + "?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (respuesta == DialogResult.Yes)
                {
                    negocio.eliminar(seleccionada.Id);

                    MessageBox.Show("Categoría eliminada correctamente.");
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
                CategoriaNegocio negocio = new CategoriaNegocio();

                if (modificando)
                {
                    categoriaSeleccionada.Descripcion = txtDescripcion.Text;
                    negocio.modificar(categoriaSeleccionada);

                    MessageBox.Show("Categoría modificada correctamente.");
                }
                else
                {
                    Categoria nueva = new Categoria();
                    nueva.Descripcion = txtDescripcion.Text;

                    negocio.agregar(nueva);

                    MessageBox.Show("Categoría agregada correctamente.");
                }

                cargar();

                txtDescripcion.Clear();
                txtDescripcion.Enabled = false;

                modificando = false;
                categoriaSeleccionada = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
