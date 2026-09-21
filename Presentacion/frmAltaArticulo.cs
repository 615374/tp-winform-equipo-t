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
        private void frmArticulos_Load(object sender, EventArgs e)
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

        private void dgvArticulos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cboCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
