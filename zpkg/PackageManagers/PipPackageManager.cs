// Read S zpkg PipPackageManager.cs
// 2023-06-05 @ 6:26 PM

namespace zpkg.PackageManagers;

public sealed class PipPackageManager : BasePackageManager
{
	public PipPackageManager() : base("pip")
	{
	}
}