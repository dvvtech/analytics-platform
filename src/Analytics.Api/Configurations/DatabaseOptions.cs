namespace Analytics.Api.Configurations
{
    public class DatabaseOptions
    {
        public const string SectionName = "DatabaseOptions";

        public required string ConnectionString { get; set; }
    }
}
