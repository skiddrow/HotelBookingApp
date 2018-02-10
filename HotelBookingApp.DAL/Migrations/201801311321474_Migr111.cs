namespace HotelBookingApp.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migr111 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bookings", "ApartmentId", "dbo.Apartments");
            DropForeignKey("dbo.Bookings", "BillId", "dbo.Bills");
            DropIndex("dbo.Bookings", new[] { "ApartmentId" });
            DropIndex("dbo.Bookings", new[] { "BillId" });
            AlterColumn("dbo.Bookings", "ApartmentId", c => c.Int());
            AlterColumn("dbo.Bookings", "BillId", c => c.Int());
            CreateIndex("dbo.Bookings", "ApartmentId");
            CreateIndex("dbo.Bookings", "BillId");
            AddForeignKey("dbo.Bookings", "ApartmentId", "dbo.Apartments", "Id");
            AddForeignKey("dbo.Bookings", "BillId", "dbo.Bills", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "BillId", "dbo.Bills");
            DropForeignKey("dbo.Bookings", "ApartmentId", "dbo.Apartments");
            DropIndex("dbo.Bookings", new[] { "BillId" });
            DropIndex("dbo.Bookings", new[] { "ApartmentId" });
            AlterColumn("dbo.Bookings", "BillId", c => c.Int(nullable: false));
            AlterColumn("dbo.Bookings", "ApartmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Bookings", "BillId");
            CreateIndex("dbo.Bookings", "ApartmentId");
            AddForeignKey("dbo.Bookings", "BillId", "dbo.Bills", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Bookings", "ApartmentId", "dbo.Apartments", "Id", cascadeDelete: true);
        }
    }
}
