namespace TransportationManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shipmentModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Shipments",
                c => new
                    {
                        ShipmentId = c.Int(nullable: false, identity: true),
                        ShipTo_EmailAddress = c.String(),
                        ShipTo_Company = c.String(),
                        ShipTo_Attention = c.String(),
                        ShipTo_AddressLine1 = c.String(),
                        ShipTo_AddressLine2 = c.String(),
                        ShipTo_AddressLine3 = c.String(),
                        ShipTo_City = c.String(),
                        ShipTo_State = c.String(),
                        ShipTo_PostalCode = c.String(),
                        ShipTo_Country = c.String(),
                        ShipFrom_EmailAddress = c.String(),
                        ShipFrom_Company = c.String(),
                        ShipFrom_Attention = c.String(),
                        ShipFrom_AddressLine1 = c.String(),
                        ShipFrom_AddressLine2 = c.String(),
                        ShipFrom_AddressLine3 = c.String(),
                        ShipFrom_City = c.String(),
                        ShipFrom_State = c.String(),
                        ShipFrom_PostalCode = c.String(),
                        ShipFrom_Country = c.String(),
                        Weight = c.Double(nullable: false),
                        Depth = c.Int(nullable: false),
                        Width = c.Int(nullable: false),
                        Height = c.Int(nullable: false),
                        TrackingId = c.Guid(nullable: false),
                        ReferenceNumber1 = c.String(),
                        ReferenceNumber2 = c.String(),
                        InvoiceAmount = c.String(),
                        ShipmentDate = c.DateTime(nullable: false),
                        Company_CompanyId = c.Int(),
                    })
                .PrimaryKey(t => t.ShipmentId)
                .ForeignKey("dbo.Companies", t => t.Company_CompanyId)
                .Index(t => t.Company_CompanyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shipments", "Company_CompanyId", "dbo.Companies");
            DropIndex("dbo.Shipments", new[] { "Company_CompanyId" });
            DropTable("dbo.Shipments");
        }
    }
}
