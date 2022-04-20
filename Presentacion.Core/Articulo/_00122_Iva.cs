namespace Presentacion.Core.Articulo
{
    using FormularioBase;
    using Presentacion.FormularioBase.Helpers;
    using Servicio.Interfaces.IVA;
    using StructureMap;
    using System.Windows.Forms;

    public partial class _00122_Iva : FormularioConsulta
    {
        private readonly IIVAServicio _ivaServicio;
        public _00122_Iva()
        {
            InitializeComponent();

            _ivaServicio = ObjectFactory.GetInstance<IIVAServicio>();
        }

        public override void ActualizarDatos(DataGridView dgv, string cadenaBuscar)
        {
            dgv.DataSource = _ivaServicio.Get(!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);



            FormatearGrilla(dgv);
        }

        public override void FormatearGrilla(DataGridView dgv)
        {
           

            dgv.Columns["Descripcion"].Visible = true;
            dgv.Columns["Descripcion"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["Descripcion"].HeaderText = "Iva";
            dgv.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgv.Columns["Porcentaje"].Visible = true;
            dgv.Columns["Porcentaje"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["Porcentaje"].HeaderText = "Porcentaje";
            dgv.Columns["Porcentaje"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgv.Columns["RowVersion"].Visible = false;
            dgv.Columns["Id"].Visible = false;
            dgv.Columns["EstaEliminadoStr"].Visible = false;
            dgv.Columns["EstaEliminado"].Visible = false;

            CentrarCabecerasGrilla(this.dgvGrilla);
        }

        public override bool EjecutarComandoNuevo()
        {
            var fNuevo = new _00123_Abm_Iva(TipoOperacion.Nuevo);
            fNuevo.ShowDialog();

            return fNuevo.RelizoAlgunaOperacion;
        }

        public override bool EjecutarComandoModificar()
        {
            var fModificar = new _00123_Abm_Iva(TipoOperacion.Modificar, _entidadId);
            fModificar.ShowDialog();

            return fModificar.RelizoAlgunaOperacion;
        }

        public override bool EjecutarComandoEliminar()
        {
            var fEliminar = new _00123_Abm_Iva(TipoOperacion.Eliminar, _entidadId);
            fEliminar.ShowDialog();

            return fEliminar.RelizoAlgunaOperacion;
        }
    }
}
