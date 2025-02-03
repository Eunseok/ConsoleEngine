using Core.Components;
using Core.Graphics;
using Core.Math;

namespace Core.Objects;

public class Text : GameObject
{
    private string _text;
    private ConsoleColor _color;
    private Vector2<float> _anchor;

    public Text(string text, ConsoleColor color = ConsoleColor.White,
                Vector2<float>? anchor = null) : base("Text")

    {
        _text = text;
        _color = color;
        
        _anchor = anchor ?? new Vector2<float>().Zero();
    }
    

    public override void Initialize() // 각 객체의 초기화시 처리할 작업
    {
        base.Initialize();
        Renderer renderer = GetComponent<Renderer>();
        renderer.Sprite = Sprite.FromString(_text);
        renderer.Color = _color;
        
        ApplyAnchor(renderer);
        

    }

    public void SetAnchor(Vector2<float> anchor)
    {
        _anchor = anchor;
    }
    private void ApplyAnchor(Renderer renderer)
    {
        // 텍스트 길이에 따라 anchor에 따라 위치 계산
        int textWidth = renderer.Sprite.Width;
        int textHeight = renderer.Sprite.Height;
        Vector2<float> offset = new Vector2<float>(_anchor.X * textWidth, _anchor.Y * textHeight); // 오프셋 설정
        renderer.Sprite.Offset += (Vector2<int>)offset; // Anchor를 통해 위치 조정
    }

    
    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
    }

    public override void Render() // 각 객체가 매 프레임마다 그려질 작업
    {
        base.Render();

    }
    
}