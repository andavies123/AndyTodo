using AndyTodo;
using AndyTodo.Interfaces;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

ServiceCollection services = [];
services.AddLogging();

ContainerBuilder builder = new();
builder.Populate(services);

builder.RegisterModule<AndyTodoModule>();

IContainer container = builder.Build();

ITodoRepository todoRepo = container.Resolve<ITodoRepository>();

todoRepo.TryAdd(new TodoItem("Journal", new DailyCompletionFrequency()));
todoRepo.TryAdd(new TodoItem("Drink Water", new DailyCompletionFrequency()));

todoRepo.TryAdd(new TodoItem("Brush Teeth", new DailyCompletionFrequency()));
todoRepo.TryAdd(new TodoItem("Floss", new DailyCompletionFrequency()));
todoRepo.TryAdd(new TodoItem("Shower", new DailyCompletionFrequency()));
todoRepo.TryAdd(new TodoItem("Lotion", new DailyCompletionFrequency()));

todoRepo.TryAdd(new TodoItem("Run", new WeeklyCompletionFrequency(DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday)));

Console.WriteLine(todoRepo.TodoItems.Values.First(x => x.Name == "Run").CompletionFrequency.IsItemActive(DateTime.Today));