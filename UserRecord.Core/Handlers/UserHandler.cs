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
    
    public void Update(UserDto userDto)
    {
        if (_users == null)
            return;

        _users.Where(u => u.Id == userDto.Id)?
            .Select(u => UpdateUserData(ref u, userDto))
            .FirstOrDefault();
        
        _users.SerializeUsers(_file);
    }

    private User UpdateUserData(ref User user, UserDto newUserData)
    {
        if (!String.IsNullOrEmpty(newUserData.FirstName))
            user.FirstName = newUserData.FirstName;
        if (!String.IsNullOrEmpty(newUserData.LastName))
            user.LastName = newUserData.LastName;
        
        user.SalaryPerHour = newUserData.SalaryPerHour;

        return user;
    }

    public void Delete(int id)
    {
        if (_users == null)
            return;
        
        int index = id - 1;
        _users.RemoveAt(id - 1);
        _users.SerializeUsers(_file);
    }

    public IEnumerable<User> GetAll()
    {
        return _users.DeserializeUsers(_file);
    }

    public User GetById(int Id)
    {
       _users = _users.DeserializeUsers(_file);

        return _users.FirstOrDefault(u => u.Id == Id);
    }

    public void Dispose()
    {
        _users = null;
    }
}