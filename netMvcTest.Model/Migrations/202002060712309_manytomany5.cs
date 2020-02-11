namespace netMvcTest.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class manytomany5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BlogCategoryArticles", "BlogCategory_Id", "dbo.BlogCategories");
            DropForeignKey("dbo.BlogCategoryArticles", "Article_Id", "dbo.Articles");
            DropIndex("dbo.BlogCategoryArticles", new[] { "BlogCategory_Id" });
            DropIndex("dbo.BlogCategoryArticles", new[] { "Article_Id" });
            DropTable("dbo.BlogCategoryArticles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BlogCategoryArticles",
                c => new
                    {
                        BlogCategory_Id = c.Guid(nullable: false),
                        Article_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.BlogCategory_Id, t.Article_Id });
            
            CreateIndex("dbo.BlogCategoryArticles", "Article_Id");
            CreateIndex("dbo.BlogCategoryArticles", "BlogCategory_Id");
            AddForeignKey("dbo.BlogCategoryArticles", "Article_Id", "dbo.Articles", "Id");
            AddForeignKey("dbo.BlogCategoryArticles", "BlogCategory_Id", "dbo.BlogCategories", "Id");
        }
    }
}
