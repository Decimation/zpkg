using System.Diagnostics;
using System.Reflection;
using System.Text;
using CliWrap;
using CliWrap.Buffered;
using Novus.OS;
using Novus.Utilities;
using Command = CliWrap.Command;

// ReSharper disable InconsistentNaming
namespace zpkg.PackageManagers;

public abstract class BasePackageManager
{
	public string Name { get; }

	protected Command Base { get; init; }

	[PackageCommand]
	protected virtual string Cmd_Search => "search";

	[PackageCommand]
	protected virtual string Cmd_List => "list";

	public delegate Task<PackageCommandResult> PackageCommandDelegate(string? s = null);

	protected Dictionary<string, PackageCommandDelegate> Commands { get; }

	protected string[] Universal { get; }

	public virtual Task<PackageCommandResult> SearchAsync(string s)
	{
		return Commands[Cmd_Search](s);
	}

	public virtual Task<PackageCommandResult> ListAsync()
	{
		return Commands[Cmd_List]();
	}

	public async Task<PackageCommandResult> RunAsync(IEnumerable<string> args)
	{
		var sb  = new StringBuilder();
		var sb2 = new StringBuilder();

		var cmdArgs = Base.WithArguments(args);

		Debug.WriteLine($"{cmdArgs}");

		var result = await cmdArgs
			             .WithValidation(CommandResultValidation.None)
			             .ExecuteBufferedAsync();

		return new PackageCommandResult()
		{
			CommandResult  = result,
			PackageManager = this
		};
	}

	protected BasePackageManager(string name)
	{
		Name = name;
		Base = new($"{Name}");

		Commands = new()
			{ };

		Universal = GetType().GetAnnotated<PackageCommandAttribute>()
			.Select(e =>
			{
				return e.Member switch
				{
					FieldInfo f    => f.GetValue(this),
					PropertyInfo p => p.GetValue(this)
				};
			})
			.OfType<string>()
			.ToArray();

		foreach (var s in Universal) {
			Bind(s, Commands);
		}
	}

	public void Bind(string s, Dictionary<string, PackageCommandDelegate> d)
	{
		d.Add(s, (x) =>
		{
			return RunAsync(new[] { s, x });

		});

	}

	public static readonly BasePackageManager[] All =
	{
		new ScoopPackageManager(),
		new PacmanPackageManager(),
		new ChocoPackageManager(),
		new WingetPackageManager()
	};
}