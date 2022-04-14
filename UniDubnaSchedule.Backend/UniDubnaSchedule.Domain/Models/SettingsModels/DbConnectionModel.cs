using Microsoft.Data.SqlClient;

namespace UniDubnaSchedule.Domain.Models.SettingsModels;

/// <summary>
/// Model of settings for connecting to the database.
/// </summary>
public class DbConnectionModel
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