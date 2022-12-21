namespace BlazorAdmin.Data;

public class SqlConnectionConfiguration
{
    public string ConnectionString { get; }
    public SqlConnectionConfiguration(string connectionString)
        => ConnectionString = connectionString;

}
