namespace Presentacion.Core.Articulo
{
    using FormularioBase;
    using Presentacion.FormularioBase.Helpers;
    using Servicio.Interfaces.Marca;
    using StructureMap;
    using System.Windows.Forms;

    public partial class _00103_Abm_Marca : FormularioAbm
    {

        private readonly IMarcaServicio _marcaServicio;

        public _00103_Abm_Marca(TipoOperacion tipoOperacion , long? entidadId = null) : base(tipoOperacion , entidadId)
        {
            InitializeComponent();

            _marcaServicio = ObjectFactory.GetInstance<IMarcaServicio>();
            AgregarControlesObligatorios(txtDescripcion.Text, "Descripcion");
           
        }

        public override void CargarDatos(long? entidadId)
        {
            if (entidadId.HasValue && entidadId > 0)
            {
                var marcaServicio = _marcaServicio.GetById(entidadId.Value);

                txtDescripcion.Text = marcaServicio.Descripcion;
               
            }
            else
            {
                LimpiarControles(this);
                this.txtDescripcion.Focus();
            }
        }

        public override void EjecutarComandoNuevo()
        {
            _marcaServicio.Add(new Servicio.Interfaces.Marca.DTOs.MarcaDtos
            {
                Descripcion = txtDescripcion.Text
            });
        }

        public override void EjecutarComandoEliminar(long? entidadId)
        {
            _marcaServicio.Delete(entidadId.Value);
        }

        public override void EjecutarComandoModificar(long? entidadId)
        {
            _marcaServicio.Update(new Servicio.Interfaces.Marca.DTOs.MarcaDtos
            {
                Id = entidadId.Value,
                Descripcion = txtDescripcion.Text,

            });
        }

    }
}
