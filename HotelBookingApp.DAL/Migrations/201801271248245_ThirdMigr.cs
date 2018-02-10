namespace HotelBookingApp.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirdMigr : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Requests", "ApartmentId", "dbo.Apartments");
            DropIndex("dbo.Requests", new[] { "ApartmentId" });
            AlterColumn("dbo.Requests", "ApartmentId", c => c.Int());
            CreateIndex("dbo.Requests", "ApartmentId");
            AddForeignKey("dbo.Requests", "ApartmentId", "dbo.Apartments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "ApartmentId", "dbo.Apartments");
            DropIndex("dbo.Requests", new[] { "ApartmentId" });
            AlterColumn("dbo.Requests", "ApartmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Requests", "ApartmentId");
            AddForeignKey("dbo.Requests", "ApartmentId", "dbo.Apartments", "Id", cascadeDelete: true);
        }
    }
}
