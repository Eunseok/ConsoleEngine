using Core.Math;

namespace Core.Components;

public class Transform : Component
{
    public Vector2<int> Position { get; set; } = new Vector2<int>().Zero();
    public Vector2<int> Scale { get; set; } = new Vector2<int>().One();
    

    public Transform() : base("Transform")
    {
    }
    public Transform(Vector2<int> pos) : base("Transform")
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