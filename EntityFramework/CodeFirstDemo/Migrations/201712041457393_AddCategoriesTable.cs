namespace CodeFirstDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoriesTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "Author_Id", "dbo.Authors");
            DropIndex("dbo.Courses", new[] { "Author_Id" });
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Courses", "Author_Id1", c => c.Int());
            AlterColumn("dbo.Courses", "Author_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Courses", "Author_Id1");
            AddForeignKey("dbo.Courses", "Author_Id1", "dbo.Authors", "Id");

            Sql("INSERT INTO Categories VALUES (1, 'Web Development')");
            Sql("INSERT INTO Categories VALUES (2, 'Programming Languages')");
        }

        public override void Down()
        {
            DropForeignKey("dbo.Courses", "Author_Id1", "dbo.Authors");
            DropIndex("dbo.Courses", new[] { "Author_Id1" });
            AlterColumn("dbo.Courses", "Author_Id", c => c.Int());
            DropColumn("dbo.Courses", "Author_Id1");
            DropTable("dbo.Categories");
            CreateIndex("dbo.Courses", "Author_Id");
            AddForeignKey("dbo.Courses", "Author_Id", "dbo.Authors", "Id");
        }
    }
}
