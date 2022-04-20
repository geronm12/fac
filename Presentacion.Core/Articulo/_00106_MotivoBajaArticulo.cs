namespace Presentacion.Core.Articulo
{
    using System.Windows.Forms;
    using FormularioBase;
    using Presentacion.FormularioBase.Helpers;
    using Servicio.Interfaces.MotivoBaja;
    using StructureMap;

    public partial class _00106_MotivoBajaArticulo : FormularioConsulta
    {
        private readonly IMotivoBajaServicio _motivoBajaServicio;

        public _00106_MotivoBajaArticulo()
        {
            InitializeComponent();

            _motivoBajaServicio = ObjectFactory.GetInstance<IMotivoBajaServicio>();
        }

        public override void ActualizarDatos(DataGridView dgv, string cadenaBuscar)
        {
            dgvGrilla.DataSource = _motivoBajaServicio.Get(!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            FormatearGrilla(dgv);
        }

        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);

            dgv.Columns["Descripcion"].Visible = true;
            dgv.Columns["Descripcion"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["Descripcion"].HeaderText = "Descripcion";
            dgv.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            CentrarCabecerasGrilla(this.dgvGrilla);
        }

        public override bool EjecutarComandoEliminar()
        {
            var fElminar = new _00107_Abm_MotivoBajaArticulo(TipoOperacion.Eliminar , _entidadId.Value);

            fElminar.ShowDialog();

            return fElminar.RelizoAlgunaOperacion;
        }

        public override bool EjecutarComandoModificar()
        {
            var fModificar = new _00107_Abm_MotivoBajaArticulo(TipoOperacion.Modificar, _entidadId.Value);

            fModificar.ShowDialog();

            return fModificar.RelizoAlgunaOperacion;
        }

        public override bool EjecutarComandoNuevo()
        {
            var fNuevo = new _00107_Abm_MotivoBajaArticulo(TipoOperacion.Nuevo);

            fNuevo.ShowDialog();

            return fNuevo.RelizoAlgunaOperacion;
        }
    }
}
