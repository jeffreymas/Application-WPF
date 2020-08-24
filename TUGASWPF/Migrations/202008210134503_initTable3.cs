namespace TUGASWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initTable3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_m_supplier", "Newpass", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_m_supplier", "Newpass");
        }
    }
}
