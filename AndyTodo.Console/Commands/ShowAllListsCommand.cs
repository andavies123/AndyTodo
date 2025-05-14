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

internal class AddTodoItemCommand(IListManager listManager) : ICommand
{
	public string Command => "add-todo-item";
	public string HelpText => $"{Command} \"list name\" \"item name\"";

	public bool TryParse(List<string> args)
	{
		if (args.Count < 2)
			return false;

		string listName = args[0];
		string itemName = args[1];
		
		return true;
	}
}