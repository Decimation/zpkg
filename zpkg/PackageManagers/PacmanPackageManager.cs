// Read S zpkg PacmanPackageManager.cs
// 2023-06-05 @ 5:54 PM

namespace zpkg.PackageManagers;

public sealed class PacmanPackageManager : BasePackageManager
{
    public PacmanPackageManager() : base("pacman") { }

    protected override string Cmd_Search => "-S";

    protected override string Cmd_List => "-Q";
}