using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using UserRecord.Entity.Models;

namespace UserRecord.Core.Json;

internal static class JsonHelper
{
    public static void SerializeUsers(this List<User> users, string file)
    {
        try
        {
            using (FileStream fileStream = File.Open(file, FileMode.Open))
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.Write(String.Empty, 0, fileStream.Length-1);
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                JsonSerializer.Serialize(fileStream, users, options);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static List<User> DeserializeUsers(this IEnumerable<User> users, string file)
    {
        try
        {         
            using (FileStream fileStream = File.OpenRead(file))
                users = JsonSerializer.Deserialize<User[]>(fileStream);
                
            return users.ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}