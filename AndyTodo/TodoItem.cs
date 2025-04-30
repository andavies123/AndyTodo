namespace AndyTodo;

public class TodoItem
{
	/// <summary>
	/// Unique Id for this instance
	/// </summary>
	public string ItemId { get; } = Guid.NewGuid().ToString();
}

// Todo: How do I keep track of how often a todo item should be completed?