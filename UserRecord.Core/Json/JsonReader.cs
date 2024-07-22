using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using UserRecord.Entity.Models;

namespace UserRecord.Core.Json;

internal class JsonReader : IDisposable
{
    private static string _file;

    public JsonReader(string file)
    {
        _file = file;
    }

    public IList<User> Read()
    {
        try
        {         
            using FileStream fileStream = File.OpenRead(_file);
            return JsonSerializer.Deserialize<IList<User>>(fileStream);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<IList<User>> ReadAsync()
    {
        try
        {         
            using FileStream fileStream = File.OpenRead(_file);
            return await JsonSerializer.DeserializeAsync<IList<User>>(fileStream);   
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
