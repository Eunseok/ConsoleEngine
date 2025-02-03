using Core.Math;
using Core.Graphics;

namespace Core.Components;

public class Renderer : Component
{
    public Sprite? Sprite { get; set; } // 렌더러가 사용할 스프라이트

    
    public ConsoleColor Color { get; set; } = ConsoleColor.White;


    public Renderer() : base("Renderer")
    {
    }
    
    // 스프라이트 중심 좌표 계산 후 Offset 업데이트


    public override void Render()
    {
        if (Sprite == null) return;
        
        
        
        
        Vector2<int> pos = Owner!.GlobalPosition() - Sprite.Offset;
        
        pos.X = System.Math.Clamp(pos.X, 0, Console.WindowWidth - Sprite.Width);
        pos.Y = System.Math.Clamp(pos.Y, 0, Console.WindowHeight - Sprite.Height);
       
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