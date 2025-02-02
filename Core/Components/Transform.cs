using Core.Math;

namespace Core.Components;

public class Transform : Component
{
    public Vector2 Position { get; set; }
    public Vector2 Scale { get; set; } = new Vector2().One();
    

    public Transform() : base("Transform")
    {
    }
    public Transform(Vector2 pos) : base("Transform")
    {
        Position = pos;
    }

    public override void Update(float deltaTime)
    {
            
    }

    public override void Render()
    {

    }
}