using UserRecord.Entity.Dto;
using UserRecord.Entity.Models;

namespace UserRecord.Contracts.Handlers;

public interface ICommandHandler
{
    public void Add(UserDto user);
    public void Update(UserDto UserDto);
    public void Delete(int Id);
}