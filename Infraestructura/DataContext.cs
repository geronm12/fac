namespace Infraestructura
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using Dominio.Entidades;
    using static Aplicacion.Conexion.CadenaConexion;

    public class DataContext : DbContext
    {
        public DataContext()
            : base(ObtenerCadenaConexion)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, Migrations.Configuration>());

            ((IObjectContextAdapter) this).ObjectContext.CommandTimeout = 600;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Properties<string>()
                .Configure(x=>x.HasColumnType("varchar"));

            base.OnModelCreating(modelBuilder);
        }

        // Mapeo de las Entidades
        public IDbSet<Persona> Personas { get; set; }
        
        public IDbSet<Empleado> Empleados { get; set; }

        public IDbSet<Cliente> Clientes { get; set; }

        public IDbSet<Proveedor> Proveedores { get; set; }

        public IDbSet<CondicionIva> CondicionIvas { get; set; }

        public IDbSet<Provincia> Provincias { get; set; }

        public IDbSet<Localidad> Localidades { get; set; }

        public IDbSet<Usuario> Usuarios { get; set; }

        public IDbSet<Formulario> Formularios { get; set; }

        public IDbSet<Perfil> Perfiles { get; set; }

        public IDbSet<Articulo> Articulos { get; set; }
    }
}
