using UserRecord.Entity.Models;
using UserRecord.Entity.Dto;
using UserRecord.Core.Handlers;

namespace UserRecord.App;

internal class Program
{
    public static async Task Main(string[] args)
    {
        string file = args[0];
        string cmd = args[1];
        string[] values = args[2..];
    }
}

