namespace CalendarForXML.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCalendarDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Calendars",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        date = c.DateTime(nullable: false),
                        name = c.String(),
                        isHoliday = c.Boolean(nullable: false),
                        holidayCategory = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Calendars");
        }
    }
}
