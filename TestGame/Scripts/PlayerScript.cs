using Core.Components;
using Core.Graphics;
using Core.Input;
using Core.Math;
using Core.Objects;

namespace TestGame.Scripts;

public class PlayerScript : Script
{
    
    public override void Update(float deltaTime)
    {
        
        Rigidbody rigidbody = Owner!.GetComponent<Rigidbody>();
        rigidbody.Velocity = new Vector2<int>().Zero();

        if (InputManager.GetKey("MoveLeft"))
            rigidbody.Velocity = Vector2<int>.Left();
        if (InputManager.GetKey("MoveRight"))
            rigidbody.Velocity = Vector2<int>.Right();
        if (InputManager.GetKey("MoveUp"))
            rigidbody.Velocity = Vector2<int>.Up();
        if (InputManager.GetKey("MoveDown"))
            rigidbody.Velocity = Vector2<int>.Down();
        
    }


}