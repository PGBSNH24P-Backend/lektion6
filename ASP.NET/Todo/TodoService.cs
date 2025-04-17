// Vanligtvis hanterar services fel och validering men vi skippar detta för att spara tid
public class TodoService
{

    private readonly TodoRepository todoRepository;

    public TodoService(TodoRepository todoRepository)
    {
        this.todoRepository = todoRepository;
    }

    public async Task<TodoEntity> CreateTodo(CreateTodoRequest request)
    {
        var tags = new List<TagEntity>();
        foreach (var tagName in request.Tags)
        {
            var tagEntity = await todoRepository.FindTagByName(tagName);
            tags.Add(tagEntity!);
        }

        var todoEntity = new TodoEntity(request.Title, tags);
        await todoRepository.AddTodo(todoEntity);
        return todoEntity;
    }

    public async Task<ICollection<TodoEntity>> GetTodos()
    {
        return await todoRepository.GetTodos();
    }
}