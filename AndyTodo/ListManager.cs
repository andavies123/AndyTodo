using System.Collections.Concurrent;
using AndyTodo.SaveSystem;
using Microsoft.Extensions.Logging;

namespace AndyTodo;

// Todo: Save the todo list when one gets added
// Todo: Load todo lists on startup
public class ListManager(ILogger<ListManager> logger, IListSaveManager saveManager) : IListManager
{
	private readonly ConcurrentDictionary<string, TodoList> m_TodoLists = new();
	
	public IReadOnlyDictionary<string, TodoList> TodoLists => m_TodoLists;

	public void AddNewList(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			logger.LogWarning("Unable to create a todo list with an invalid name.");
			return;
		}

		TodoList todoList = new() { Name = name };
		m_TodoLists[todoList.Id] = todoList;
		
		logger.LogDebug("Created new todo list named {Name}", todoList.Name);
	}

	public void DeleteList(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			logger.LogWarning("Unable to delete a todo list with an invalid name.");
			return;
		}

		TodoList? listToRemove = m_TodoLists.Values.FirstOrDefault(list => list.Name == name);
		if (listToRemove != null && m_TodoLists.TryRemove(listToRemove.Id, out _))
		{
			logger.LogDebug("Removed list named {Name}", listToRemove.Name);
		}
		else
		{
			logger.LogDebug("Unable to find list named {Name}", name);
		}
	}
}