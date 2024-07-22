using System.Linq;
using System.Threading.Tasks;
using UserRecord.Entity.Models;

namespace UserRecord.Core.Json;

internal class JsonManager : IDisposable
{
    public static IList<User> Users { get; set; }

    public IList<User> ReadJson(string file)
    {
        IList<User> users;
        using (var reader = new JsonReader(file))
            users = reader.Read();
        return users;
    }

    public async Task<IList<User>> ReadJsonAsync(string file)
    {
        IList<User> users;
        using (var reader = new JsonReader(file))
            users = await reader.ReadAsync();

        return users;  
    }

    public void WriteJson(string file)
    {
        using var reader = new JsonWriter(file);
        reader.Write(Users); 
    }

    public async Task WriteJsonAsync(string file)
    {
        using var reader = new JsonWriter(file);
        await reader.WriteAsync(Users);
    }

    public void Dispose()
    {
        Users = null;
    }
}