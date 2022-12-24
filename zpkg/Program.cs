using CliWrap.Buffered;

namespace zpkg;

public static class Program
{
	public static async Task Main(string[] args)
	{
		Console.WriteLine("Hello, World!");

		var manager = BasePackageManager.All.Select(r => r.ListAsync()).ToList();

		while (manager.Any()) {
			var o = (await Task.WhenAny(manager));
			manager.Remove(o);
			Console.WriteLine();
			var r = (BufferedCommandResult) await o;
			Console.WriteLine(r.StandardOutput);
		}
	}
}