namespace TableLibFullIntegration.API.Utils
{
    public class ConnectionConfiguration : IConnectionConfiguration
    {
        public readonly IConfiguration Configuration;
        public ConnectionConfiguration(IConfiguration configuration)
        {
            this.ConnectionString = configuration.GetConnectionString("DefaultConnection");
            Configuration = configuration;
        }

        internal string ConnectionString { get; }

        string IConnectionConfiguration.ConnectionString => ConnectionString;
    }
}