using System.Data.Entity;
using Leaf.DataAccess.Models;

namespace Leaf.DataAccess.Contexts
{
    public class ConferenceContext : DbContext
    {
        public ConferenceContext()
            : base("DataConnection")
        {
        }

        public DbSet<Conference> Conferences { get; set; }

        public void Clean()
        {
            Conferences.RemoveRange(Conferences);
            Save();
        }

        public bool Add<T>(T entity) where T : class
        {

            Entry(entity).State = EntityState.Added;
            return Save();
        }

        public bool Update<T>(T entity) where T : class
        {
            Entry(entity).State = EntityState.Modified;
            return Save();
        }

        public bool Delete<T>(T entity) where T : class
        {
            Entry(entity).State = EntityState.Deleted;
            return Save();
        }

        public bool Save()
        {
            return SaveChanges() > 0;
        }
    }
}
