namespace AndyTodo.Commands;

internal class DeleteListCommand(IListManager listManager) : ICommand
{
	public string Command => "delete-list";
	public string HelpText => $"{Command} \"list name\"";

	public bool TryParse(List<string> args)
	{
		if (args.Count != 1)
			return false;

		listManager.DeleteList(args[0]);
		return true;
	}
}