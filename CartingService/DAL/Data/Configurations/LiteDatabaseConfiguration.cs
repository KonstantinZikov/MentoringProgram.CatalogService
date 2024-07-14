using DAL.Common.Interface;

namespace DAL.Data.Configurations
{
    public class LiteDatabaseConfiguration(string connectionString) : ILiteDatabaseConfiguration
    {
        public string ConnectionString { get; init; } = connectionString;
    }
}
