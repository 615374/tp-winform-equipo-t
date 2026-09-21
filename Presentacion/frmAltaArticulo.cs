using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestion_de_Catalogo_de_Productos
{
    public partial class frmAltaArticulo : Form
    {
        private Articulo articuloSeleccionado = null;
        private bool modificando = false;
        public frmAltaArticulo()
        {
            InitializeComponent();
        }
        private void frmAltaArticulo_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            try
            {
                dgvArticulos.DataSource = negocio.listar();

                cboMarca.DataSource = marcaNegocio.listar();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";

                cboCategoria.DataSource = categoriaNegocio.listar();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";

                cboMarca.SelectedIndex = -1;
                cboCategoria.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Método auxiliar
        private void cargarImagenes(int idArticulo)
        {
            ImagenNegocio negocio = new ImagenNegocio();

            try
            {
                dgvImagenes.DataSource = null;
                dgvImagenes.DataSource = negocio.listarPorIdArticulo(idArticulo);
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
            articuloSeleccionado = null;

            txtCodigo.Enabled = true;
            txtNombre.Enabled = true;
            txtDescripcion.Enabled = true;
            txtPrecio.Enabled = true;
            cboMarca.Enabled = true;
            cboCategoria.Enabled = true;

            txtCodigo.Clear();
            txtNombre.Clear();
            txtDescripcion.Clear();
            txtPrecio.Clear();

            cboMarca.SelectedIndex = -1;
            cboCategoria.SelectedIndex = -1;

            txtCodigo.Focus();
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un artículo para modificar.");
                return;
            }

            articuloSeleccionado =
                (Articulo)dgvArticulos.SelectedRows[0].DataBoundItem;

            txtCodigo.Enabled = true;
            txtNombre.Enabled = true;
            txtDescripcion.Enabled = true;
            txtPrecio.Enabled = true;
            cboMarca.Enabled = true;
            cboCategoria.Enabled = true;

            txtCodigo.Text = articuloSeleccionado.Codigo;
            txtNombre.Text = articuloSeleccionado.Nombre;
            txtDescripcion.Text = articuloSeleccionado.Descripcion;
            txtPrecio.Text = articuloSeleccionado.Precio.ToString();

            //Cargamos en el Combo Box la marca y categoría del articulo a modificar
            cboMarca.SelectedValue = articuloSeleccionado.Marca.Id;
            cboCategoria.SelectedValue = articuloSeleccionado.Categoria.Id;

            modificando = true;
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "")
            {
                MessageBox.Show("Ingrese un código.");
                return;
            }

            if (txtNombre.Text == "")
            {
                MessageBox.Show("Ingrese un nombre.");
                return;
            }

            if (txtDescripcion.Text == "")
            {
                MessageBox.Show("Ingrese una descripción.");
                return;
            }

            if (txtPrecio.Text == "")
            {
                MessageBox.Show("Ingrese un precio.");
                return;
            }

            if (cboMarca.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione una marca.");
                return;
            }

            if (cboCategoria.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione una categoría.");
                return;
            }

            decimal precio;

            if (!decimal.TryParse(txtPrecio.Text, out precio))
            {
                MessageBox.Show("Ingrese un precio válido.");
                return;
            }

            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();

                if (modificando)
                {
                    articuloSeleccionado.Codigo = txtCodigo.Text;
                    articuloSeleccionado.Nombre = txtNombre.Text;
                    articuloSeleccionado.Descripcion = txtDescripcion.Text;
                    articuloSeleccionado.Precio = precio;
                    articuloSeleccionado.Marca = (Marca)cboMarca.SelectedItem;
                    articuloSeleccionado.Categoria = (Categoria)cboCategoria.SelectedItem;

                    negocio.modificar(articuloSeleccionado);

                    MessageBox.Show("Artículo modificado correctamente.");
                }
                else
                {
                    Articulo nuevo = new Articulo();

                    nuevo.Codigo = txtCodigo.Text;
                    nuevo.Nombre = txtNombre.Text;
                    nuevo.Descripcion = txtDescripcion.Text;
                    nuevo.Precio = precio;
                    nuevo.Marca = (Marca)cboMarca.SelectedItem;
                    nuevo.Categoria = (Categoria)cboCategoria.SelectedItem;

                    negocio.agregar(nuevo);

                    MessageBox.Show("Artículo agregado correctamente.");
                }

                cargar();

                txtCodigo.Clear();
                txtNombre.Clear();
                txtDescripcion.Clear();
                txtPrecio.Clear();

                txtCodigo.Enabled = false;
                txtNombre.Enabled = false;
                txtDescripcion.Enabled = false;
                txtPrecio.Enabled = false;
                cboMarca.Enabled = false;
                cboCategoria.Enabled = false;

                modificando = false;
                articuloSeleccionado = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un artículo para eliminar.");
                return;
            }

            Articulo seleccionado =
                (Articulo)dgvArticulos.SelectedRows[0].DataBoundItem;

            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                DialogResult respuesta = MessageBox.Show(
                    "¿Está seguro de que desea eliminar el artículo " + seleccionado.Nombre + "?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (respuesta == DialogResult.Yes)
                {
                    negocio.eliminar(seleccionado.Id);

                    MessageBox.Show("Artículo eliminado correctamente.");

                    cargar();

                    txtCodigo.Clear();
                    txtNombre.Clear();
                    txtDescripcion.Clear();
                    txtPrecio.Clear();

                    txtCodigo.Enabled = false;
                    txtNombre.Enabled = false;
                    txtDescripcion.Enabled = false;
                    txtPrecio.Enabled = false;
                    cboMarca.Enabled = false;
                    cboCategoria.Enabled = false;

                    modificando = false;
                    articuloSeleccionado = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null && dgvArticulos.CurrentRow.DataBoundItem is Articulo)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

                cargarImagenes(seleccionado.Id);
            }
        }
    }
}
