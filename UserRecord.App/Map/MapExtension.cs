using UserRecord.Entity.Dto;

namespace UserRecord.App.Map;

internal static class MapExtension
{
    public static UserDto MapAddUserDto(this string[] parameters)
    {
        int id = 0;
        string firstName = parameters[0];
        string lastName = parameters[1];
        decimal salaryPerHour = MapUtils.ParseSalaryPerHour(parameters[2]);

        return new UserDto(Id: id, FirstName: firstName, LastName: lastName, SalaryPerHour: salaryPerHour);
    }

    public static UserDto MapUpdateUserDto(this string[] parameters)
    {

        int id = MapUtils.ParseId(parameters[0]);
        string firstName = parameters[1];
        string lastName = parameters[2];
        decimal salaryPerHour = MapUtils.ParseSalaryPerHour(parameters[3]);

        return new UserDto(Id: id, FirstName: firstName, LastName: lastName, SalaryPerHour: salaryPerHour);
    }
}

internal class MapUtils
{
    public static int ParseId (string idParameter)
    {            
        if (int.TryParse(idParameter, out var id))
            return id;
        else
            return 0;
    }

    public static  decimal ParseSalaryPerHour(string salaryParameter)
    {
        if (decimal.TryParse(salaryParameter, out var salary))
            return salary;
        else
            return 0;
    }
}