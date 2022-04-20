namespace Infraestructura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateUltimoMomento : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articulos", "CodigoDeProveedor", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articulos", "CodigoDeProveedor");
        }
    }
}
