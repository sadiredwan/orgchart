namespace orgchart.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<orgchart.DBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(orgchart.DBContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.companies.AddOrUpdate(x => x.id,
                new company()
                {
                    id = 1,
                    name = "Excel",
                    created_at = DateTime.Now
                   
                }
                );
        }
    }
}
