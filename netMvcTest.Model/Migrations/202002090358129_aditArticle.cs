namespace netMvcTest.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aditArticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "LikeCount", c => c.Int(nullable: false));
            AddColumn("dbo.Articles", "OpposeCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "OpposeCount");
            DropColumn("dbo.Articles", "LikeCount");
        }
    }
}
