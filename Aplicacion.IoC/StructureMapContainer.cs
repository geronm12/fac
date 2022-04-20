using System.Runtime.Remoting.Contexts;

namespace Aplicacion.IoC
{
    using Infraestructura;
    using System.Data.Entity;
    using Servicio.Implementacion.Provincia;
    using Servicio.Interfaces.Provincia;
    using Servicio.Implementacion.CondicionIva;
    using Servicio.Implementacion.Localidad;
    using Servicio.Implementacion.Persona;
    using Servicio.Interfaces.CondicionIva;
    using Servicio.Interfaces.Localidad;
    using Servicio.Interfaces.Persona;
    using StructureMap;
    using Dominio.Base;
    using Infraestructura.Repositorio;
    using Dominio.Entidades.Repositorio;
    using Servicio.Implementacion.Formulario;
    using Servicio.Implementacion.Perfil;
    using Servicio.Implementacion.PerfilFormulario;
    using Servicio.Implementacion.PerfilUsuario;
    using Servicio.Implementacion.Seguridad;
    using Servicio.Implementacion.Usuario;
    using Servicio.Interfaces.Formulario;
    using Servicio.Interfaces.Perfil;
    using Servicio.Interfaces.PerfilFormulario;
    using Servicio.Interfaces.PerfilUsuario;
    using Servicio.Interfaces.Seguridad;
    using Servicio.Interfaces.Usuario;
    using Dominio.Entidades.UnidadDeTrabajo;
    using Infraestructura.UnidadDeTrabajo;
    using Servicio.Interfaces.Articulo;
    using Servicio.Implementacion.Articulo;
    using Servicio.Interfaces.Marca;
    using Servicio.Implementacion.Marca;
    using Servicio.Interfaces.IVA;
    using Servicio.Implementacion.IVA;
    using Servicio.Interfaces.Rubro;
    using Servicio.Implementacion.Rubro;
    using Servicio.Interfaces.MotivoBaja;
    using Servicio.Implementacion.MotivoBaja;
    using Servicio.Interfaces.ListaDePrecio;
    using Servicio.Implementacion.ListaDePrecios;
    using Servicio.Interfaces.UnidadMedida;
    using Servicio.Implementacion.UnidadMedida;

    public class StructureMapContainer
    {
        public void Configure()
        {
            ObjectFactory.Configure(x =>
            {
                x.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.ConnectImplementationsToTypesClosing(typeof(IRepositorio<>));
                    scan.Assembly(GetType().Assembly);
                });

                x.For(typeof(IRepositorio<>)).Use(typeof(Repositorio<>));
                x.ForSingletonOf<DbContext>().HybridHttpOrThreadLocalScoped();

                x.For<IUnidadDeTrabajo>().Use<UnidadDeTrabajo>();

                x.For<IRepositorioEmpleado>().Use<RepositorioEmpleado>();
                x.For<IRepositorioCliente>().Use<RepositorioCliente>();
                x.For<IRepositorioProveedor>().Use<RepositorioProveedor>();

                // ======================================================== //

                x.For<IProvinciaServicio>().Use<ProvinciaServicio>();
                x.For<ILocalidadServicio>().Use<LocalidadServicio>();
                x.For<ICondicionIvaServicio>().Use<CondicionIvaServicio>();

                // ======================================================== //

                x.For<IPersonaServicio>().Use<PersonaServicio>();
                x.For<IEmpleadoServicio>().Use<EmpleadoServicio>();
                x.For<IClienteServicio>().Use<ClienteServicio>();
                x.For<IProveedorServicio>().Use<ProveedorServicio>();

                // ======================================================== //

                x.For<IArticuloServicio>().Use<ArticuloServicio>();
                x.For<IMarcaServicio>().Use<MarcaServicio>();
                x.For<IIVAServicio>().Use<IVAServicio>();
                x.For<IRubroServicio>().Use<RubroServicio>();
                x.For<IMotivoBajaServicio>().Use<MotivoBajaServicio>();
                x.For<IListaDePrecioServicios>().Use<ListaDePrecioServicios>();
                x.For<IUnidadMedidaServicio>().Use<UnidadMedidaServicio>();
              
                
                // ======================================================== //

                x.For<IUsuarioServicio>().Use<UsuarioServicio>();
                x.For<ISeguridadServicio>().Use<SeguridadServicio>();
                x.For<IFormularioServicio>().Use<FormularioServicio>();

                x.For<IPerfilServicio>().Use<PerfilServicio>();
                x.For<IPerfilFormularioServicio>().Use<PerfilFormularioServicio>();
                x.For<IPerfilUsuarioServicio>().Use<PerfilUsuarioServicio>();

                //Ejemplo de como declarar las propiedades en StructureMap
                //x.SetAllProperties(p => p.OfType<Implementacion>());
            });
        }
    }
}
