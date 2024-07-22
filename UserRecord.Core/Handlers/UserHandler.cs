using System.Collections.Generic;
using System.Linq;
using UserRecord.Entity.Dto;
using UserRecord.Entity.Models;
using UserRecord.Contracts.Handlers;
using UserRecord.Core.Json;

namespace UserRecord.Core.Handlers;

internal class UserHandler : ICommandHandler, IQueryHandler
{
    private static string _file = null;

    private static IList<User> _users = null;

    public UserHandler(string file)
    {
        _file = file;
    }

    public void Create()
    {
        using var jsonManager = new JsonManager();
        _users = jsonManager.ReadJson(_file);
    }
    
    public void Add(UserDto userDto)
    {
        if (_users == null)
            return;

        int lastId = _users.Last().Id;

        var user = new User
        {
            Id = lastId++,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            SalaryPerHour = userDto.SalaryPerHour
        };

        _users.Add(user);
    }
    
    public void Update(UserDto userDto)
    {        
        if (_users == null)
            return;

        _users.Where(u => u.Id == userDto.Id)?
            .Select(u => 
            {
                u.FirstName = userDto.FirstName;
                u.LastName = userDto.LastName;
                u.SalaryPerHour = userDto.SalaryPerHour;
                return u;
            })
            .FirstOrDefault();
    }

    public void Delete(int Id)
    {
        if (_users == null)
            return;

        _users.FirstOrDefault(u => u.Id == Id);
    }

    public IEnumerable<User> GetAll() =>
        _users;

    public User GetById(int Id) => 
        _users.FirstOrDefault(u => u.Id == Id);

    public void Dispose()
    {
        _users = null;
    }
}