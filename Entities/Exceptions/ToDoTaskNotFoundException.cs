namespace Entities.Exceptions;

public class ToDoTaskNotFoundException : NotFoundException
{
	public ToDoTaskNotFoundException(Guid todoTaskId) 
		: base($"The company with id: {todoTaskId} doesn't exist in the database.")
    { }
}
