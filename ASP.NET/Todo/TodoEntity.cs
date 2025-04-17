public class TodoEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public bool Completed { get; set; }
    public ICollection<TagEntity> Tags { get; set; }

    public TodoEntity(string title, ICollection<TagEntity> tags)
    {
        this.Id = Guid.NewGuid();
        this.Completed = false;
        this.Title = title;
        this.Tags = tags;
    }

    public TodoEntity()
    {
        this.Title = string.Empty;
        this.Tags = new List<TagEntity>();
    }
}