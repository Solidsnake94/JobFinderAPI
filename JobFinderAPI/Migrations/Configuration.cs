namespace JobFinderAPI.Migrations
{
    using Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<JobFinderAPI.AuthContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "JobFinderAPI.AuthContext";
        }

        protected override void Seed(JobFinderAPI.AuthContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //


            //// adding some dummy data to the db
            //    context.Jobs.AddOrUpdate( 
            //        j => j.Id, 
            //        new Job {Title = "Cleaning Huge At Norrebro",
            //                 Price = 50}
            //)
        }
    }
}
