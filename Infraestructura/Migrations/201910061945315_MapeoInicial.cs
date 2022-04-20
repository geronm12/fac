namespace Infraestructura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MapeoInicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Persona",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LocalidadId = c.Long(nullable: false),
                        Telefono = c.String(maxLength: 25, unicode: false),
                        Celular = c.String(maxLength: 25, unicode: false),
                        Direccion = c.String(nullable: false, maxLength: 400, unicode: false),
                        Email = c.String(maxLength: 250, unicode: false),
                        EstaEliminado = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Apellido = c.String(maxLength: 250, unicode: false),
                        Nombre = c.String(maxLength: 250, unicode: false),
                        Dni = c.String(maxLength: 9, unicode: false),
                        Cuil = c.String(maxLength: 13, unicode: false),
                        FechaNacimiento = c.DateTime(),
                        Foto = c.Binary(),
                        RazonSocial = c.String(maxLength: 250, unicode: false),
                        CUIT = c.String(maxLength: 13, unicode: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Localidad", t => t.LocalidadId)
                .Index(t => t.LocalidadId);
            
            CreateTable(
                "dbo.CondicionIva",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 250, unicode: false),
                        EstaEliminado = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Localidad",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ProvinciaId = c.Long(nullable: false),
                        Descripcion = c.String(nullable: false, maxLength: 250, unicode: false),
                        EstaEliminado = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Provincia", t => t.ProvinciaId)
                .Index(t => t.ProvinciaId);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EmpleadoId = c.Long(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 50, unicode: false),
                        Password = c.String(nullable: false, maxLength: 400, unicode: false),
                        EstaBloqueado = c.Boolean(nullable: false),
                        EstaEliminado = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Persona_Empleado", t => t.EmpleadoId)
                .Index(t => t.EmpleadoId);
            
            CreateTable(
                "dbo.Perfiles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 250, unicode: false),
                        EstaEliminado = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Formularios",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Codigo = c.Int(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 250, unicode: false),
                        NombreCompleto = c.String(nullable: false, maxLength: 400, unicode: false),
                        EstaEliminado = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Provincia",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 250, unicode: false),
                        EstaEliminado = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FormularioPerfil",
                c => new
                    {
                        Formulario_Id = c.Long(nullable: false),
                        Perfil_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Formulario_Id, t.Perfil_Id })
                .ForeignKey("dbo.Formularios", t => t.Formulario_Id)
                .ForeignKey("dbo.Perfiles", t => t.Perfil_Id)
                .Index(t => t.Formulario_Id)
                .Index(t => t.Perfil_Id);
            
            CreateTable(
                "dbo.PerfilUsuario",
                c => new
                    {
                        Perfil_Id = c.Long(nullable: false),
                        Usuario_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Perfil_Id, t.Usuario_Id })
                .ForeignKey("dbo.Perfiles", t => t.Perfil_Id)
                .ForeignKey("dbo.Usuarios", t => t.Usuario_Id)
                .Index(t => t.Perfil_Id)
                .Index(t => t.Usuario_Id);
            
            CreateTable(
                "dbo.Persona_Cliente",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        CondicionIvaId = c.Long(nullable: false),
                        ActivarCtaCte = c.Boolean(nullable: false),
                        TieneLimiteCompra = c.Boolean(nullable: false),
                        Monto = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Persona", t => t.Id)
                .ForeignKey("dbo.CondicionIva", t => t.CondicionIvaId)
                .Index(t => t.Id)
                .Index(t => t.CondicionIvaId);
            
            CreateTable(
                "dbo.Persona_Empleado",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Legajo = c.Int(nullable: false),
                        FechaIngreso = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Persona", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Persona_Proveedor",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        CondicionIvaId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Persona", t => t.Id)
                .ForeignKey("dbo.CondicionIva", t => t.CondicionIvaId)
                .Index(t => t.Id)
                .Index(t => t.CondicionIvaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Persona_Proveedor", "CondicionIvaId", "dbo.CondicionIva");
            DropForeignKey("dbo.Persona_Proveedor", "Id", "dbo.Persona");
            DropForeignKey("dbo.Persona_Empleado", "Id", "dbo.Persona");
            DropForeignKey("dbo.Persona_Cliente", "CondicionIvaId", "dbo.CondicionIva");
            DropForeignKey("dbo.Persona_Cliente", "Id", "dbo.Persona");
            DropForeignKey("dbo.Localidad", "ProvinciaId", "dbo.Provincia");
            DropForeignKey("dbo.PerfilUsuario", "Usuario_Id", "dbo.Usuarios");
            DropForeignKey("dbo.PerfilUsuario", "Perfil_Id", "dbo.Perfiles");
            DropForeignKey("dbo.FormularioPerfil", "Perfil_Id", "dbo.Perfiles");
            DropForeignKey("dbo.FormularioPerfil", "Formulario_Id", "dbo.Formularios");
            DropForeignKey("dbo.Usuarios", "EmpleadoId", "dbo.Persona_Empleado");
            DropForeignKey("dbo.Persona", "LocalidadId", "dbo.Localidad");
            DropIndex("dbo.Persona_Proveedor", new[] { "CondicionIvaId" });
            DropIndex("dbo.Persona_Proveedor", new[] { "Id" });
            DropIndex("dbo.Persona_Empleado", new[] { "Id" });
            DropIndex("dbo.Persona_Cliente", new[] { "CondicionIvaId" });
            DropIndex("dbo.Persona_Cliente", new[] { "Id" });
            DropIndex("dbo.PerfilUsuario", new[] { "Usuario_Id" });
            DropIndex("dbo.PerfilUsuario", new[] { "Perfil_Id" });
            DropIndex("dbo.FormularioPerfil", new[] { "Perfil_Id" });
            DropIndex("dbo.FormularioPerfil", new[] { "Formulario_Id" });
            DropIndex("dbo.Usuarios", new[] { "EmpleadoId" });
            DropIndex("dbo.Localidad", new[] { "ProvinciaId" });
            DropIndex("dbo.Persona", new[] { "LocalidadId" });
            DropTable("dbo.Persona_Proveedor");
            DropTable("dbo.Persona_Empleado");
            DropTable("dbo.Persona_Cliente");
            DropTable("dbo.PerfilUsuario");
            DropTable("dbo.FormularioPerfil");
            DropTable("dbo.Provincia");
            DropTable("dbo.Formularios");
            DropTable("dbo.Perfiles");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Localidad");
            DropTable("dbo.CondicionIva");
            DropTable("dbo.Persona");
        }
    }
}
