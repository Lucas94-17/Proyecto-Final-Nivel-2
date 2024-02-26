using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using negocios;
using dominio;

namespace Presentación
{
    public partial class VentanaPrincipal : Form
    {
        public List<Articulo> listaArticulos;
        public VentanaPrincipal()
        {
            InitializeComponent();
        }

        private bool soloNumeros(string cadena)
        {

            foreach (char caracter in cadena)
            {
                if (!(char.IsNumber(caracter)))
                {
                    return false;
                }

            }
            return true;
        }
        private bool validarFiltro()
        {
            if (cbxCampo.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor ingrese el campo a filtrar");
                return true;
            }
            if (cbxCriterio.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor , seleccione el criterio a filtrar");
                return true;
            }
            if (cbxCampo.SelectedItem.ToString() == "Precio")
            {
                if (string.IsNullOrEmpty(txtboxFiltro.Text))
                {
                    MessageBox.Show("El campo está vacío");
                    return true;
                }
                if (!(soloNumeros(txtboxFiltro.Text)))
                {
                    MessageBox.Show("Solo nros para filtrar por un campo numérico");
                    return true;
                }
            }
            return false;
        } 

        private void Form1_Load(object sender, EventArgs e)
        {
            //Articulo elemento;
            Cargar();
            cargarBotones("Detalles","Ver mas detalles");
            cargarBotones("Modificar", "Modificar Elemento");
            cargarBotones("Eliminar", "Eliminar");
            cbxCampo.Items.Add("Codigo");
            cbxCampo.Items.Add("Nombre");
            cbxCampo.Items.Add("Descripcion");
            cbxCampo.Items.Add("Precio");
        }
        private void Cargar()
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            try
            {
                
                listaArticulos = articuloNegocio.Listar();
                dgvDatos.DataSource = articuloNegocio.Listar();
                dgvDatos.AutoResizeColumns();
                dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;                
                ocultarColumnas();
                cargarImagen(listaArticulos[2].urlImage);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private void cargarBotones(string nombre,string texto)
        {
            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            button.HeaderText = nombre;
            button.Text = texto;
            button.UseColumnTextForButtonValue = true;
            dgvDatos.Columns.Add(button);
        }
       
        private void dgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1)
                return;

            if (this.dgvDatos.Columns[e.ColumnIndex].HeaderText == "Detalles")
            {
                Articulo elegido;
                elegido = (Articulo)dgvDatos.CurrentRow.DataBoundItem;
                DetallesArticulo Detalles = new DetallesArticulo(elegido);
                Detalles.ShowDialog();
            }
            if (this.dgvDatos.Columns[e.ColumnIndex].HeaderText == "Modificar")
            {
                Articulo elegido;
                elegido = (Articulo)dgvDatos.CurrentRow.DataBoundItem;
                AltaArticulo modificar = new AltaArticulo(elegido);
                modificar.ShowDialog();
                Cargar();
            }
            if (this.dgvDatos.Columns[e.ColumnIndex].HeaderText == "Eliminar")
            {
                eliminar();
            }

        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbArticulos.Load(imagen);
            }
            catch (Exception)
            {

                pbArticulos.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQr64U1qrn_mnXFwQoOmiuJs1zp0aLvApc1WmtDk-_IywS0eg7pzlSCsqDNbUzJuPSRupo&usqp=CAU");
            }
        }
        private void ocultarColumnas()
        {
            dgvDatos.Columns["UrlImage"].Visible = false;
            dgvDatos.Columns["Id"].Visible = false;
            dgvDatos.Columns["Descripcion"].Visible = false;
            dgvDatos.Columns["Codigo"].Visible = false;
            dgvDatos.Columns["Precio"].Visible = true;
            //dgvDatos.Columns["Categoria"].Visible = false;
        }
        private void dgvDatos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDatos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvDatos.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.urlImage);

            }
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AltaArticulo Alta = new AltaArticulo();
            Alta.ShowDialog();
            Cargar();
        }
        private void eliminar(bool logico = false)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("¿De verdad quiere eliminar ? ","Eliminado !" , MessageBoxButtons.YesNo , MessageBoxIcon.Warning);
                if(respuesta == DialogResult.Yes)
                {
                    seleccionado = (Articulo)dgvDatos.CurrentRow.DataBoundItem;
                    negocio.eliminar(seleccionado.Id);
                    MessageBox.Show("El elemento se ha eliminado correctamente !");
                }
                Cargar();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void cbxCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cbxCampo.SelectedItem.ToString();
            if (opcion == "Precio" )
            {
                cbxCriterio.Items.Clear();
                cbxCriterio.Items.Add("Mayor a");
                cbxCriterio.Items.Add("Menor a");
                cbxCriterio.Items.Add("Igual a");
            }
            else if(opcion == "Codigo")
            {
                cbxCriterio.Items.Clear();
                cbxCriterio.Items.Add("Igual a");
            }
            else
            {
                cbxCriterio.Items.Clear();
                cbxCriterio.Items.Add("Comienza con");
                cbxCriterio.Items.Add("Termina con");
                cbxCriterio.Items.Add("Contiene");
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio articulo = new ArticuloNegocio();

            try
            {
                if (validarFiltro())
                {
                    return;
                }
                string campo = cbxCampo.SelectedItem.ToString();
                string criterio = cbxCriterio.SelectedItem.ToString();
                string filtro = txtboxFiltro.Text;
                dgvDatos.DataSource = articulo.filtrar(campo, criterio, filtro);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
