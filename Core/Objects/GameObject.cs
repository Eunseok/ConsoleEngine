using Core.Math;

namespace Core.Objects;

// GameObject: 게임 내 모든 객체의 기본 클래스
public abstract class GameObject
{
    public Vector2 Position { get; set; }
    public char Shape { get; set; } // 화면에 출력될 문자

    protected GameObject()
    {
        Position = new Vector2().Zero();
        Shape = ' ';
    }
    
    protected GameObject(int x, int y, char shape)
    {
        Position = new Vector2(x, y);
        Shape = shape;
    }

    //public abstract void Initialize(); // 각 객체의 초기화시 처리할 작업
    public abstract void Update(); // 각 객체가 매 프레임마다 처리할 작업
    public abstract void Render(); // 각 객체가 매 프레임마다 그려질 작업
}
