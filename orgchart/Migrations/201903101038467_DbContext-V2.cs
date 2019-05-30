namespace orgchart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbContextV2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        rl_id = c.Int(nullable: false),
                        user_id = c.Int(nullable: false),
                        created_at = c.DateTime(),
                        created_by = c.Int(nullable: false),
                        modified_at = c.DateTime(),
                        modified_by = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.rl_id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.user_id, cascadeDelete: true)
                .Index(t => t.rl_id)
                .Index(t => t.user_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "user_id", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "rl_id", "dbo.Roles");
            DropIndex("dbo.UserRoles", new[] { "user_id" });
            DropIndex("dbo.UserRoles", new[] { "rl_id" });
            DropTable("dbo.UserRoles");
        }
    }
}
