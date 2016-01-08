namespace TransportationManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shipment_InvoiceAmount_toDouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Shipments", "InvoiceAmount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Shipments", "InvoiceAmount", c => c.String());
        }
    }
}
