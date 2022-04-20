namespace Presentacion.Core.Articulo
{
    using FormularioBase;
    using Presentacion.FormularioBase.Helpers;
    using Servicio.Interfaces.ListaDePrecio;
    using StructureMap;
    using System.Windows.Forms;

    public partial class _00155_ListaPrecio : FormularioConsulta
    {
        private readonly IListaDePrecioServicios listaDePrecioServicio;
        public _00155_ListaPrecio()
        {
            InitializeComponent();

            listaDePrecioServicio = ObjectFactory.GetInstance<IListaDePrecioServicios>();
        }

        public override void ActualizarDatos(DataGridView dgv, string cadenaBuscar)
        {
            dgv.DataSource = listaDePrecioServicio.Get(!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);



            FormatearGrilla(dgv);
        }

        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);

            dgv.Columns["Descripcion"].Visible = true;
            dgv.Columns["Descripcion"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["Descripcion"].HeaderText = "Lista de Precio";
            dgv.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgv.Columns["PorcentajeGanancia"].Visible = true;
            dgv.Columns["PorcentajeGanancia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["PorcentajeGanancia"].HeaderText = "Porcentaje de Ganancia";
            dgv.Columns["PorcentajeGanancia"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgv.Columns["NecesitaAutorizacion"].Visible = true;
            dgv.Columns["NecesitaAutorizacion"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["NecesitaAutorizacion"].HeaderText = "Necesita Autorizacion";
            dgv.Columns["NecesitaAutorizacion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            CentrarCabecerasGrilla(this.dgvGrilla);
        }

        public override bool EjecutarComandoNuevo()
        {
            var fNuevo = new _00156_Abm_ListaPrecio(TipoOperacion.Nuevo);
            fNuevo.ShowDialog();

            return fNuevo.RelizoAlgunaOperacion;
        }

        public override bool EjecutarComandoModificar()
        {
            var fModificar = new _00156_Abm_ListaPrecio(TipoOperacion.Modificar, _entidadId);
            fModificar.ShowDialog();

            return fModificar.RelizoAlgunaOperacion;
        }

        public override bool EjecutarComandoEliminar()
        {
            var fEliminar = new _00156_Abm_ListaPrecio(TipoOperacion.Eliminar, _entidadId);
            fEliminar.ShowDialog();

            return fEliminar.RelizoAlgunaOperacion;
        }
    }
}
