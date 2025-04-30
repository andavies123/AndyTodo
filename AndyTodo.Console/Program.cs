using AndyTodo;
using AndyTodo.Interfaces;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

ServiceCollection services = [];
services.AddLogging();

ContainerBuilder builder = new();
builder.Populate(services);

builder.RegisterModule<AndyTodoModule>();

IContainer container = builder.Build();

container.Resolve<ITodoRepository>();