using AndyTodo.SaveSystem;
using Autofac;

namespace AndyTodo;

public class AndyTodoModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		builder.RegisterType<ListManager>().As<IListManager>().SingleInstance();
		builder.RegisterType<ListSaveManager>().As<IListSaveManager>().SingleInstance();
	}
}