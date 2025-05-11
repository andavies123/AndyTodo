namespace AndyTodo;

public class TodoItem
{
	/// <summary>
	/// Unique Id for this instance
	/// </summary>
	public string Id { get; init; } = Guid.NewGuid().ToString();

	/// <summary>
	/// Name for this instance
	/// </summary>
	public required string Name { get; set; }

	/// <summary>
	/// How often this item should be completed
	/// </summary>
	public required ICompletionFrequency CompletionFrequency { get; set; }

	/// <summary>
	/// The date we should start keeping track of this item
	/// </summary>
	public required DateTime StartDate { get; set; }
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