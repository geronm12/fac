namespace Infraestructura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lacoscuarmani : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articulos", "TipoArticulo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articulos", "TipoArticulo");
        }
    }
}
