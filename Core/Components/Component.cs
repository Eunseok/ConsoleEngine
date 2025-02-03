using Core.Objects;

namespace Core.Components;

public abstract class Component : Entity
{
    public GameObject? Owner { get;  set; } = null;
    
    
    public Component(string name) : base(name)
    {
    }

    public virtual void Initialize()
    {
        
    }
    
    public virtual void Update(float deltaTime)
    {
        
    }

    public virtual void Render()
    {
        
    }
}