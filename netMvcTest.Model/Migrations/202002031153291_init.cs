namespace netMvcTest.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        CommentCount = c.Int(nullable: false),
                        UserId = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Email = c.String(nullable: false, maxLength: 40),
                        Password = c.String(nullable: false, maxLength: 30),
                        ImagePath = c.String(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ArticleToCategories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BlogCategoryId = c.Guid(nullable: false),
                        ArticleId = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId)
                .ForeignKey("dbo.BlogCategories", t => t.BlogCategoryId)
                .Index(t => t.BlogCategoryId)
                .Index(t => t.ArticleId);
            
            CreateTable(
                "dbo.BlogCategories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        UserId = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        ArticleId = c.Guid(nullable: false),
                        Content = c.String(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ArticleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.ArticleToCategories", "BlogCategoryId", "dbo.BlogCategories");
            DropForeignKey("dbo.BlogCategories", "UserId", "dbo.Users");
            DropForeignKey("dbo.ArticleToCategories", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.Articles", "UserId", "dbo.Users");
            DropIndex("dbo.Comments", new[] { "ArticleId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.BlogCategories", new[] { "UserId" });
            DropIndex("dbo.ArticleToCategories", new[] { "ArticleId" });
            DropIndex("dbo.ArticleToCategories", new[] { "BlogCategoryId" });
            DropIndex("dbo.Articles", new[] { "UserId" });
            DropTable("dbo.Comments");
            DropTable("dbo.BlogCategories");
            DropTable("dbo.ArticleToCategories");
            DropTable("dbo.Users");
            DropTable("dbo.Articles");
        }
    }
}
