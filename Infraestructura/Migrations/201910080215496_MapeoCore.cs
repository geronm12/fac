namespace Infraestructura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MapeoCore : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cheques",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ClienteId = c.Long(nullable: false),
                        BancoId = c.Long(nullable: false),
                        Numero = c.String(nullable: false, maxLength: 100, unicode: false),
                        FechaVencimiento = c.DateTime(nullable: false),
                        EstaRechazado = c.Boolean(nullable: false),
                        EstaEliminado = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bancos", t => t.BancoId)
                .ForeignKey("dbo.Persona_Cliente", t => t.ClienteId)
                .Index(t => t.ClienteId)
                .Index(t => t.BancoId);
            
            CreateTable(
                "dbo.Bancos",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 250, unicode: false),
                        EstaEliminado = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CuentaBancarias",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        BancoId = c.Long(nullable: false),
                        Numero = c.String(nullable: false, maxLength: 100, unicode: false),
                        Titular = c.String(nullable: false, maxLength: 250, unicode: false),
                        EstaEliminado = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bancos", t => t.BancoId)
                .Index(t => t.BancoId);
            
            CreateTable(
                "dbo.DepositoCheques",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ChequeId = c.Long(nullable: false),
                        CuentaBancariaId = c.Long(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        EstaEliminado = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cheques", t => t.ChequeId)
                .ForeignKey("dbo.CuentaBancarias", t => t.CuentaBancariaId)
                .Index(t => t.ChequeId)
                .Index(t => t.CuentaBancariaId);
            
            CreateTable(
                "dbo.FormaPago_Cheque",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ChequeId = c.Long(nullable: false),
                        ComprobanteId = c.Long(nullable: false),
                        TipoPago = c.Int(nullable: false),
                        Monto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EstaEliminado = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cheques", t => t.ChequeId)
                .ForeignKey("dbo.Comprobantes", t => t.ComprobanteId)
                .Index(t => t.ChequeId)
                .Index(t => t.ComprobanteId);
            
            CreateTable(
                "dbo.Comprobantes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UsuarioId = c.Long(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        Numero = c.Int(nullable: false),
                        SubTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Descuento = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TipoComprobante = c.Int(nullable: false),
                        EstaEliminado = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        ClienteId = c.Long(),
                        Discriminator = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId)
                .ForeignKey("dbo.Persona_Cliente", t => t.ClienteId)
                .Index(t => t.UsuarioId)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.DetalleComprobantes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ComprobanteId = c.Long(nullable: false),
                        ArticuloId = c.Long(nullable: false),
                        Codigo = c.String(nullable: false, maxLength: 8000, unicode: false),
                        Descripcion = c.String(nullable: false, maxLength: 8000, unicode: false),
                        Cantidad = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EstaEliminado = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comprobantes", t => t.ComprobanteId)
                .Index(t => t.ComprobanteId);
            
            CreateTable(
                "dbo.Movimientos",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CajaId = c.Long(nullable: false),
                        ComprobanteId = c.Long(nullable: false),
                        UsuarioId = c.Long(nullable: false),
                        Monto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Fecha = c.DateTime(nullable: false),
                        Descripcion = c.String(nullable: false, maxLength: 4000, unicode: false),
                        TipoMovimiento = c.Int(nullable: false),
                        EstaEliminado = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cajas", t => t.CajaId)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId)
                .ForeignKey("dbo.Comprobantes", t => t.ComprobanteId)
                .Index(t => t.CajaId)
                .Index(t => t.ComprobanteId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Cajas",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UsuarioAperturaId = c.Long(nullable: false),
                        MontoInicial = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FechaApertura = c.DateTime(nullable: false),
                        FechaCierre = c.DateTime(),
                        UsuarioCierreId = c.Long(),
                        MontoCierre = c.Decimal(precision: 18, scale: 2),
                        TotalVentaEfectivo = c.Decimal(precision: 18, scale: 2),
                        TotalCobranzaEfectivo = c.Decimal(precision: 18, scale: 2),
                        TotalSalidaCompras = c.Decimal(precision: 18, scale: 2),
                        TotalSalidaGastos = c.Decimal(precision: 18, scale: 2),
                        TotalSalidaNotaCreditos = c.Decimal(precision: 18, scale: 2),
                        TotalTarjetaEntrada = c.Decimal(precision: 18, scale: 2),
                        TotalChequeEntrada = c.Decimal(precision: 18, scale: 2),
                        TotalCuentaCorrienteEntrada = c.Decimal(precision: 18, scale: 2),
                        TotalTarjetaSalida = c.Decimal(precision: 18, scale: 2),
                        TotalChequeSalida = c.Decimal(precision: 18, scale: 2),
                        TotalCuentaCorrienteSalida = c.Decimal(precision: 18, scale: 2),
                        EstaEliminado = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioAperturaId)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioCierreId)
                .Index(t => t.UsuarioAperturaId)
                .Index(t => t.UsuarioCierreId);
            
            CreateTable(
                "dbo.DetalleCajas",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CajaId = c.Long(nullable: false),
                        TipoPago = c.Int(nullable: false),
                        Monto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EstaEliminado = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cajas", t => t.CajaId)
                .Index(t => t.CajaId);
            
            CreateTable(
                "dbo.CuentaCorrientes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UsuarioId = c.Long(nullable: false),
                        ClienteId = c.Long(nullable: false),
                        Descripcion = c.String(nullable: false, maxLength: 400, unicode: false),
                        Monto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Fecha = c.DateTime(nullable: false),
                        TipoMovimiento = c.Int(nullable: false),
                        EstaEliminado = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Persona_Cliente", t => t.ClienteId)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId)
                .Index(t => t.UsuarioId)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.FormaPagoCtaCtes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ClienteId = c.Long(nullable: false),
                        ComprobanteId = c.Long(nullable: false),
                        TipoPago = c.Int(nullable: false),
                        Monto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EstaEliminado = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Persona_Cliente", t => t.ClienteId)
                .ForeignKey("dbo.Comprobantes", t => t.ComprobanteId)
                .Index(t => t.ClienteId)
                .Index(t => t.ComprobanteId);
            
            CreateTable(
                "dbo.Comprobantes_Compra",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        ProveedorId = c.Long(nullable: false),
                        FechaEntrega = c.DateTime(nullable: false),
                        Iva27 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Iva21 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Iva105 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrecepcionTemp = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrecepcionPyP = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrecepcionIva = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrecepcionIB = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comprobantes", t => t.Id)
                .ForeignKey("dbo.Persona_Proveedor", t => t.ProveedorId)
                .Index(t => t.Id)
                .Index(t => t.ProveedorId);
            
            CreateTable(
                "dbo.Comprobantes_NotaCredito",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        ComprobanteId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comprobantes", t => t.Id)
                .ForeignKey("dbo.Comprobantes", t => t.ComprobanteId)
                .Index(t => t.Id)
                .Index(t => t.ComprobanteId);
            
            CreateTable(
                "dbo.Comprobantes_Presupuesto",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        ClienteId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comprobantes", t => t.Id)
                .ForeignKey("dbo.Persona_Cliente", t => t.ClienteId)
                .Index(t => t.Id)
                .Index(t => t.ClienteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comprobantes_Presupuesto", "ClienteId", "dbo.Persona_Cliente");
            DropForeignKey("dbo.Comprobantes_Presupuesto", "Id", "dbo.Comprobantes");
            DropForeignKey("dbo.Comprobantes_NotaCredito", "ComprobanteId", "dbo.Comprobantes");
            DropForeignKey("dbo.Comprobantes_NotaCredito", "Id", "dbo.Comprobantes");
            DropForeignKey("dbo.Comprobantes_Compra", "ProveedorId", "dbo.Persona_Proveedor");
            DropForeignKey("dbo.Comprobantes_Compra", "Id", "dbo.Comprobantes");
            DropForeignKey("dbo.FormaPagoCtaCtes", "ComprobanteId", "dbo.Comprobantes");
            DropForeignKey("dbo.FormaPagoCtaCtes", "ClienteId", "dbo.Persona_Cliente");
            DropForeignKey("dbo.FormaPago_Cheque", "ComprobanteId", "dbo.Comprobantes");
            DropForeignKey("dbo.Comprobantes", "ClienteId", "dbo.Persona_Cliente");
            DropForeignKey("dbo.Movimientos", "ComprobanteId", "dbo.Comprobantes");
            DropForeignKey("dbo.Movimientos", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.CuentaCorrientes", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.CuentaCorrientes", "ClienteId", "dbo.Persona_Cliente");
            DropForeignKey("dbo.Comprobantes", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.Cajas", "UsuarioCierreId", "dbo.Usuarios");
            DropForeignKey("dbo.Cajas", "UsuarioAperturaId", "dbo.Usuarios");
            DropForeignKey("dbo.Movimientos", "CajaId", "dbo.Cajas");
            DropForeignKey("dbo.DetalleCajas", "CajaId", "dbo.Cajas");
            DropForeignKey("dbo.DetalleComprobantes", "ComprobanteId", "dbo.Comprobantes");
            DropForeignKey("dbo.FormaPago_Cheque", "ChequeId", "dbo.Cheques");
            DropForeignKey("dbo.Cheques", "ClienteId", "dbo.Persona_Cliente");
            DropForeignKey("dbo.DepositoCheques", "CuentaBancariaId", "dbo.CuentaBancarias");
            DropForeignKey("dbo.DepositoCheques", "ChequeId", "dbo.Cheques");
            DropForeignKey("dbo.CuentaBancarias", "BancoId", "dbo.Bancos");
            DropForeignKey("dbo.Cheques", "BancoId", "dbo.Bancos");
            DropIndex("dbo.Comprobantes_Presupuesto", new[] { "ClienteId" });
            DropIndex("dbo.Comprobantes_Presupuesto", new[] { "Id" });
            DropIndex("dbo.Comprobantes_NotaCredito", new[] { "ComprobanteId" });
            DropIndex("dbo.Comprobantes_NotaCredito", new[] { "Id" });
            DropIndex("dbo.Comprobantes_Compra", new[] { "ProveedorId" });
            DropIndex("dbo.Comprobantes_Compra", new[] { "Id" });
            DropIndex("dbo.FormaPagoCtaCtes", new[] { "ComprobanteId" });
            DropIndex("dbo.FormaPagoCtaCtes", new[] { "ClienteId" });
            DropIndex("dbo.CuentaCorrientes", new[] { "ClienteId" });
            DropIndex("dbo.CuentaCorrientes", new[] { "UsuarioId" });
            DropIndex("dbo.DetalleCajas", new[] { "CajaId" });
            DropIndex("dbo.Cajas", new[] { "UsuarioCierreId" });
            DropIndex("dbo.Cajas", new[] { "UsuarioAperturaId" });
            DropIndex("dbo.Movimientos", new[] { "UsuarioId" });
            DropIndex("dbo.Movimientos", new[] { "ComprobanteId" });
            DropIndex("dbo.Movimientos", new[] { "CajaId" });
            DropIndex("dbo.DetalleComprobantes", new[] { "ComprobanteId" });
            DropIndex("dbo.Comprobantes", new[] { "ClienteId" });
            DropIndex("dbo.Comprobantes", new[] { "UsuarioId" });
            DropIndex("dbo.FormaPago_Cheque", new[] { "ComprobanteId" });
            DropIndex("dbo.FormaPago_Cheque", new[] { "ChequeId" });
            DropIndex("dbo.DepositoCheques", new[] { "CuentaBancariaId" });
            DropIndex("dbo.DepositoCheques", new[] { "ChequeId" });
            DropIndex("dbo.CuentaBancarias", new[] { "BancoId" });
            DropIndex("dbo.Cheques", new[] { "BancoId" });
            DropIndex("dbo.Cheques", new[] { "ClienteId" });
            DropTable("dbo.Comprobantes_Presupuesto");
            DropTable("dbo.Comprobantes_NotaCredito");
            DropTable("dbo.Comprobantes_Compra");
            DropTable("dbo.FormaPagoCtaCtes");
            DropTable("dbo.CuentaCorrientes");
            DropTable("dbo.DetalleCajas");
            DropTable("dbo.Cajas");
            DropTable("dbo.Movimientos");
            DropTable("dbo.DetalleComprobantes");
            DropTable("dbo.Comprobantes");
            DropTable("dbo.FormaPago_Cheque");
            DropTable("dbo.DepositoCheques");
            DropTable("dbo.CuentaBancarias");
            DropTable("dbo.Bancos");
            DropTable("dbo.Cheques");
        }
    }
}
