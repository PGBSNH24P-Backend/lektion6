
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/todo")]
public class TodoController : ControllerBase
{

    private readonly TodoService todoService;

    public TodoController(TodoService todoService)
    {
        this.todoService = todoService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTodo([FromBody] CreateTodoRequest request)
    {
        var todo = await todoService.CreateTodo(request);
        return CreatedAtAction(nameof(CreateTodo), todo.Id);
    }

    [HttpGet]
    public async Task<IActionResult> GetTodos()
    {
        var todos = await todoService.GetTodos();
        return Ok(todos.Select(todo => new TodoResponse(todo)).ToList());
    }
}

public class CreateTodoRequest
{
    public required string Title { get; set; }
    public required ICollection<string> Tags { get; set; }
}

public class TodoResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public bool Completed { get; set; }
    public ICollection<string> Tags { get; set; }

    public TodoResponse(TodoEntity entity)
    {
        this.Id = entity.Id;
        this.Title = entity.Title;
        this.Completed = entity.Completed;
        this.Tags = entity.Tags.Select(tag => tag.Name).ToList();
    }
}