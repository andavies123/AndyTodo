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
		m_Logger.LogInformation("Saving {Name} todo list", todoList.Name);
		
		string filePath = Path.Combine(SaveFolderPath, $"{todoList.Id}.json");
		string json = JsonSerializer.Serialize(todoList, m_JsonSerializerOptions);
		
		File.WriteAllText(filePath, json);
		
		m_Logger.LogInformation("Save complete");
	}
}