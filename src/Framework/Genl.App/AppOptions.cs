namespace Genl.App;

public class AppOptions
{
    public string Name { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string Project { get; set; } = string.Empty;

    public string AppInfo => $"{Project} {Name} {Version}";
}
