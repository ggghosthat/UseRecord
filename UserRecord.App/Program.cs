using UserRecord.Entity.Models;
using UserRecord.Entity.Dto;
using UserRecord.Core.Handlers;

namespace UserRecord.App;

internal class Program
{
    public static async Task Main(string[] args)
    {
        
        var uh = new UserHandler("./users.json");
        uh.Delete(3);
    }
}

internal class ArgumentParser
{
    private static string[] commands = ["add", "update", "delete", "get", "getall"];
    private static string[] properties = ["Firstname", "LastName", "SalaryPerHour"];

    public static void ParseCommad(string inputCommand)
    {

    }
}