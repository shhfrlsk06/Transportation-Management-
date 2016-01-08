namespace TransportationManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressID = c.Int(nullable: false, identity: true),
                        AddressLine1 = c.String(),
                        AddressLine2 = c.String(),
                        AddressLine3 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        PostalCode = c.String(),
                        Country = c.String(),
                        Company_CompanyId = c.Int(),
                    })
                .PrimaryKey(t => t.AddressID)
                .ForeignKey("dbo.Companies", t => t.Company_CompanyId)
                .Index(t => t.Company_CompanyId);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        ClientNumber = c.String(nullable: false),
                        Industry = c.String(),
                        CompanyName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CompanyId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ContactID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(),
                        MiddleName = c.String(),
                        Title = c.String(),
                        PhoneNumber = c.String(maxLength: 25),
                        Company_CompanyId = c.Int(),
                    })
                .PrimaryKey(t => t.ContactID)
                .ForeignKey("dbo.Companies", t => t.Company_CompanyId)
                .Index(t => t.Company_CompanyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contacts", "Company_CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Addresses", "Company_CompanyId", "dbo.Companies");
            DropIndex("dbo.Contacts", new[] { "Company_CompanyId" });
            DropIndex("dbo.Addresses", new[] { "Company_CompanyId" });
            DropTable("dbo.Contacts");
            DropTable("dbo.Companies");
            DropTable("dbo.Addresses");
        }
    }
}
