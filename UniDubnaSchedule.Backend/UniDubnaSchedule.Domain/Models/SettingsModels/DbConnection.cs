using Microsoft.Data.SqlClient;

namespace UniDubnaSchedule.Domain.Models.SettingsModels;

public class DbConnection
{
    public string? DataSource { get; set; }
    public string? InitialCatalog { get; set; }
    public string? UserId { get; set; }
    public string? Password { get; set; }

    public string ConnectionString =>
        new SqlConnectionStringBuilder
        {
            DataSource = DataSource,
            InitialCatalog = InitialCatalog,
            IntegratedSecurity = false,
            UserID = UserId,
            Password = Password
        }.ConnectionString;
}