using Pattern.Repository.Sample.Domain.Model;
using System.Data.Entity;

namespace Pattern.Repository.Sample.Persistence
{

    public class PlutoContext : DbContext
    {

        // Your context has been configured to use a 'Pluto' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Pattern.Repository.Sample.Persistence.Pluto' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Pluto' 
        // connection string in the application configuration file.
        public PlutoContext()
            : base("name=PlutoContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CourseConfiguration());
        }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}