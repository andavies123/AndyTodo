using System.Diagnostics;
using System.Runtime.InteropServices;
using AndyTodo.SaveSystem;

namespace AndyTodo.Commands;

internal class OpenSaveFolderCommand(IListSaveManager listSaveManager) : ICommand
{
	private readonly Dictionary<OSPlatform, string> m_FileExplorerCommands = new()
	{
		{OSPlatform.Windows, "explorer.exe"},
		{OSPlatform.OSX, "open"},
		{OSPlatform.Linux, "xdg-open"}
	};
	
	public string Command => "open-save-folder";
	public string HelpText => $"{Command}";
	
	public bool TryParse(List<string> args)
	{
		if (args.Count != 0)
			return false;
		
		if (TryGetOsPlatform(out OSPlatform? platform) && platform != null && 
		    m_FileExplorerCommands.TryGetValue(platform.Value, out string? processCommand))
		{
			Console.WriteLine($"Attempting to open: {listSaveManager.SaveFolderPath}");
			Process.Start(processCommand, $"\"{listSaveManager.SaveFolderPath}\"");
		}
		else
		{
			Console.WriteLine($"Unsupported OS: {platform}");
			return false;
		}
		
		return true;
	}

	private static bool TryGetOsPlatform(out OSPlatform? platform)
	{
		platform = null;
		
		if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) platform = OSPlatform.Windows;
		else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) platform = OSPlatform.Linux;
		else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) platform = OSPlatform.OSX;
    
		return platform != null;
	}
}