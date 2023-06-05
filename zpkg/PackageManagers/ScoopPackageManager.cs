// Read S zpkg ScoopPackageManager.cs
// 2023-06-05 @ 5:54 PM

namespace zpkg.PackageManagers;

public sealed class ScoopPackageManager : BasePackageManager
{
    public ScoopPackageManager() : base("scoop") { }
}