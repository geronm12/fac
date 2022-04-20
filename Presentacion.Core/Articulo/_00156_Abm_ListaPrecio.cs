namespace Presentacion.Core.Articulo
{
    using System;
    using Presentacion.FormularioBase;
    using Presentacion.FormularioBase.Helpers;
    using Servicio.Interfaces.ListaDePrecio;
    using StructureMap;

    public partial class _00156_Abm_ListaPrecio : FormularioAbm
    {
        private readonly IListaDePrecioServicios _listadePrecioServicio;
        public _00156_Abm_ListaPrecio(TipoOperacion tipoOperacion, long? entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();
           
            _listadePrecioServicio = ObjectFactory.GetInstance<IListaDePrecioServicios>();

            AgregarControlesObligatorios(txtDescripcion, "Descripcion");
            AgregarControlesObligatorios(nudPorcentaje, "Porcentaje");
            AgregarControlesObligatorios(chbAutorizacion, "Autorizacion");
        }

        public override void CargarDatos(long? entidadId)
        {
            if (entidadId.HasValue && entidadId > 0)
            {
                var listaDePrecio = _listadePrecioServicio.GetById(entidadId.Value);

                txtDescripcion.Text = listaDePrecio.Descripcion;
                nudPorcentaje.Value = listaDePrecio.PorcentajeGanancia;
              
                if(listaDePrecio.NecesitaAutorizacion == true)
                {
                    chbAutorizacion.Checked = true;
                }
                else
                {
                    chbAutorizacion.Checked = false;
                }
            }
            else
            {
                LimpiarControles(this);
                this.txtDescripcion.Focus();
            }
        }

        public override void EjecutarComandoNuevo()
        {
            _listadePrecioServicio.Add(new Servicio.Interfaces.ListaDePrecio.DTOs.ListaDePrecioDto
            {
                Descripcion = txtDescripcion.Text,
                PorcentajeGanancia = nudPorcentaje.Value,
                NecesitaAutorizacion = ValorCheckBox(),

            }) ;
        }

        private bool ValorCheckBox()
        {
            if (chbAutorizacion.Checked)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void EjecutarComandoModificar(long? entidadId)
        {
            _listadePrecioServicio.Update(new Servicio.Interfaces.ListaDePrecio.DTOs.ListaDePrecioDto
            {
                Id = entidadId.Value,
                Descripcion = txtDescripcion.Text,
                PorcentajeGanancia = nudPorcentaje.Value,
                NecesitaAutorizacion = ValorCheckBox()

            }) ;
        }

        public override void EjecutarComandoEliminar(long? entidadId)
        {
            _listadePrecioServicio.Delete(entidadId.Value);
        }
    }
}
