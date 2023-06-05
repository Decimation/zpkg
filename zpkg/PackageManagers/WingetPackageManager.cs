// Read S zpkg WingetPackageManager.cs
// 2023-06-05 @ 5:54 PM

namespace zpkg.PackageManagers;

public sealed class WingetPackageManager : BasePackageManager
{
    public WingetPackageManager() : base("winget") { }
}