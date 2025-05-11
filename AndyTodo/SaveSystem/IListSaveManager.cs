namespace AndyTodo.SaveSystem;

public interface IListSaveManager
{
	/// <summary>
	/// The string path to the save folder location
	/// </summary>
	string SaveFolderPath { get; }
	
	/// <summary>
	/// Save the todo list to a file
	/// </summary>
	/// <param name="todoList">The object to save</param>
	void SaveTodoList(TodoList todoList);

	/// <summary>
	/// Deletes the todo list file
	/// </summary>
	/// <param name="todoList">The todo list to delete</param>
	void DeleteTodoList(TodoList todoList);

	/// <summary>
	/// Loads all todo lists from the save folder
	/// </summary>
	/// <returns>Collection of all loaded todo lists</returns>
	List<TodoList> LoadAllTodoLists();
}