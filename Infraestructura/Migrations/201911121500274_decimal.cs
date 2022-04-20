namespace Infraestructura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _decimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Articulos", "PrecioCosto", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Articulos", "PrecioCosto", c => c.Int(nullable: false));
        }
    }
}
