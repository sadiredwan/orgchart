namespace orgchart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbContextV1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.companaies",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        user_id = c.Int(nullable: false),
                        name = c.String(),
                        created_at = c.DateTime(),
                        modified_at = c.DateTime(),
                        created_by = c.Int(nullable: false),
                        modified_by = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.designations",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        short_name = c.String(nullable: false),
                        remarks = c.String(),
                        parent_id = c.Int(nullable: false),
                        created_at = c.DateTime(),
                        modified_at = c.DateTime(),
                        created_by = c.Int(nullable: false),
                        modified_by = c.Int(nullable: false),
                        company_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.employees",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        supervisor_id = c.Int(nullable: false),
                        test = c.String(),
                        national_idcard = c.String(),
                        first_name = c.String(nullable: false),
                        last_name = c.String(nullable: false),
                        email = c.String(nullable: false),
                        contact_no = c.String(),
                        contact_address = c.String(),
                        photo = c.String(),
                        designation_id = c.Int(nullable: false),
                        company_id = c.Int(nullable: false),
                        joining_date = c.DateTime(nullable: false),
                        created_at = c.DateTime(),
                        modified_at = c.DateTime(),
                        created_by = c.Int(nullable: false),
                        modified_by = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.companaies", t => t.company_id, cascadeDelete: true)
                .ForeignKey("dbo.designations", t => t.designation_id, cascadeDelete: true)
                .Index(t => t.designation_id)
                .Index(t => t.company_id);
            
            CreateTable(
                "dbo.Modules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        parent_id = c.Int(nullable: false),
                        path_url = c.String(),
                        ControllerName = c.String(),
                        Action = c.String(),
                        created_at = c.DateTime(),
                        created_by = c.Int(nullable: false),
                        modified_at = c.DateTime(),
                        modified_by = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RolePermissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        role_id = c.Int(nullable: false),
                        module_id = c.Int(nullable: false),
                        Is_View = c.Boolean(nullable: false),
                        Is_Create = c.Boolean(nullable: false),
                        Is_Delete = c.Boolean(nullable: false),
                        Is_Update = c.Boolean(nullable: false),
                        created_at = c.DateTime(),
                        created_by = c.Int(nullable: false),
                        modified_at = c.DateTime(),
                        modified_by = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Modules", t => t.module_id, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.role_id, cascadeDelete: true)
                .Index(t => t.role_id)
                .Index(t => t.module_id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        created_at = c.DateTime(),
                        created_by = c.Int(nullable: false),
                        modified_at = c.DateTime(),
                        modified_by = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        email = c.String(nullable: false),
                        password = c.String(nullable: false),
                        first_name = c.String(),
                        last_name = c.String(),
                        created_at = c.DateTime(),
                        modified_at = c.DateTime(),
                        created_by = c.Int(nullable: false),
                        modified_by = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RolePermissions", "role_id", "dbo.Roles");
            DropForeignKey("dbo.RolePermissions", "module_id", "dbo.Modules");
            DropForeignKey("dbo.employees", "designation_id", "dbo.designations");
            DropForeignKey("dbo.employees", "company_id", "dbo.companaies");
            DropIndex("dbo.RolePermissions", new[] { "module_id" });
            DropIndex("dbo.RolePermissions", new[] { "role_id" });
            DropIndex("dbo.employees", new[] { "company_id" });
            DropIndex("dbo.employees", new[] { "designation_id" });
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.RolePermissions");
            DropTable("dbo.Modules");
            DropTable("dbo.employees");
            DropTable("dbo.designations");
            DropTable("dbo.companaies");
        }
    }
}
