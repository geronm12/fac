namespace Presentacion.Core.LookUp
{
    using FormularioBase;
    using Servicio.Interfaces.Articulo;
    using System.Linq;
    using System.Windows.Forms;

    public partial class LookUpArticulo : FormularioLookUp
    {
        private readonly IArticuloServicio _articuloServicio2;

        public LookUpArticulo()
        {
            InitializeComponent();
        }

        public override void ActualizarDatos(string cadenaBuscar)
        {
            dgvGrilla.DataSource = _articuloServicio2.Get(cadenaBuscar)
                        .Where(x => !x.EstaEliminado).ToList();

            base.ActualizarDatos(cadenaBuscar);
        }

        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);


            dgv.Columns["Codigo"].Visible = true;
            dgv.Columns["Codigo"].HeaderText = "Codigo";
            dgv.Columns["Codigo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgv.Columns["CodigoBarra"].Visible = true;
            dgv.Columns["CodigoBarra"].HeaderText = "CodigoBarra";
            dgv.Columns["CodigoBarra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns["Descripcion"].Visible = true;
            dgv.Columns["Descripcion"].HeaderText = "Descripcion";
            dgv.Columns["Descripcion"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            CentrarCabecerasGrilla(dgv);
        }
    }
}
