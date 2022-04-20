namespace Presentacion.Core.Articulo
{
    using FormularioBase;
    using Presentacion.FormularioBase.Helpers;
    using Servicio.Interfaces.MotivoBaja;
    using StructureMap;

    public partial class _00107_Abm_MotivoBajaArticulo : FormularioAbm
    {
        private readonly IMotivoBajaServicio _motivoBajaServicio;
        public _00107_Abm_MotivoBajaArticulo(TipoOperacion tipoOperacion , long? entidadId = null) 
            : base(tipoOperacion , entidadId)
        {
             InitializeComponent();

            _motivoBajaServicio = ObjectFactory.GetInstance<IMotivoBajaServicio>();

            AgregarControlesObligatorios(txtDescripcion, "Descripcion");
        }

        public override void CargarDatos(long? entidadId)
        {
            if(entidadId.HasValue && entidadId > 0)
            {
                var entidad = _motivoBajaServicio.GetById(entidadId.Value);

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
            _motivoBajaServicio.Add(new Servicio.Interfaces.MotivoBaja.DTOs.MotivoBajaDto
            {
                Descripcion = txtDescripcion.Text
            }) ;
        }

        public override void EjecutarComandoEliminar(long? entidadId)
        {
            _motivoBajaServicio.Delete(entidadId.Value);
        }

        public override void EjecutarComandoModificar(long? entidadId)
        {
            _motivoBajaServicio.Update(new Servicio.Interfaces.MotivoBaja.DTOs.MotivoBajaDto
            {
                Id = entidadId.Value,
                Descripcion = txtDescripcion.Text
            });
        }

    }
}
