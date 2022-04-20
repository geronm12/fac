namespace Presentacion.Core.Articulo
{
    using System.Windows.Forms;
    using Presentacion.FormularioBase;
    using Presentacion.FormularioBase.Helpers;
    using Servicio.Interfaces.UnidadMedida;
    using StructureMap;

    public partial class _00305_UnidadDeMedida : FormularioConsulta
    {
        private readonly IUnidadMedidaServicio _unidadMedidaServicio;
        public _00305_UnidadDeMedida()
        {
            InitializeComponent();

            _unidadMedidaServicio = ObjectFactory.GetInstance<IUnidadMedidaServicio>();
        }

        public override void ActualizarDatos(DataGridView dgv, string cadenaBuscar)
        {
            dgv.DataSource = _unidadMedidaServicio.Get(!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);



            FormatearGrilla(dgv);
        }

        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);


            dgv.Columns["Descripcion"].Visible = true;
            dgv.Columns["Descripcion"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["Descripcion"].HeaderText = "Unidad de Medida";
            dgv.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            CentrarCabecerasGrilla(this.dgvGrilla);
        }

        public override bool EjecutarComandoNuevo()
        {
            var fNuevo = new _00306_Abm_UnidadMedida(TipoOperacion.Nuevo);

            fNuevo.ShowDialog();

            return fNuevo.RelizoAlgunaOperacion;
        }

        public override bool EjecutarComandoEliminar()
        {
            var fEliminar = new _00306_Abm_UnidadMedida(TipoOperacion.Eliminar , _entidadId);

            fEliminar.ShowDialog();

            return fEliminar.RelizoAlgunaOperacion;
        }

        public override bool EjecutarComandoModificar()
        {
            var fModificar = new _00306_Abm_UnidadMedida(TipoOperacion.Modificar, _entidadId);

            fModificar.ShowDialog();

            return fModificar.RelizoAlgunaOperacion;
        }
    }
}
