namespace ContactRegistry.Persistence;

public static class Configurations
{
    public static string GetConnectionString
    {
        get { return "Server=127.0.0.1;Port=5432;Database=ContactDb;User Id=postgres;Password=123456;"; }
    }
}