using CliWrap.Buffered;
using zpkg.PackageManagers;

namespace zpkg;

public static class Program
{
	public static async Task Main(string[] args)
	{
		Console.WriteLine("Hello, World!");

		var q = Console.ReadLine();

		var manager = BasePackageManager.All.Select(r =>
		{
			return r.SearchAsync(q);
		}).ToList();

		int i  = 0;
		int cn = manager.Count;

		while (manager.Any()) {
			var o = (await Task.WhenAny(manager));
			manager.Remove(o);
			Console.Title = $"\r{++i}/{cn}";
			var r = await o;
			Console.WriteLine(r.CommandResult.StandardOutput);
			Console.WriteLine(r.CommandResult.StandardError);
		}
		
	}
}