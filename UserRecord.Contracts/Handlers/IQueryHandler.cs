using UserRecord.Entity.Models;

namespace UserRecord.Contracts.Handlers;

public interface IQueryHandler : IHandler
{
    public IEnumerable<User> GetAll();
    public User GetById(int Id);
}