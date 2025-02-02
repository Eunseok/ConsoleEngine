using Core.Math;
using Core.Graphics;

namespace Core.Components;

public class Renderer : Component
{
    public Sprite? Sprite { get; set; } // 렌더러가 사용할 스프라이트
    public Vector2 Offset { get; private set; } = new Vector2().Zero(); // offset


    public Renderer() : base("Renderer")
    {
    }


    public override void Render()
    {
        if (Sprite == null) return;

        // 스프라이트를 부모 객체 위치에 표시
        Vector2 position = new Vector2(Console.CursorLeft - Offset.X, Console.CursorTop - Offset.Y);

        Console.SetCursorPosition(position.X, position.Y);
        foreach (var line in Sprite.Data)
        {
            Console.Write(line);
            Console.SetCursorPosition(position.X, Console.CursorTop + 1);
        }
    }
}