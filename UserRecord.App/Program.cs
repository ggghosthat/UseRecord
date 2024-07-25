using UserRecord.App.Parser;
using UserRecord.App.Map;
using UserRecord.Entity.Models;
using UserRecord.Entity.Dto;
using UserRecord.Core.Handlers;
using System.IO;

namespace UserRecord.App;

internal class Program
{
    private static UserHandler _userhandler;

    private static string _file;
    private static string _command;
    private static string[] _values;

    public static async Task Main(string[] args)
    {
        SpellArgs(args);
        CheckFile();
        RouteCommand();
    }
    
    private static void SpellArgs(string[] arguments)
    {
        try
        {
            if (arguments[0] == "help")
                PrintHelpMessage();

            _file = arguments[0];
            _command = arguments[1];
            _values = arguments[2..];

        }
        catch (System.Exception)
        {
            PrintHelpMessage();
        }
    }

    private static void CheckFile()
    {
        if (!File.Exists(_file))
            throw new Exception($"Could not find file -> {_file}");

        if (Path.GetExtension(_file) != ".json")
            throw new Exception($"The input file is not a JSON file");

        _userhandler = new(_file);
    }

    private static void RouteCommand()
    {
        var parameters = ArgumentParser.GetParameters(_command, _values);

        switch (_command)
        {
            case "-add":
                var addDto = parameters.MapAddUserDto();
                _userhandler.Add(addDto);
                break;
            case "-update":
                var updateDto = parameters.MapUpdateUserDto();
                _userhandler.Update(updateDto);
                break;
            case "-delete":
                int deleteId = ParseId(parameters[0]);
                _userhandler.Delete(deleteId);
                break;
            case "-get":
                int getId = ParseId(parameters[0]);
                var user = _userhandler.GetById(getId);               
                Console.WriteLine(user);
                break;
            case "-getall":
                var users = _userhandler.GetAll();
                foreach (var userItem in users)
                    Console.WriteLine(userItem);
                break;
            default:
                throw new Exception($"Not supported command -> {_command}");

        }
    }

    private static int ParseId (string idParameter)
    {
        if (int.TryParse(idParameter, out var id))
            return id;
        else
            throw new Exception($"Could not parse input id -> {idParameter}");
    }

    private static void PrintHelpMessage()
    {
        string helpMessage = 
        """
        UserRecord is a console utility to keep record user data.

        Arguments:
          <FILE> - path to json file to store records

        Options:
            '-add FirstName:<FIRSTNAME> LastName:<LASTNAME> SalaryPerHour:<SALARY_PER_HOUR>' - add new user record
            '-update Id:<ID> <PROPERTY_NAME>:<PROPERTY_VALUE>' - update user record with specified Id
            '-delete Id:<ID>' - delete user with specified Id
            '-get Id:<ID>' - print user record with specified Id
            '-getall' - print all user records
        """;

        Console.WriteLine(helpMessage);
    }
}

