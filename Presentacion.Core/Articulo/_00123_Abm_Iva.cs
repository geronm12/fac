namespace Presentacion.Core.Articulo
{
    using FormularioBase;
    using Presentacion.FormularioBase.Helpers;
    using Servicio.Interfaces.IVA;
    using StructureMap;

    public partial class _00123_Abm_Iva : FormularioAbm
    {
        private readonly IIVAServicio _ivaServicio;
        public _00123_Abm_Iva(TipoOperacion tipoOperacion, long? entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();
            _ivaServicio = ObjectFactory.GetInstance<IIVAServicio>();

            AgregarControlesObligatorios(this.txtDescripcion, "Descripcion");
            AgregarControlesObligatorios(this.nudAlicuota, "Alicuota");
        }

        public override void CargarDatos(long? entidadId)
        {
            if(entidadId.HasValue && entidadId > 0)
            {
                var ivaServicio = _ivaServicio.GetById(entidadId.Value);

                txtDescripcion.Text = ivaServicio.Descripcion;
                nudAlicuota.Value = ivaServicio.Porcentaje;
            }
            else
            {
                LimpiarControles(this);
                this.txtDescripcion.Focus();
            }
        }

        public override void EjecutarComandoNuevo()
        {
            _ivaServicio.Add(new Servicio.Interfaces.IVA.DTOs.IVADto
            {
                Descripcion = txtDescripcion.Text,
                Porcentaje = nudAlicuota.Value

            }) ;
        }

        public override void EjecutarComandoModificar(long? entidadId)
        {
            _ivaServicio.Update(new Servicio.Interfaces.IVA.DTOs.IVADto
            {
                Id = entidadId.Value,
                Descripcion = txtDescripcion.Text,
                Porcentaje = nudAlicuota.Value,
            });
        }

        public override void EjecutarComandoEliminar(long? entidadId)
        {
            _ivaServicio.Delete(entidadId.Value);
        }

    }
    
}
