namespace netMvcTest.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class manytomany4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogCategoryArticles",
                c => new
                    {
                        BlogCategory_Id = c.Guid(nullable: false),
                        Article_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.BlogCategory_Id, t.Article_Id })
                .ForeignKey("dbo.BlogCategories", t => t.BlogCategory_Id)
                .ForeignKey("dbo.Articles", t => t.Article_Id)
                .Index(t => t.BlogCategory_Id)
                .Index(t => t.Article_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BlogCategoryArticles", "Article_Id", "dbo.Articles");
            DropForeignKey("dbo.BlogCategoryArticles", "BlogCategory_Id", "dbo.BlogCategories");
            DropIndex("dbo.BlogCategoryArticles", new[] { "Article_Id" });
            DropIndex("dbo.BlogCategoryArticles", new[] { "BlogCategory_Id" });
            DropTable("dbo.BlogCategoryArticles");
        }
    }
}
