namespace Infraestructura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articulos",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        MarcaId = c.Long(nullable: false),
                        RubroId = c.Long(nullable: false),
                        UnidadMedidaId = c.Long(nullable: false),
                        IvaId = c.Long(nullable: false),
                        Codigo = c.Int(nullable: false),
                        CodigoBarra = c.String(nullable: false, maxLength: 8000, unicode: false),
                        Abreviatura = c.String(maxLength: 8000, unicode: false),
                        Descripcion = c.String(nullable: false, maxLength: 400, unicode: false),
                        Detalle = c.String(maxLength: 4000, unicode: false),
                        Ubicacion = c.String(maxLength: 400, unicode: false),
                        Foto = c.Binary(nullable: false),
                        ActivarLimiteVenta = c.Boolean(nullable: false),
                        LimiteVenta = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ActivarHoraVenta = c.Boolean(nullable: false),
                        HoraLimiteVenta = c.DateTime(nullable: false),
                        PermiteStockNegativo = c.Boolean(nullable: false),
                        DescuentaStock = c.Boolean(nullable: false),
                        Stock = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StockMinimo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EstaEliminado = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ivas", t => t.IvaId)
                .ForeignKey("dbo.Marcas", t => t.MarcaId)
                .ForeignKey("dbo.Rubros", t => t.RubroId)
                .ForeignKey("dbo.UnidadMedidas", t => t.UnidadMedidaId)
                .Index(t => t.MarcaId)
                .Index(t => t.RubroId)
                .Index(t => t.UnidadMedidaId)
                .Index(t => t.IvaId);
            
            CreateTable(
                "dbo.BajaArticulos",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ArticuloId = c.Long(nullable: false),
                        MotivoBajaId = c.Long(nullable: false),
                        Cantidad = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Fecha = c.DateTime(nullable: false),
                        Observacion = c.String(nullable: false, maxLength: 400, unicode: false),
                        EstaEliminado = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articulos", t => t.ArticuloId)
                .ForeignKey("dbo.MotivoBajas", t => t.MotivoBajaId)
                .Index(t => t.ArticuloId)
                .Index(t => t.MotivoBajaId);
            
            CreateTable(
                "dbo.MotivoBajas",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 250, unicode: false),
                        EstaEliminado = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DetalleArticuloCompuestos",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ArticuloPadreId = c.Long(nullable: false),
                        ArticuloHijoId = c.Long(nullable: false),
                        Cantidad = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EstaEliminado = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articulos", t => t.ArticuloHijoId)
                .ForeignKey("dbo.Articulos", t => t.ArticuloPadreId)
                .Index(t => t.ArticuloPadreId)
                .Index(t => t.ArticuloHijoId);
            
            CreateTable(
                "dbo.Ivas",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 250, unicode: false),
                        Porcentaje = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EstaEliminado = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Marcas",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 250, unicode: false),
                        EstaEliminado = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Precios",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ListaPrecioId = c.Long(nullable: false),
                        ArticuloId = c.Long(nullable: false),
                        PrecioCosto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrecioPublico = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FechaActualizacion = c.DateTime(nullable: false),
                        EstaEliminado = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articulos", t => t.ArticuloId)
                .ForeignKey("dbo.ListaPrecios", t => t.ListaPrecioId)
                .Index(t => t.ListaPrecioId)
                .Index(t => t.ArticuloId);
            
            CreateTable(
                "dbo.ListaPrecios",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 250, unicode: false),
                        PorcentajeGanancia = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NecesitaAutorizacion = c.Boolean(nullable: false),
                        EstaEliminado = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Configuraciones",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LocalidadId = c.Long(nullable: false),
                        RazonSocial = c.String(nullable: false, maxLength: 250, unicode: false),
                        Cuit = c.String(nullable: false, maxLength: 15, unicode: false),
                        Telefono = c.String(maxLength: 35, unicode: false),
                        Celular = c.String(maxLength: 35, unicode: false),
                        Direccion = c.String(nullable: false, maxLength: 400, unicode: false),
                        Email = c.String(nullable: false, maxLength: 250, unicode: false),
                        FacturaDescuentaStock = c.Boolean(nullable: false),
                        PresupuestoDescuentaStock = c.Boolean(nullable: false),
                        RemitoDescuentaStock = c.Boolean(nullable: false),
                        ActualizaCostoDesdeCompra = c.Boolean(nullable: false),
                        ModificaPrecioVentaDesdeCompra = c.Boolean(nullable: false),
                        TipoFormaPagoPorDefectoCompraId = c.Long(nullable: false),
                        TipoFormaPagoPorDefectoVentaId = c.Long(nullable: false),
                        ObservacionEnPieFactura = c.String(nullable: false, maxLength: 400, unicode: false),
                        UnificarRenglonesIngresarMismoProducto = c.Boolean(nullable: false),
                        ListaPrecioPorDefectoId = c.Long(nullable: false),
                        IngresoManualCajaInicial = c.Boolean(nullable: false),
                        EstaEliminado = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ListaPrecios", t => t.ListaPrecioPorDefectoId)
                .ForeignKey("dbo.Localidad", t => t.LocalidadId)
                .ForeignKey("dbo.TipoFormaPagos", t => t.TipoFormaPagoPorDefectoCompraId)
                .ForeignKey("dbo.TipoFormaPagos", t => t.TipoFormaPagoPorDefectoVentaId)
                .Index(t => t.LocalidadId)
                .Index(t => t.TipoFormaPagoPorDefectoCompraId)
                .Index(t => t.TipoFormaPagoPorDefectoVentaId)
                .Index(t => t.ListaPrecioPorDefectoId);
            
            CreateTable(
                "dbo.TipoFormaPagos",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 250, unicode: false),
                        EstaEliminado = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rubros",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 250, unicode: false),
                        EstaEliminado = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UnidadMedidas",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 250, unicode: false),
                        EstaEliminado = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.DetalleComprobantes", "ArticuloId");
            AddForeignKey("dbo.DetalleComprobantes", "ArticuloId", "dbo.Articulos", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articulos", "UnidadMedidaId", "dbo.UnidadMedidas");
            DropForeignKey("dbo.Articulos", "RubroId", "dbo.Rubros");
            DropForeignKey("dbo.Precios", "ListaPrecioId", "dbo.ListaPrecios");
            DropForeignKey("dbo.Configuraciones", "TipoFormaPagoPorDefectoVentaId", "dbo.TipoFormaPagos");
            DropForeignKey("dbo.Configuraciones", "TipoFormaPagoPorDefectoCompraId", "dbo.TipoFormaPagos");
            DropForeignKey("dbo.Configuraciones", "LocalidadId", "dbo.Localidad");
            DropForeignKey("dbo.Configuraciones", "ListaPrecioPorDefectoId", "dbo.ListaPrecios");
            DropForeignKey("dbo.Precios", "ArticuloId", "dbo.Articulos");
            DropForeignKey("dbo.Articulos", "MarcaId", "dbo.Marcas");
            DropForeignKey("dbo.Articulos", "IvaId", "dbo.Ivas");
            DropForeignKey("dbo.DetalleComprobantes", "ArticuloId", "dbo.Articulos");
            DropForeignKey("dbo.DetalleArticuloCompuestos", "ArticuloPadreId", "dbo.Articulos");
            DropForeignKey("dbo.DetalleArticuloCompuestos", "ArticuloHijoId", "dbo.Articulos");
            DropForeignKey("dbo.BajaArticulos", "MotivoBajaId", "dbo.MotivoBajas");
            DropForeignKey("dbo.BajaArticulos", "ArticuloId", "dbo.Articulos");
            DropIndex("dbo.Configuraciones", new[] { "ListaPrecioPorDefectoId" });
            DropIndex("dbo.Configuraciones", new[] { "TipoFormaPagoPorDefectoVentaId" });
            DropIndex("dbo.Configuraciones", new[] { "TipoFormaPagoPorDefectoCompraId" });
            DropIndex("dbo.Configuraciones", new[] { "LocalidadId" });
            DropIndex("dbo.Precios", new[] { "ArticuloId" });
            DropIndex("dbo.Precios", new[] { "ListaPrecioId" });
            DropIndex("dbo.DetalleComprobantes", new[] { "ArticuloId" });
            DropIndex("dbo.DetalleArticuloCompuestos", new[] { "ArticuloHijoId" });
            DropIndex("dbo.DetalleArticuloCompuestos", new[] { "ArticuloPadreId" });
            DropIndex("dbo.BajaArticulos", new[] { "MotivoBajaId" });
            DropIndex("dbo.BajaArticulos", new[] { "ArticuloId" });
            DropIndex("dbo.Articulos", new[] { "IvaId" });
            DropIndex("dbo.Articulos", new[] { "UnidadMedidaId" });
            DropIndex("dbo.Articulos", new[] { "RubroId" });
            DropIndex("dbo.Articulos", new[] { "MarcaId" });
            DropTable("dbo.UnidadMedidas");
            DropTable("dbo.Rubros");
            DropTable("dbo.TipoFormaPagos");
            DropTable("dbo.Configuraciones");
            DropTable("dbo.ListaPrecios");
            DropTable("dbo.Precios");
            DropTable("dbo.Marcas");
            DropTable("dbo.Ivas");
            DropTable("dbo.DetalleArticuloCompuestos");
            DropTable("dbo.MotivoBajas");
            DropTable("dbo.BajaArticulos");
            DropTable("dbo.Articulos");
        }
    }
}
