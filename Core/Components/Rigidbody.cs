using Core.Math;
using Core.Objects;

namespace Core.Components;

public class Rigidbody : Component
{
    public Vector2<int> Velocity { get; set; } = new Vector2<int>().Zero();
    

    public Rigidbody() : base("Rigdibody")
    {
    }

    public override void Update(float deltaTime)
    {
        Owner!.GetComponent<Transform>().Position += Velocity;
    }

    public override void Render()
    {

    }
}