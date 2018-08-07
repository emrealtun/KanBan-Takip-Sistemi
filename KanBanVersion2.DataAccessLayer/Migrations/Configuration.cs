namespace KanBanVersion2.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<KanBanVersion2.DataAccessLayer.EntityFramework.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "KanBanVersion2.DataAccessLayer.EntityFramework.DatabaseContext";
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(KanBanVersion2.DataAccessLayer.EntityFramework.DatabaseContext context)
        {
           
        }
    }
}
