namespace Presentacion.Core.Articulo
{
    using System.Windows.Forms;
    using FormularioBase;
    using Presentacion.FormularioBase.Helpers;
    using Servicio.Interfaces.Articulo;
    using Servicio.Interfaces.IVA;
    using Servicio.Interfaces.Marca;
    using Servicio.Interfaces.Rubro;
    using Servicio.Interfaces.UnidadMedida;
    using StructureMap;

    public partial class _00100_Articulo : FormularioConsulta
    {
        private readonly IUnidadMedidaServicio _unidadMedidaServicio;
        private readonly IMarcaServicio _marcaServicio;
        private readonly IIVAServicio _ivaServicio;
        private readonly IRubroServicio _rubroServicio;

        private readonly IArticuloServicio articuloServicio;
        public _00100_Articulo()
        {
            InitializeComponent();

            _marcaServicio = ObjectFactory.GetInstance<IMarcaServicio>();
            _ivaServicio = ObjectFactory.GetInstance<IIVAServicio>();
            _rubroServicio = ObjectFactory.GetInstance<IRubroServicio>();
            _unidadMedidaServicio = ObjectFactory.GetInstance<IUnidadMedidaServicio>();
            articuloServicio = ObjectFactory.GetInstance<IArticuloServicio>();
        }

        public override void ActualizarDatos(DataGridView dgv, string cadenaBuscar)
        {
            base.ActualizarDatos(dgv, cadenaBuscar);

            dgvGrilla.DataSource = articuloServicio.Get(!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            FormatearGrilla(dgv);
        }

        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);

            dgv.Columns["Descripcion"].Visible = true;
            dgv.Columns["Descripcion"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["Descripcion"].HeaderText = "Articulo";
            dgv.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgv.Columns["PrecioCosto"].Visible = true;
            dgv.Columns["PrecioCosto"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["PrecioCosto"].HeaderText = "Articulo";
            dgv.Columns["PrecioCosto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgv.Columns["Stock"].Visible = true;
            dgv.Columns["Stock"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["Stock"].HeaderText = "Articulo";
            dgv.Columns["Stock"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            CentrarCabecerasGrilla(this.dgvGrilla);
        }

       

        public override bool EjecutarComandoNuevo()
        {
            var fNuevo = new _00101_Abm_Articulo(TipoOperacion.Nuevo);

            fNuevo.ShowDialog();

            return fNuevo.RelizoAlgunaOperacion;
        }

        public override bool EjecutarComandoModificar()
        {
            var fModificar = new _00101_Abm_Articulo(TipoOperacion.Modificar, _entidadId.Value);

            fModificar.ShowDialog();

            return fModificar.RelizoAlgunaOperacion;
        }

        public override bool EjecutarComandoEliminar()
        {
            var fEliminar = new _00101_Abm_Articulo(TipoOperacion.Eliminar , _entidadId.Value);

            fEliminar.ShowDialog();

            return fEliminar.RelizoAlgunaOperacion;
        }
    }
}
