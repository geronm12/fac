namespace Presentacion.Core.Articulo
{
    using System.Windows.Forms;
    using FormularioBase;
    using Presentacion.FormularioBase.Helpers;
    using Servicio.Interfaces.Rubro;
    using StructureMap;

    public partial class _00104_Rubro : FormularioConsulta
    {
        private readonly IRubroServicio rubroServicio;
        public _00104_Rubro()
        {
            InitializeComponent();
            rubroServicio = ObjectFactory.GetInstance<IRubroServicio>();
        }

        public override void ActualizarDatos(DataGridView dgv, string cadenaBuscar)
        {
            dgvGrilla.DataSource = rubroServicio.Get(!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty );

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
            var fEliminar = new _00105_Abm_Rubro(TipoOperacion.Eliminar , _entidadId.Value);

            fEliminar.ShowDialog();

            return fEliminar.RelizoAlgunaOperacion;
        }

        public override bool EjecutarComandoModificar()
        {
            var fModificar = new _00105_Abm_Rubro(TipoOperacion.Modificar , _entidadId.Value);

            fModificar.ShowDialog();

            return fModificar.RelizoAlgunaOperacion;
        }

        public override bool EjecutarComandoNuevo()
        {
             var fNuevo = new _00105_Abm_Rubro(TipoOperacion.Nuevo);

            fNuevo.ShowDialog();

            return fNuevo.RelizoAlgunaOperacion;
        }
    }
}
