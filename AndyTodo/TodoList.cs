namespace AndyTodo;

public class TodoList
{
	/// <summary>
	/// Unique Id for this Todo List
	/// </summary>
	public string Id { get; init; } = Guid.NewGuid().ToString();

	/// <summary>
	/// Name of this Todo List instance
	/// </summary>
	public required string Name { get; set; }

	/// <summary>
	/// Collection of items in the todo list
	/// </summary>
	public Dictionary<string, TodoItem> Items { get; } = new();
}