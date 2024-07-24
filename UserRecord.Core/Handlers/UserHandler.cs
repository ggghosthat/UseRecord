using System.Collections.Generic;
using System.Linq;
using UserRecord.Entity.Dto;
using UserRecord.Entity.Models;
using UserRecord.Contracts.Handlers;
using UserRecord.Core.Json;

namespace UserRecord.Core.Handlers;

public class UserHandler : ICommandHandler, IQueryHandler
{
    private static string _file = null;

    private static List<User> _users = [];

    public UserHandler(string file)
    {
        _file = file;
        _users = _users.DeserializeUsers(_file);
    }
    
    public List<User> Users => _users;

    public int UsersCount => _users.Count;

    public void Add(UserDto userDto)
    {
        var user = new User
        {
            Id = UsersCount + 1,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            SalaryPerHour = userDto.SalaryPerHour
        };

        _users.Add(user);
        _users.SerializeUsers(_file);
    }
    
    public void Update(UserDto newUserData)
    {
        if (_users == null)
            return;

        if (!_users.Exists(u => u.Id == newUserData.Id))
            throw new Exception($"User record with  <{newUserData.Id}> does not exists");

        var user = _users.First(u => u.Id == newUserData.Id);

        if (!String.IsNullOrEmpty(newUserData.FirstName))
            user.FirstName = newUserData.FirstName;

        if (!String.IsNullOrEmpty(newUserData.LastName))
            user.LastName = newUserData.LastName;
        
        if (newUserData.SalaryPerHour != default)
            user.SalaryPerHour = newUserData.SalaryPerHour;

        _users.SerializeUsers(_file);
    }

    public void Delete(int id)
    {
        if (_users == null)
            return;
        
        if (!_users.Exists(u => u.Id == id))
            throw new Exception($"User record with  <{id}> does not exists");

        var user = _users.First(u => u.Id == id);

        _users.Remove(user);
        _users.SerializeUsers(_file);
    }

    public IEnumerable<User> GetAll()
    {
        return _users.DeserializeUsers(_file);
    }

    public User GetById(int id)
    {
       _users = _users.DeserializeUsers(_file);

        if (!_users.Exists(u => u.Id == id))
            throw new Exception($"User record with  <{id}> does not exists");
        
        return _users.First(u => u.Id == id);
    }

    public void Dispose()
    {
        _users = null;
    }
}