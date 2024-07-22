using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using UserRecord.Entity.Models;

namespace UserRecord.Core.Json;

internal class JsonWriter : IDisposable
{
    private static string _file;

    public JsonWriter(string file)
    {
        _file = file;
    }

    public void Write(IList<User> users)
    {
        try
        {
            using FileStream fileStream = File.Open(_file, FileMode.Open);
            JsonSerializer.Serialize(fileStream, users);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task WriteAsync(IList<User> users)
    {
        try
        {
            using FileStream fileStream = File.Open(_file, FileMode.Open);
            await JsonSerializer.SerializeAsync(fileStream, users);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    
    public void Dispose()
    {
        _file = null;
    }
}