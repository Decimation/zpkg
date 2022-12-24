using System.Text;
using CliWrap;
using CliWrap.Buffered;
using Novus.OS;
using Command = CliWrap.Command;

// ReSharper disable InconsistentNaming
namespace zpkg;

public abstract class BasePackageManager
{
	public string Name { get; }

	protected Command Base { get; init; }

	protected string Cmd_Search { get; init; }

	protected string Cmd_List { get; init; }

	public virtual async Task<CommandResult> SearchAsync(string s)
	{
		return await RunAsync(new[] { Cmd_Search, s });
	}

	public virtual async Task<CommandResult> ListAsync()
	{
		return await RunAsync(new[] { Cmd_List });
	}

	public async Task<CommandResult> RunAsync(IEnumerable<string> args)
	{
		var result = await Base.WithArguments(args)
		                       .ExecuteBufferedAsync();
		return result;
	}

	protected BasePackageManager(string name,
	                             string cmdSearch = "search",
	                             string cmdList = "list")
	{
		Name       = name;
		Base       = new($"{Name}");
		Cmd_Search = cmdSearch;
		Cmd_List   = cmdList;
	}

	public static readonly BasePackageManager[] All =
	{
		new ScoopPackageManager(), 
		new PacmanPackageManager(), 
		new ChocoPackageManager()
	};
}

public sealed class ScoopPackageManager : BasePackageManager
{
	public ScoopPackageManager() : base("scoop") { }
}

public sealed class PacmanPackageManager : BasePackageManager
{
	public PacmanPackageManager() : base("pacman", "-Q", "-Q") { }
}

public sealed class ChocoPackageManager : BasePackageManager
{
	public ChocoPackageManager() : base("choco") { }
}