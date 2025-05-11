using System.Text.RegularExpressions;
using AndyTodo.Commands;

namespace AndyTodo;

public partial class CommandLineParser
{
	private readonly Dictionary<string, ICommand> m_Commands = new();
	
	public List<string> CommandNames => m_Commands.Keys.ToList();

	public CommandLineParser(IEnumerable<ICommand> commands)
	{
		foreach (ICommand command in commands)
		{
			m_Commands.Add(command.Command, command);
		}
	}

	public void Parse(string input)
	{
		List<string> args = CliArgsRegex().Split(input).Select(x => x.Trim('"')).ToList();
		if (args.Count == 0)
		{
			return;
		}

		if (m_Commands.TryGetValue(args[0], out ICommand? command))
		{
			args.RemoveAt(0);
			if (!command.TryParse(args))
			{
				Console.WriteLine("Invalid usage of command...");
				Console.WriteLine(command.HelpText);
				Console.WriteLine();
			}
		}
	}

	public void DisplayAllHelp()
	{
		foreach (ICommand command in m_Commands.Values)
		{
			Console.WriteLine(command.HelpText);
		}
	}

    [GeneratedRegex(@"\s+(?=(?:[^""]*""[^""]*"")*[^""]*$)")]
    private static partial Regex CliArgsRegex();
}