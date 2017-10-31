using Pattern.Repository.Sample.Domain.Repository;
using System;

namespace Pattern.Repository.Sample.Domain.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ICourseRepository Courses { get; }
        IAuthorRepository Authors { get; }
        int Complete();
    }
}
