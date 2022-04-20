namespace Presentacion.Core.FormaPago
{
    using System;
    using FormularioBase;
    using Presentacion.Core.Articulo.Clases;

    public partial class FormaPago : Formulario
    {
        private FacturaView _facturav;
        public FormaPago(FacturaView nuevaFactura)
        {
            InitializeComponent();

            _facturav = nuevaFactura;
        }

        private void tabPage4_Click(object sender, System.EventArgs e)
        {

        }

        private void FormaPago_Load(object sender, System.EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            txtAbonar.Text = _facturav.Total.ToString("C");
            nudMontoEfectivo.Value = _facturav.Total;
            nudTotal.Value = _facturav.Total;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void nudMontoEfectivo_ValueChanged(object sender, EventArgs e)
        {
            nudTotalEfectivo.Value = nudMontoEfectivo.Value;
        }

        private void CalcularTotal()
        {
            nudTotal.Value = nudTotalEfectivo.Value +
                             nudTotalCheque.Value +
                             nudTotalCtaCte.Value +
                             nudTotalTarjeta.Value;
        }

        private void nudMontoTarjeta_ValueChanged(object sender, EventArgs e)
        {
            nudTotalEfectivo.Value = nudMontoEfectivo.Value;
        }

        private void nudTotal_ValueChanged(object sender, EventArgs e)
        {
            CalcularTotal();
        }
    }
}
