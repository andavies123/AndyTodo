namespace AndyTodo;

public class TodoItem(string name, ICompletionFrequency timeEvent)
{
	/// <summary>
	/// Unique Id for this instance
	/// </summary>
	public string ItemId { get; } = Guid.NewGuid().ToString();

	/// <summary>
	/// Name for this instance
	/// </summary>
	public string Name { get; } = name;

	/// <summary>
	/// How often this item should be completed
	/// </summary>
	public ICompletionFrequency CompletionFrequency { get; } = timeEvent;
}

public interface ICompletionFrequency
{
	/// <summary>
	/// Calculates if this item should be completed on the given day
	/// </summary>
	/// <param name="dateTime">The date time object of the day to check against</param>
	/// <returns>True if the item should be completed. False if not</returns>
	bool IsItemActive(DateTime dateTime);
}

public class DailyCompletionFrequency : ICompletionFrequency
{
	public bool IsItemActive(DateTime dateTime) => true;
}

public class WeeklyCompletionFrequency(params DayOfWeek[] daysOfWeek) : ICompletionFrequency
{
	private readonly HashSet<DayOfWeek> m_DaysOfWeek = daysOfWeek.ToHashSet();
	
	public bool IsItemActive(DateTime dateTime) => m_DaysOfWeek.Contains(dateTime.DayOfWeek);
}