namespace Presentacion.Core.LookUp
{
    using FormularioBase;
    using Servicio.Interfaces.Persona;
    using Servicio.Interfaces.Persona.DTOs;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    public partial class LookUpCliente : FormularioLookUp
    {
        private readonly IClienteServicio _clienteServicio;
        public LookUpCliente(IClienteServicio clienteServicio)
        {
            InitializeComponent();

            _clienteServicio = clienteServicio;
        }

        private void LookUpCliente_Load(object sender, System.EventArgs e)
        {

        }

        public override void ActualizarDatos(string cadenaBuscar)
        {

            dgvGrilla.DataSource = ((List<ClienteDto>)_clienteServicio.Get(typeof(ClienteDto), cadenaBuscar))
                .Where(x => !x.EstaEliminado).ToList();


            base.ActualizarDatos(cadenaBuscar);
        }

        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);


            dgv.Columns["ApyNom"].Visible = true;
            dgv.Columns["ApyNom"].HeaderText = "Apellido y Nombre";
            dgv.Columns["ApyNom"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgv.Columns["Dni"].Visible = true;
            dgv.Columns["Dni"].HeaderText = "DNI";

            CentrarCabecerasGrilla(dgv);
        }

    }
}
