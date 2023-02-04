namespace Genl.Framework.App;

public record AppInfo(string Name, string Version, string Project)
{
    public override string ToString() => $"{Project} {Name} {Version}";
}
