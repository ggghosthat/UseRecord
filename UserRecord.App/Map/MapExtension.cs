internal static class MapExtension
{
    public static UserDto CreateUserDto(this string[] parameters)
    {
        int ParseId (string idParameter)
        {            
            if (int.TryParse(idParameter, out var id))
                return id;
            else
               return 0;
        }

        decimal ParseSalaryPerHour(string salaryParameter)
        {
            if (decimal.TryParse(salaryParameter, out var salary))
                return salary;
            else
               return 0;
        }

        int id = ParseId(parameters[0]);
        string firstName = parameters[1];
        string lastName = parameters[2];
        decimal salaryPerHour = ParseSalaryPerHour(parameters[3]);

        return new UserDto(Id: id, FirstName: firstName, LastName: lastName, SalaryPerHour: salaryPerHour);
    }
}