using Pattern.Repository.Sample.Domain.Model;

namespace Pattern.Repository.Sample.Domain.Repository
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Author GetAuthorWithCourses(int id);
    }
}