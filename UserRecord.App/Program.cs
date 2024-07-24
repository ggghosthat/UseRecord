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

    public static async Task Main(string[] args)
    {
        string file = args[0];
        string command = args[1];
        string[] values = args[2..];

        CheckFile(file);
        RouteCommand(command, values);
    }
    
    private static void CheckFile(string filePath)
    {
        if (!File.Exists(filePath))
            throw new Exception($"Could not find file -> {filePath}");

        if (Path.GetExtension(filePath) != ".json")
            throw new Exception($"The input file is not a JSON file");

        _userhandler = new(filePath);
    }

    private static void RouteCommand(string command, string[] values)
    {
        var parameters = ArgumentParser.GetParameters(command, values);

        switch (command)
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
                throw new Exception($"Not supported command -> {command}");

        }
    }

    private static int ParseId (string idParameter)
    {
        if (int.TryParse(idParameter, out var id))
            return id;
        else
            throw new Exception($"Could not parse input id -> {idParameter}");
    }
}

