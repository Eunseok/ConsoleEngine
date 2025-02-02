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
        rigidbody.Velocity = new Vector2().Zero();
        
        if(InputManager.GetKey("MoveLeft"))
            rigidbody.Velocity = new Vector2().Left();
        if(InputManager.GetKey("MoveRight"))
            rigidbody.Velocity = new Vector2().Right();
        if(InputManager.GetKey("MoveUp"))
            rigidbody.Velocity = new Vector2().Up();
        if(InputManager.GetKey("MoveDown"))
            rigidbody.Velocity = new Vector2().Down();
        
    }


}