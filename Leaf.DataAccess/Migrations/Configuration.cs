using Leaf.DataAccess.Contexts;
using Leaf.DataAccess.Models;

namespace Leaf.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ConferenceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ConferenceContext context)
        {
            context.Clean();
            context.Conferences.AddOrUpdate(new Conference
            {
                Id = Guid.NewGuid(),
                Code = "conf-1",
                Name = "First Conference",
                Description = "Desc of first conference.",
                ShortDescription = "Best conference.",
                StartDate = new DateTime(2014,3,12),
                EndDate = new DateTime(2014,3,14),
                IsPublished = true
            });

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
        }
    }
}
