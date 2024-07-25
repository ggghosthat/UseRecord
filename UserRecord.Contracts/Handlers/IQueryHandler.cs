using UserRecord.Entity.Models;

namespace UserRecord.Contracts.Handlers;

public interface IQueryHandler
{
    public IEnumerable<User> GetAll();
    public User GetById(int Id);
}