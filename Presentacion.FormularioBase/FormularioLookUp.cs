using Aplicacion.Constantes.Imagenes;
using System;
using System.Windows.Forms;

namespace Presentacion.FormularioBase
{
    public partial class FormularioLookUp : Formulario
    {
        private object _entidad;
        public object EntidadSeleccionada => _entidad;
        public FormularioLookUp()
        {
            InitializeComponent();

            //Ponemos imagenes a los botones
            btnActualizar.Image = Imagen.Actualizar;
            btnSalir.Image = Imagen.Salir;

            _entidad = null;
        }

        private void FormularioLookUp_Load(object sender, System.EventArgs e)
        {
            ActualizarDatos(string.Empty);
        }

        public virtual void ActualizarDatos(string cadenaBuscar)
        {
            FormatearGrilla(dgvGrilla);
        }

        private void dgvGrilla_RowErrorTextChanged(object sender, System.Windows.Forms.DataGridViewRowEventArgs e)
        {

        }

        private void dgvGrilla_RowEnter(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (dgvGrilla.RowCount > 0) //Contiene datos
            {
                _entidad = dgvGrilla.Rows[e.RowIndex].DataBoundItem;
            }
            else
            {
                _entidad = null;
            }
        }

        private void dgvGrilla_DoubleClick(object sender, EventArgs e)
        {
            if (_entidad != null)
            {
                Close();
            }
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ActualizarDatos(txtBusqueda.Text);
        }

        private void FormularioLookUp_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnBuscar.PerformClick();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarDatos(string.Empty);
        }
    }
}
