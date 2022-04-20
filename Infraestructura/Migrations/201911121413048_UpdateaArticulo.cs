namespace Infraestructura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateaArticulo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articulos", "PrecioCosto", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articulos", "PrecioCosto");
        }
    }
}
