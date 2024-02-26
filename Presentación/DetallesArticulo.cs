using dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentación
{
    public partial class DetallesArticulo : Form
    {
        public Articulo articulo = null;
        public DetallesArticulo()
        {
            InitializeComponent();
        }
        public DetallesArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            
        }

        private void btnAceptarDetalles_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DetallesArticulo_Load(object sender, EventArgs e)
        {
            try
            {
                if (articulo != null)
                {
                    lblCodigoDetalle.Text = articulo.Codigo;
                    lblNombreDetalle.Text = articulo.Nombre;
                    lblPrecioDetalle.Text = articulo.Precio.ToString();
                    lblCategoriaDetalle.Text = articulo.Categoria.ToString();
                    lblDescripcionDetalle.Text = articulo.Descripcion;
                    lblMarcaDetalle.Text = articulo.Marca.ToString();
                    lblDetallesNombreArticulo.Text = articulo.Nombre;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
