namespace AndyTodo.Interfaces;

public interface ITodoRepository
{
	/// <summary>
	/// Collection of todo items stored by item Id
	/// </summary>
	IReadOnlyDictionary<string, TodoItem> TodoItems { get; }

	/// <summary>
	/// Tries to add a todo item to the internal collection
	/// </summary>
	/// <param name="todoItem">The item to add</param>
	/// <returns>
	/// True if the todo item was successfully added.
	/// False if the todo item was not successfully added.
	/// </returns>
	bool TryAdd(TodoItem todoItem);

	/// <summary>
	/// Tries to remove a todo item from the internal collection using the item Id
	/// </summary>
	/// <param name="todoId">The unique Id corresponding to the todo item to remove</param>
	/// <param name="removedItem">The item that was found and removed from the internal collection</param>
	/// <returns>
	/// True if the todo item was successfully removed.
	///	False if the todo item was not found or removed.
	/// </returns>
	bool TryRemove(string todoId, out TodoItem? removedItem);
}