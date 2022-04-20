namespace Infraestructura.UnidadDeTrabajo
{
    using Dominio.Base;
    using Dominio.Entidades;
    using Dominio.Entidades.Repositorio;
    using Dominio.Entidades.UnidadDeTrabajo;

    using Repositorio;

    public class UnidadDeTrabajo : IUnidadDeTrabajo
    {
        private readonly DataContext _dataContext;

        public UnidadDeTrabajo(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        private IRepositorio<Usuario> _usuarioRepositorio;
        public IRepositorio<Usuario> UsuarioRepositorio
        {
            get { return _usuarioRepositorio ?? (_usuarioRepositorio = new Repositorio<Usuario>(_dataContext)); }
        }

        private IRepositorio<Provincia> _provinciaRepositorio;
        public IRepositorio<Provincia> ProvinciaRepositorio
        {
            get { return _provinciaRepositorio ?? (_provinciaRepositorio = new Repositorio<Provincia>(_dataContext)); }
        }

        private IRepositorio<Localidad> _localidadRepositorio;
        public IRepositorio<Localidad> LocalidadRepositorio
        {
            get { return _localidadRepositorio ?? (_localidadRepositorio = new Repositorio<Localidad>(_dataContext)); }
        }

        private IRepositorio<CondicionIva> _condicionIvaRepositorio;
        public IRepositorio<CondicionIva> CondicionIvaRepositorio
        {
            get { return _condicionIvaRepositorio ?? (_condicionIvaRepositorio = new Repositorio<CondicionIva>(_dataContext)); }
        }

        private IRepositorio<Articulo> _articuloRepositorio;

        public IRepositorio<Articulo> ArticuloRepositorio
        {
            get { return _articuloRepositorio ?? (_articuloRepositorio = new Repositorio<Articulo>(_dataContext)); }
        }

        private IRepositorio<Iva> _ivaRepositorio;

        public IRepositorio<Iva> IvaRepositorio
        {
            get { return _ivaRepositorio ?? (_ivaRepositorio = new Repositorio<Iva>(_dataContext)); }
        }

        private IRepositorioEmpleado _empleadoRepositorio;
        public IRepositorioEmpleado EmpleadoRepositorio
        {
            get { return _empleadoRepositorio ?? (_empleadoRepositorio = new RepositorioEmpleado(_dataContext)); }
        }

        private IRepositorioCliente _clienteRepositorio;
        public IRepositorioCliente ClienteRepositorio
        {
            get { return _clienteRepositorio ?? (_clienteRepositorio = new RepositorioCliente(_dataContext)); }
        }

        private IRepositorioProveedor _proveedorRepositorio;
        public IRepositorioProveedor ProveedorRepositorio
        {
            get { return _proveedorRepositorio ?? (_proveedorRepositorio = new RepositorioProveedor(_dataContext)); }
        }

        private IRepositorio<Perfil> _perfilRepositorio;
        public IRepositorio<Perfil> PerfilRepositorio
        {
            get { return _perfilRepositorio ?? (_perfilRepositorio = new Repositorio<Perfil>(_dataContext)); }
        }

        private IRepositorio<Formulario> _formularioRepositorio;
        public IRepositorio<Formulario> FormularioRepositorio
        {
            get { return _formularioRepositorio ?? (_formularioRepositorio = new Repositorio<Formulario>(_dataContext)); }
        }

        private IRepositorio<Marca> _marcaRepositorio;
        public IRepositorio<Marca> MarcaRepositorio 
        {
            get { return _marcaRepositorio ?? (_marcaRepositorio = new Repositorio<Marca>(_dataContext)); }
        }

        private IRepositorio<Rubro> _rubroRepositorio;
        public IRepositorio<Rubro> RubroRepositorio
        {
            get { return _rubroRepositorio ?? (_rubroRepositorio = new Repositorio<Rubro>(_dataContext)); }
        }

        private IRepositorio<MotivoBaja> _motivoBajaRepositorio;
        public IRepositorio<MotivoBaja> MotivoBajaRepositorio
        {
            get { return _motivoBajaRepositorio ?? (_motivoBajaRepositorio = new Repositorio<MotivoBaja>(_dataContext)); }
        }

        private IRepositorio<ListaPrecio> _listaPrecioRepositorio;
        public IRepositorio<ListaPrecio> ListaPrecioRepositorio
        {
            get { return _listaPrecioRepositorio ?? (_listaPrecioRepositorio = new Repositorio<ListaPrecio>(_dataContext)); }
        }

        private IRepositorio<UnidadMedida> _unidadMedidaRepositorio;
        public IRepositorio<UnidadMedida> UnidadMedidaRepositorio
        {
            get { return _unidadMedidaRepositorio ?? (_unidadMedidaRepositorio = new Repositorio<UnidadMedida>(_dataContext)); }
        }

        public void Commit()
        {
            _dataContext.SaveChanges();
        }

        public void Disposed()
        {
            _dataContext.Dispose();
        }
    }
}
