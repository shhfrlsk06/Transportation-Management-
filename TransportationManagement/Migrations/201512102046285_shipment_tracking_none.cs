namespace TransportationManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shipment_tracking_none : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Shipments", "TrackingId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Shipments", "TrackingId", c => c.Guid(nullable: false, identity: true));
        }
    }
}
