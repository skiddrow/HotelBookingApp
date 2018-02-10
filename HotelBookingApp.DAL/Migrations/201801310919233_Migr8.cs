namespace HotelBookingApp.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migr8 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bookings", "BookingDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Bookings", "BookingDate", c => c.DateTime(nullable: false));
        }
    }
}
