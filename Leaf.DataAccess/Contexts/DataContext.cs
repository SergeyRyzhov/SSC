using Leaf.DataAccess.Model;
using System.Data.Entity;

namespace Leaf.DataAccess.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("DataConnection")
        {
        }

        public DbSet<Conference> Conferences { get; set; }

        public DbSet<Report> Reports { get; set; }

        public DbSet<File> Files { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<ReportRequest> ReportRequests { get; set; }

        public DbSet<ConferenceRequest> ConferenceRequests { get; set; }

        public void Clean()
        {
            foreach (var conference in Conferences)
            {
                this.Delete(conference);
            }
        }

        public bool Add<T>(T entity)
        where T : class
        {
            this.Entry(entity).State = EntityState.Added;
            return this.Save();
        }

        public bool Update<T>(T entity)
        where T : class
        {
            this.Entry(entity).State = EntityState.Modified;
            return this.Save();
        }

        public bool Delete<T>(T entity)
        where T : class
        {
            this.Entry(entity).State = EntityState.Deleted;
            return this.Save();
        }

        public bool Save()
        {
            return this.SaveChanges() > 0;
        }
    }
}