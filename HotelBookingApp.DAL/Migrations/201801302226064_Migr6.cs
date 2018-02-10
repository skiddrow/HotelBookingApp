namespace HotelBookingApp.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migr6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bills", "BookingDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bills", "BookingDate");
        }
    }
}
