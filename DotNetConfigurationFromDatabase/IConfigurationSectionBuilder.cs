namespace DotNetConfigurationFromDatabase
{
    public interface IConfigurationSectionBuilder
    {
        TSection BuildSection<TSection>(string xml) where TSection : System.Configuration.ConfigurationSection;
    }
}