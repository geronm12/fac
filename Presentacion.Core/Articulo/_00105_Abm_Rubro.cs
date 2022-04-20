namespace Presentacion.Core.Articulo
{
    using FormularioBase;
    using Presentacion.FormularioBase.Helpers;
    using Servicio.Interfaces.Rubro;
    using StructureMap;
    using System.Windows.Forms;

    public partial class _00105_Abm_Rubro : FormularioAbm
    {
        private readonly IRubroServicio _rubroServicio;
        public _00105_Abm_Rubro(TipoOperacion tipoOperacion, long? entidadId = null) 
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();
            _rubroServicio = ObjectFactory.GetInstance<IRubroServicio>();
            AgregarControlesObligatorios(txtDescripcion.Text, "Descripcion");
        }

        public override void CargarDatos(long? entidadId)
        {
            if(entidadId.HasValue && entidadId > 0)
            {
                var rubroServicio = _rubroServicio.GetById(entidadId.Value);

                txtDescripcion.Text = rubroServicio.Descripcion;
            }
            else
            {
              
                LimpiarControles(this);
                txtDescripcion.Focus();
            }
        }

        public override void EjecutarComandoNuevo()
        {
            _rubroServicio.Add(new Servicio.Interfaces.Rubro.RubroDTOs.RubroDto
            {
                Descripcion = txtDescripcion.Text
            });
        }

        public override void EjecutarComandoEliminar(long? entidadId)
        {
            _rubroServicio.Delete(entidadId.Value);
        }

        public override void EjecutarComandoModificar(long? entidadId)
        {
            _rubroServicio.Update(new Servicio.Interfaces.Rubro.RubroDTOs.RubroDto
            {
                Id = entidadId.Value,
                Descripcion = txtDescripcion.Text,
              
            });
        }
    }
}
