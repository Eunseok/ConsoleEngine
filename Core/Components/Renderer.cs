using Core.MyMath;
using Core.Graphics;

namespace Core.Components;

public class Renderer : Component
{
    public Sprite? Sprite { get; private set; } // 렌더러가 사용할 스프라이트

    public ConsoleColor Color { get; set; } = ConsoleColor.White;

    public Renderer() //: base("Renderer")
    {
    }

    public void SetSprite(Sprite sprite)
    {
        Sprite = sprite;
    }

    // 스프라이트 중심 좌표 계산 후 Offset 업데이트
    public override void Update(float deltaTime)
    {
    if (Sprite == null) return;
    
    Vector2<int> pos = Owner!.GlobalPosition - Sprite.Offset;
    
    // System.Math.Clamp를 사용해서 위치 제한
    pos.X = System.Math.Clamp(pos.X, 0, Console.WindowWidth - Sprite.Width);
    pos.Y = System.Math.Clamp(pos.Y, 0, Console.WindowHeight - Sprite.Height);
    
    // Render Logic
    Console.SetCursorPosition(pos.X, pos.Y);
    Console.ForegroundColor = Color;
    foreach (var line in Sprite.Data)
    {
        Console.Write(line);
        Console.SetCursorPosition(pos.X, Console.CursorTop + 1);
    }
    
    Console.ResetColor();
    }
}