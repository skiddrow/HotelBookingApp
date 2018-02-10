namespace HotelBookingApp.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMigr : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Requests", "ManagerId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Requests", "ManagerId", c => c.Int(nullable: false));
        }
    }
}
