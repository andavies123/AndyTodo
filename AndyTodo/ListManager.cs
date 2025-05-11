using System.Collections.Concurrent;
using AndyTodo.SaveSystem;
using Microsoft.Extensions.Logging;

namespace AndyTodo;

public class ListManager : IListManager
{
	private readonly ConcurrentDictionary<string, TodoList> m_TodoLists = new();
	private readonly ILogger m_Logger;
	private readonly IListSaveManager m_SaveManager;

	public ListManager(ILogger<ListManager> logger, IListSaveManager saveManager)
	{
		m_Logger = logger;
		m_SaveManager = saveManager;

		m_SaveManager.LoadAllTodoLists().ForEach(AddExistingList);
	}

	public IReadOnlyDictionary<string, TodoList> TodoLists => m_TodoLists;

	public void AddNewList(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			m_Logger.LogWarning("Unable to create a todo list with an invalid name.");
			return;
		}

		TodoList todoList = new() { Name = name };
		AddExistingList(todoList);
		m_SaveManager.SaveTodoList(todoList);
		
		m_Logger.LogInformation("Created new todo list named {Name}", todoList.Name);
	}

	public void AddExistingList(TodoList todoList)
	{
		m_TodoLists[todoList.Id] = todoList;
	}

	public void DeleteList(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			m_Logger.LogWarning("Unable to delete a todo list with an invalid name.");
			return;
		}

		TodoList? listToRemove = m_TodoLists.Values.FirstOrDefault(list => list.Name == name);
		if (listToRemove != null && m_TodoLists.TryRemove(listToRemove.Id, out TodoList? removedList))
		{
			m_SaveManager.DeleteTodoList(removedList);
			m_Logger.LogDebug("Removed list named {Name}", listToRemove.Name);
		}
		else
		{
			m_Logger.LogDebug("Unable to find list named {Name}", name);
		}
	}
}