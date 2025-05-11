using AndyTodo;
using AndyTodo.Commands;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

ServiceCollection services = [];
services.AddLogging(loggingBuilder =>
{
	loggingBuilder.SetMinimumLevel(LogLevel.Debug);
	loggingBuilder.AddSimpleConsole(options =>
	{
		options.SingleLine = true;
		options.TimestampFormat = "[yyyy-MM-dd HH:mm:ss] ";
		options.ColorBehavior = LoggerColorBehavior.Enabled;
	});
});

ContainerBuilder builder = new();
builder.Populate(services);
builder.RegisterModule<AndyTodoModule>();

builder.RegisterType<CommandLineParser>().SingleInstance();
builder.RegisterAssemblyTypes(typeof(ICommand).Assembly)
	.Where(t => typeof(ICommand).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
	.As<ICommand>().SingleInstance();

IContainer container = builder.Build();

CommandLineParser commandLineParser = container.Resolve<CommandLineParser>();

bool keepParsing = true;
while (keepParsing)
{
	Console.Write("=> ");
	string input = ReadInputWithAutocomplete(commandLineParser.CommandNames);
	
	if (string.IsNullOrEmpty(input))
		continue;

	if (input.Equals("q", StringComparison.OrdinalIgnoreCase))
	{
		keepParsing = false;
		continue;
	}

	if (input.Equals("help", StringComparison.OrdinalIgnoreCase))
	{
		Console.WriteLine("q => Quit");
		Console.WriteLine("help => Help");
		commandLineParser.DisplayAllHelp();
		continue;
	}
	
	commandLineParser.Parse(input);
	
	Thread.Sleep(50); // Used to make sure log statements are printed before continuing
	Console.WriteLine();
}

Console.WriteLine("Exiting...");
return;

static string ReadInputWithAutocomplete(List<string> commandNames)
{
	string input = "";
	while (true)
	{
		ConsoleKeyInfo key = Console.ReadKey(intercept: true);

		if (key.Key == ConsoleKey.Tab) // Handle Tab key for autocomplete
		{
			string? foundCommand = commandNames.FirstOrDefault(command => command.StartsWith(input));
			if (!string.IsNullOrEmpty(foundCommand))
			{
				Console.Write(new string('\b', input.Length)); // Remove existing input
				Console.Write(foundCommand); // Print suggestion
				input = foundCommand;
			}
		}
		else if (key.Key == ConsoleKey.Enter) // Handle Enter key
		{
			Console.WriteLine();
			break;
		}
		else if (key.Key == ConsoleKey.Backspace && input.Length > 0) // Handle Backspace
		{
			Console.Write("\b \b");
			input = input[..^1]; // Remove last character
		}
		else // Handle normal character input
		{
			Console.Write(key.KeyChar);
			input += key.KeyChar;
		}
	}
	return input;
}