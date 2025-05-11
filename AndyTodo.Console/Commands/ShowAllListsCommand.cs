namespace AndyTodo.Commands;

internal class ShowAllListsCommand(IListManager listManager) : ICommand
{
	public string Command => "show-all-lists";
	public string HelpText => $"{Command}";

	public bool TryParse(List<string> args)
	{
		if (args.Count != 0)
			return false;

		foreach (TodoList list in listManager.TodoLists.Values)
		{
			Console.WriteLine(list.Name);
		}

		return true;
	}
}