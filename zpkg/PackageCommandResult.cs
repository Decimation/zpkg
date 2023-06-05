// Read S zpkg PackageCommandResult.cs
// 2023-06-05 @ 5:55 PM

using CliWrap.Buffered;
using zpkg.PackageManagers;

namespace zpkg;

public class PackageCommandResult
{
	public BufferedCommandResult CommandResult { get; internal init; }

	public BasePackageManager PackageManager { get; internal init; }
}