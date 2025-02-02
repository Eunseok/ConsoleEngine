using Core.Input;
using Core.Math;
using Core.Objects;

namespace TestGame.Objects;

public class TestObj : GameObject
{
    private  Vector2 Velocity { get; set; }

    public TestObj(int x, int y, char shape) : base(x, y, shape)
    {
        Velocity = new Vector2().Right();
    }
    
    
    public override void Update()
    {
        Velocity = new Vector2().Zero();
        if(InputManager.GetKey("MoveLeft"))
            Velocity = new Vector2().Left();
        if(InputManager.GetKey("MoveRight"))
            Velocity = new Vector2().Right();
        if(InputManager.GetKey("MoveUp"))
            Velocity = new Vector2().Up();
        if(InputManager.GetKey("MoveDown"))
            Velocity = new Vector2().Down();
       
        Position += Velocity; // 현재 위치를 속도만큼 이동
    }

    public override void Render() // 각 객체가 매 프레임마다 그려질 작업
    {
        Console.Write(Shape);
    }
}