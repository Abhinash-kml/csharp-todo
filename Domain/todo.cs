using System.ComponentModel.DataAnnotations;

public class Todo
{
    [Key]
    public Guid Id { get; init; } = Guid.CreateVersion7();
    public string Name { get; set; } = "";
    public bool IsCompleted { get; set; } = false;

    public Todo(Guid id, string name, bool isCompleted)
    {
        Id = id;
        Name = name;
        IsCompleted = isCompleted;
    }

    public Todo() {}
}