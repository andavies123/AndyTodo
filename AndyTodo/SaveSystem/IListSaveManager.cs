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
}