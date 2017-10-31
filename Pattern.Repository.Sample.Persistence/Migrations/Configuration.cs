namespace Pattern.Repository.Sample.Persistence.Migrations
{
    using Pattern.Repository.Sample.Domain.Model;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Pattern.Repository.Sample.Persistence.PlutoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Pattern.Repository.Sample.Persistence.PlutoContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            IList<Author> testAuthors = new List<Author>();
            
            testAuthors.Add(new Author() { Id = 1, Name = "John Steinbeck" });
            testAuthors.Add(new Author() { Id = 2, Name = "John Grisham" });
            testAuthors.Add(new Author() { Id = 3, Name = "John Doe" });
            testAuthors.Add(new Author() { Id = 4, Name = "John Paper" });
            testAuthors.Add(new Author() { Id = 5, Name = "John Tillack" });
            
            foreach (Author a in testAuthors)
                context.Authors.Add(a);

            IList<Course> testCourse = new List<Course>();

            testCourse.Add(new Course() { Id = 1,
                Name = "First Book",
                Description = "The First Book",
                Level = 1,
                FullPrice = 20.00f,
                AuthorId = 1
            });


            foreach (Course c in testCourse)
                context.Courses.Add(c);

            base.Seed(context);
        }
    }
}
