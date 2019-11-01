namespace RoverController.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MissionAttachments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MissionId = c.Int(nullable: false),
                        OriginalFilename = c.String(nullable: false),
                        FileType = c.String(nullable: false),
                        FileSize = c.Int(nullable: false),
                        FileName = c.String(nullable: false),
                        AWSPublicUrl = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedByUserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedByUserId)
                .ForeignKey("dbo.Missions", t => t.MissionId)
                .Index(t => t.MissionId)
                .Index(t => t.CreatedByUserId);
            
            AlterColumn("dbo.PinPoints", "Direction", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MissionAttachments", "MissionId", "dbo.Missions");
            DropForeignKey("dbo.MissionAttachments", "CreatedByUserId", "dbo.AspNetUsers");
            DropIndex("dbo.MissionAttachments", new[] { "CreatedByUserId" });
            DropIndex("dbo.MissionAttachments", new[] { "MissionId" });
            AlterColumn("dbo.PinPoints", "Direction", c => c.String());
            DropTable("dbo.MissionAttachments");
        }
    }
}
