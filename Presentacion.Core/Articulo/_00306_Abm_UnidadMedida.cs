namespace Presentacion.Core.Articulo
{
    using Presentacion.FormularioBase;
    using Presentacion.FormularioBase.Helpers;
    using Servicio.Interfaces.UnidadMedida;
    using StructureMap;

    public partial class _00306_Abm_UnidadMedida : FormularioAbm
    {
        private readonly IUnidadMedidaServicio _unidadMedidaServicio;
        public _00306_Abm_UnidadMedida(TipoOperacion tipoOperacion , long? entidadId = null )
            : base(tipoOperacion , entidadId)
        {
            InitializeComponent();

            _unidadMedidaServicio = ObjectFactory.GetInstance<IUnidadMedidaServicio>();

            AgregarControlesObligatorios(txtDescripcion.Text, "Descripcion");
        }

        public override void CargarDatos(long? entidadId)
        {
            if(entidadId.HasValue && entidadId > 0)
            {
                var entidad = _unidadMedidaServicio.GetById(entidadId.Value);

                txtDescripcion.Text = entidad.Descripcion;
            }
            else
            {
                LimpiarControles(this);
                txtDescripcion.Focus();
            }
        }

        public override void EjecutarComandoNuevo()
        {
            _unidadMedidaServicio.Add(new Servicio.Interfaces.UnidadMedida.DTOs.UnidadMedidaDto
            {
                Descripcion = txtDescripcion.Text
            });
        }

        public override void EjecutarComandoModificar(long? entidadId)
        {
            _unidadMedidaServicio.Update(new Servicio.Interfaces.UnidadMedida.DTOs.UnidadMedidaDto
            {
                Descripcion = txtDescripcion.Text,
                Id = entidadId.Value,

            });

        }

        public override void EjecutarComandoEliminar(long? entidadId)
        {
            _unidadMedidaServicio.Delete(entidadId.Value);
        }
    }
}
