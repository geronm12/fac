namespace Presentacion.Core.LookUp
{
    using Presentacion.FormularioBase;
    using Servicio.Interfaces.Persona;
    using Servicio.Interfaces.Persona.DTOs;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    public partial class LookUpEmpleado : FormularioLookUp
    {
        private readonly IEmpleadoServicio _empleadoServicio;
        public LookUpEmpleado(IEmpleadoServicio empleadoServicio)
        {
            InitializeComponent();

            _empleadoServicio = empleadoServicio;
        }

        public override void ActualizarDatos(string cadenaBuscar)
        {
            dgvGrilla.DataSource = ((List<EmpleadoDto>)_empleadoServicio.Get(typeof(EmpleadoDto), cadenaBuscar))
               .Where(x => !x.EstaEliminado).ToList();

            base.ActualizarDatos(cadenaBuscar); //Format de la grilla
        }

        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);

            dgv.Columns["Legajo"].Visible = true;


            dgv.Columns["ApyNom"].Visible = true;
            dgv.Columns["ApyNom"].HeaderText = "Apellido y Nombre";
            dgv.Columns["ApyNom"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgv.Columns["Dni"].Visible = true;
            dgv.Columns["Dni"].HeaderText = "DNI";

            CentrarCabecerasGrilla(dgv);
        }
    }
}
