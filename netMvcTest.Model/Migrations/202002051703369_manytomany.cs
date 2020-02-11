namespace netMvcTest.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class manytomany : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "ImagePath", c => c.String(nullable: false));
        }
    }
}
