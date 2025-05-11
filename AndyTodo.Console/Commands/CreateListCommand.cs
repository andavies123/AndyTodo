namespace AndyTodo.Commands;

internal class CreateListCommand(IListManager listManager) : ICommand
{
	public string Command => "create-list";
	public string HelpText => $"{Command} \"list name\"";

	public bool TryParse(List<string> args)
	{
		if (args.Count != 1)
			return false;

		listManager.AddNewList(args[0]);
		return true;
	}
}