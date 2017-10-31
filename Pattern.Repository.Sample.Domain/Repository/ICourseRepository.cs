using Pattern.Repository.Sample.Domain.Model;
using System.Collections.Generic;

namespace Pattern.Repository.Sample.Domain.Repository
{
    public interface ICourseRepository : IRepository<Course>
    {
        IEnumerable<Course> GetTopSellingCourses(int count);
        IEnumerable<Course> GetCoursesWithAuthors(int pageIndex, int pageSize);
    }
}