namespace HotelBookingApp.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migr2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Requests", "IsConfirmed", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Requests", "IsConfirmed", c => c.Boolean(nullable: false));
        }
    }
}
