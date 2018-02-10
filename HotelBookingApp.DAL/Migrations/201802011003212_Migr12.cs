namespace HotelBookingApp.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migr12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "IsCanceled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bookings", "IsCanceled");
        }
    }
}
