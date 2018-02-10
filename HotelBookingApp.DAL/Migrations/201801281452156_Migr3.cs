namespace HotelBookingApp.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migr3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requests", "IsCanceled", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Requests", "IsConfirmed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Requests", "IsConfirmed", c => c.Boolean());
            DropColumn("dbo.Requests", "IsCanceled");
        }
    }
}
