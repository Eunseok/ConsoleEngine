namespace Core;

public abstract class Entity
{
    public string Name { get; private set; }

    protected Entity(string name)
    {
        Name = name;
    }
    
    public void SetName(string name)
    {
        Name = name;
    }
}