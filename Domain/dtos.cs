public record CreateTodoRequest(string Name, bool IsCompleted);
public record CreateTodoResponse(Guid id, string Name, bool IsCompleted);