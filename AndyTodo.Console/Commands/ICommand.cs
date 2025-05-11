namespace AndyTodo.Commands;

public interface ICommand
{
	string Command { get; }
	string HelpText { get; }

	bool TryParse(List<string> args);
}