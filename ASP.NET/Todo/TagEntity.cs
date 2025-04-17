public class TagEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    public ICollection<TodoEntity> Todos { get; set; }


    public TagEntity(string name)
    {
        this.Id = Guid.NewGuid();
        this.Name = name;
        this.Todos = new List<TodoEntity>();
    }

    public TagEntity()
    {
        this.Name = string.Empty;
        this.Todos = new List<TodoEntity>();
    }
}