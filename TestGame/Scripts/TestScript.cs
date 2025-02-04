using Core.Components;
using Core.Input;
using Core.MyMath;

namespace TestGame.Scripts;

public class TestScript : Script
{
    protected override void OnUpdate(float deltaTime)
    {
        
        InputHandler();
    }
    
    private void InputHandler()
    {
        Vector2<int> velcity = Vector2<int>.Zero();
        if (InputManager.GetKey("MoveLeft"))
            velcity += Vector2<int>.Left();
        if(InputManager.GetKey("MoveRight"))
            velcity += Vector2<int>.Right();
        if (InputManager.GetKey("MoveUp"))
            velcity += Vector2<int>.Up();
        if (InputManager.GetKey("MoveDown"))
            velcity += Vector2<int>.Down();
        
        Owner.GetComponent<Transform>()?.Translate(velcity);
    }
}