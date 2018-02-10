namespace HotelBookingApp.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migr9 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bills", "BookingDate", c => c.DateTime());
            AlterColumn("dbo.Bills", "DateOfPayment", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Bills", "DateOfPayment", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Bills", "BookingDate", c => c.DateTime(nullable: false));
        }
    }
}
