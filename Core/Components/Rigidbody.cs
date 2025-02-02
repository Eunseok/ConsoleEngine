using Core.Math;
using Core.Objects;

namespace Core.Components;

public class Rigidbody : Component
{
    public Vector2 Velocity { get; set; } = new Vector2().Zero();
    

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