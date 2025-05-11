namespace AndyTodo;

public interface IListManager
{
	/// <summary>
	/// Collection of all active todo lists stored by the list Id
	/// </summary>
	IReadOnlyDictionary<string, TodoList> TodoLists { get; }

	/// <summary>
	/// Creates and adds a new todo list\
	/// </summary>
	/// <param name="name">The name of the todo list</param>
	void AddNewList(string name);

	/// <summary>
	/// Deletes an existing todo list
	/// </summary>
	/// <param name="name">The name of the todo list</param>
	void DeleteList(string name);
}