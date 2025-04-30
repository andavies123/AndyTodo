using AndyTodo.Interfaces;
using Autofac;

namespace AndyTodo;

public class AndyTodoModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		// Other
		builder.RegisterType<TodoRepository>().As<ITodoRepository>().SingleInstance();
	}
}