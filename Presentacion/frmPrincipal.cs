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

namespace Presentacion
{
    public partial class frmPrincipal : Form
    {
        private List<Articulo> listaArticulo;

        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                listaArticulo = negocio.listar();
                dgvArticulos.DataSource = listaArticulo;

                ocultarColumnas();

                if (listaArticulo != null && listaArticulo.Count > 0)
                {
                    cargarImagen(listaArticulo[0].Imagenes);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ocultarColumnas()
        {
            if (dgvArticulos.Columns["Id"] != null)
                dgvArticulos.Columns["Id"].Visible = false;

            // Ocultar la lista de imágenes para que no rompa el formato de celda
            if (dgvArticulos.Columns["Imagenes"] != null)
                dgvArticulos.Columns["Imagenes"].Visible = false;
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.Imagenes);
            }
        }

        private void cargarImagen(List<Imagen> imagenes)
        {
            try
            {
                if (imagenes != null && imagenes.Count > 0 && !string.IsNullOrEmpty(imagenes[0].ImagenUrl))
                {
                    pbxArticulo.Load(imagenes[0].ImagenUrl);
                }
                else
                {
                    cargarImagenPlaceholder();
                }
            }
            catch (Exception)
            {
                // Si la URL guardada en la BD está caída/rota, forzamos el placeholder para verificar que cambió de fila
                cargarImagenPlaceholder();
            }
        }

        private void cargarImagenPlaceholder()
        {
            try
            {
                pbxArticulo.Load("https://efectuscostarica.com/wp-content/uploads/2021/07/placeholder.png");
            }
            catch (Exception)
            {
                pbxArticulo.Image = null;
            }
        }

        private void dgvArticulos_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Silencia los errores automáticos de renderizado de celdas
            e.Cancel = true;
        }

        private void frmPrincipal_Load_1(object sender, EventArgs e)
        {
            cargar();
        }
    }
}
