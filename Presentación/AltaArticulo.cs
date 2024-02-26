using dominio;
using negocios;
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
    public partial class AltaArticulo : Form
    {
        private Articulo articulo = null;
        ErrorProvider error = new ErrorProvider();
        public AltaArticulo()
        {
            InitializeComponent();
            
        }
        public AltaArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar articulo";
        }

        private void AltaArticulo_Load(object sender, EventArgs e)
        {
            CategoriaNegocio categoria = new CategoriaNegocio();
            MarcasNegocio marca = new MarcasNegocio();

            try
            {
                //Esto es para el desplegable
                //Primero categorias
                cbxCategoria.DataSource = categoria.Listar();
                cbxCategoria.ValueMember = "Id";
                cbxCategoria.DisplayMember = "Descripcion";

                //Segundo Marcas 
                cbxMarca.DataSource = marca.Listar();
                cbxMarca.ValueMember = "Id";
                cbxMarca.DisplayMember = "Descripcion";

                if (articulo != null)
                {
                    txtCodigo.Text = articulo.Codigo;
                    txtDescripcion.Text = articulo.Descripcion;
                    txtNombre.Text = articulo.Nombre;
                    txtPrecio.Text = articulo.Precio.ToString();
                    txtUrlImagen.Text = articulo.urlImage;
                    cargarImagen(articulo.urlImage);
                    cbxCategoria.Text = articulo.Categoria.Descripcion;
                    cbxMarca.Text = articulo.Marca.Descripcion;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAceptarAlta_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                if (articulo == null)
                    articulo = new Articulo();       
                    articulo.Codigo = (txtCodigo.Text);
                    articulo.Nombre = (txtNombre.Text);
                    articulo.Descripcion = (txtDescripcion.Text);
                    articulo.Precio = decimal.Parse(txtPrecio.Text);
                    articulo.urlImage = txtUrlImagen.Text;
                    articulo.Categoria = (Categorias)cbxCategoria.SelectedItem;
                    articulo.Marca = (Marcas)cbxMarca.SelectedItem;

                if (articulo.Id == 0)
                {
                    negocio.agregar(articulo);
                    MessageBox.Show("Se ha agregado exitosamente ! ");
                    Close();
                }
                else if(articulo.Id != 0)
                {
                    if (cbxCategoria.SelectedIndex == -1)
                    {
                        MessageBox.Show("El campo de Categoria está vacío ! agrege algo");
                    }else if(cbxMarca.SelectedIndex == -1)
                    {
                        MessageBox.Show("El campo de Marca está vacío ! agrege algo");
                    }
                    else
                    {
                        negocio.modificar(articulo);
                        MessageBox.Show("Se ha modificado exitosamente ! ");
                        Close();
                    }
                    
                }
                
                
            }
            catch (Exception)
            {

                MessageBox.Show("Faltan campos para llenar ! ");
            }
        }
        private void cargarImagen(string imagen)
        {
            try
            {
                pbAlta.Load(imagen);
            }
            catch (Exception)
            {

                pbAlta.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQr64U1qrn_mnXFwQoOmiuJs1zp0aLvApc1WmtDk-_IywS0eg7pzlSCsqDNbUzJuPSRupo&usqp=CAU");
            }
        }

        private void AltaArticulo_Leave(object sender, EventArgs e)
        {
            ArticuloNegocio articulo = new ArticuloNegocio();

            try
            {
                cbxCategoria.DataSource = articulo.Listar();
                cbxMarca.DataSource = articulo.Listar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            if (textoVacio(txtUrlImagen))
            {
                error.SetError(txtUrlImagen, "el campo está vacío");
            }
            else
            {
                error.Clear();
            }

            cargarImagen(txtUrlImagen.Text);
           
        }

        private void btnCancelarAlta_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private bool soloNumerosDecimales(KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == '.')
            {
                e.Handled = false;
                return true;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
                return true;
            }
            else
            {
                e.Handled = true;
                return false;
            }
        }

        private bool textoVacio(TextBox text)
        {
            if (text.Text == string.Empty)
            {
                text.Focus();
                return true;
            }
            else
            {
                return false;
            }
        }
        
        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {

            bool valida = soloNumerosDecimales(e);
            if (valida != true)
            {
                error.SetError(txtPrecio, "Solo numeros decimales por favor !");
            }
            else
            {
                error.Clear();
            }

        }

        private void txtCodigo_Leave(object sender, EventArgs e)
        {
            if (textoVacio(txtCodigo))
            {
                error.SetError(txtCodigo, "el campo está vacío");
            }
            else
            {
                error.Clear();
            }
        }

        private void txtDescripcion_Leave(object sender, EventArgs e)
        {
            if (textoVacio(txtDescripcion))
            {
                error.SetError(txtDescripcion, "el campo está vacío");
            }
            else
            {
                error.Clear();
            }
        }

        private void txtNombre_Leave(object sender, EventArgs e)
        {
            if (textoVacio(txtNombre))
            {
                error.SetError(txtNombre, "el campo está vacío");
            }
            else
            {
                error.Clear();
            }
        }

        private void txtPrecio_Leave(object sender, EventArgs e)
        {
            if (textoVacio(txtPrecio))
            {
                error.SetError(txtPrecio, "el campo está vacío");
            }
            else
            {
                error.Clear();
            }
        }

        
    }
}
