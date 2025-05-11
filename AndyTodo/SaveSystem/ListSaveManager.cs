using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace AndyTodo.SaveSystem;

internal class ListSaveManager : IListSaveManager
{
	private readonly JsonSerializerOptions m_JsonSerializerOptions = new() { WriteIndented = true };
	private readonly ILogger m_Logger;

	public ListSaveManager(ILogger<ListSaveManager> logger)
	{
		m_Logger = logger;
		
		Directory.CreateDirectory(SaveFolderPath);
	}

	public string SaveFolderPath { get; } = Path.GetFullPath(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AndyTodo"));
	
	public void SaveTodoList(TodoList todoList)
	{
		m_Logger.LogInformation("Attempting to save \"{Name}\" todo list...", todoList.Name);
		
		string filePath = Path.Combine(SaveFolderPath, $"{todoList.Id}.json");
		string json = JsonSerializer.Serialize(todoList, m_JsonSerializerOptions);
		
		File.WriteAllText(filePath, json);
		
		m_Logger.LogInformation("Save complete!");
	}

	public void DeleteTodoList(TodoList todoList)
	{
		m_Logger.LogInformation("Attempting to delete \"{Name}\" todo list...", todoList.Name);
		
		string filePath = Path.Combine(SaveFolderPath, $"{todoList.Id}.json");
		File.Delete(filePath);
		
		m_Logger.LogInformation("Save deleted!");
	}

	public List<TodoList> LoadAllTodoLists()
	{
		List<TodoList> todoLists = [];
		
		if (!Directory.Exists(SaveFolderPath))
			return todoLists;

		string[] filePaths = Directory.GetFiles(SaveFolderPath, "*.json", SearchOption.TopDirectoryOnly);

		foreach (string filePath in filePaths)
		{
			TodoList? todoList = JsonSerializer.Deserialize<TodoList>(File.ReadAllText(filePath));
			if (todoList != null)
			{
				todoLists.Add(todoList);
			}
		}

		return todoLists;
	}
}