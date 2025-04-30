using System.Collections.Concurrent;
using AndyTodo.Interfaces;
using Microsoft.Extensions.Logging;

namespace AndyTodo;

public class TodoRepository : ITodoRepository
{
	private readonly ILogger m_Logger;
	private readonly ConcurrentDictionary<string, TodoItem> m_TodoItems = new();

	public IReadOnlyDictionary<string, TodoItem> TodoItems => m_TodoItems;

	public TodoRepository(ILogger<TodoRepository> logger)
	{
		m_Logger = logger ?? throw new ArgumentNullException(nameof(logger));
	}

	public bool TryAdd(TodoItem todoItem)
	{
		if (!m_TodoItems.TryAdd(todoItem.ItemId, todoItem))
		{
			m_Logger.LogWarning("Failed to add {TodoItemType} with Id: {ItemId}", nameof(TodoItem), todoItem.ItemId);
			return false;
		}

		m_Logger.LogInformation("Successfully added {TodoItemType} with Id: {ItemId}", nameof(TodoItem), todoItem.ItemId);
		return true;
	}

	public bool TryRemove(string todoId, out TodoItem? removedItem)
	{
		if (!m_TodoItems.TryRemove(todoId, out removedItem))
		{
			m_Logger.LogWarning("Failed to remove {TodoItemType} with Id: {ItemId}", nameof(TodoItem), todoId);
			return false;
		}
		
		m_Logger.LogInformation("Successfully removed {TodoItemType} with Id: {ItemId}", nameof(TodoItem), todoId);
		return false;
	}
}