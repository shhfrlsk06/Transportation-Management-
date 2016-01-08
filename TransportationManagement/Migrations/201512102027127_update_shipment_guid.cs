namespace TransportationManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_shipment_guid : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Shipments", "TrackingId", c => c.Guid(nullable: false));
            DropColumn("dbo.Shipments", "ShipFrom_Attention");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Shipments", "ShipFrom_Attention", c => c.String());
            AlterColumn("dbo.Shipments", "TrackingId", c => c.Guid(nullable: false));
        }
    }
}
