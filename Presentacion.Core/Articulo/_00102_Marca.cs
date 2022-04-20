namespace Presentacion.Core.Articulo
{
    using System.Windows.Forms;
    using FormularioBase;
    using Presentacion.FormularioBase.Helpers;
    using Servicio.Interfaces.Marca;
    using StructureMap;

    public partial class _00102_Marca : FormularioConsulta
    {
        private readonly IMarcaServicio _marcaServicio;
        public _00102_Marca()
        {
            InitializeComponent();

            _marcaServicio = ObjectFactory.GetInstance<IMarcaServicio>();
        }

        public override void ActualizarDatos(DataGridView dgv, string cadenaBuscar)
        {
            dgvGrilla.DataSource = _marcaServicio.Get(!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            FormatearGrilla(dgv);
        }

        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);


            dgv.Columns["Descripcion"].Visible = true;
            dgv.Columns["Descripcion"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["Descripcion"].HeaderText = "Iva";
            dgv.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            CentrarCabecerasGrilla(this.dgvGrilla);
        }

        public override bool EjecutarComandoEliminar()
        {
            var fEliminar = new _00103_Abm_Marca(TipoOperacion.Eliminar, _entidadId.Value);

            fEliminar.ShowDialog();

            return fEliminar.RelizoAlgunaOperacion;
        }

        public override bool EjecutarComandoNuevo()
        {
            var fNuevo = new _00103_Abm_Marca(TipoOperacion.Nuevo);

            fNuevo.ShowDialog();

            return fNuevo.RelizoAlgunaOperacion;
        }

        public override bool EjecutarComandoModificar()
        {
            var fModificar = new _00103_Abm_Marca(TipoOperacion.Modificar, _entidadId.Value);

            fModificar.ShowDialog();

            return fModificar.RelizoAlgunaOperacion;
        }
    }
}
