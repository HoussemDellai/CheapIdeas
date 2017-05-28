using System.Data.Entity;
using CheapIdeas.Web.Models.Entities;

namespace CheapIdeas.Web.Models
{
    public class IdeasContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public IdeasContext() : base("name=IdeasContext")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<IdeasContext>());
        }

        public DbSet<Idea> Ideas { get; set; }
    }
}
