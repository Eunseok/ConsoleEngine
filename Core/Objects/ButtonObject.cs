using Core.Components;
using Core.MyMath;
using Core.Graphics;

namespace Core.Objects;

public class ButtonObject : GameObject
{
    private LabelObject? _label;
    
    private ButtonState _state = ButtonState.Normal;
    private ButtonType _type = ButtonType.Default;

    private Sprite[] _buttonSprites = new Sprite[2];

    public ButtonObject() : base("Button")
    {
        _order = 1;
    }

    public void SetType(ButtonType type)
    {
        _type = type;
        SetButtonSprites(_type);
    }

    public void SetOffset(int x, int y)
    {
        GetComponent<Renderer>()?.Sprite?.SetOffset(x, y);
    }
    public override void Awake()
    {
        AddComponent<Transform>();
        AddComponent<Button>();
        AddComponent<Renderer>();

    }
    public override void Initialize()
    {
        Renderer renderer = GetComponent<Renderer>()?? AddComponent<Renderer>();
        _label = Object.Instantiate<LabelObject>(this);

        SetButtonSprites(_type);
        SetRendererSprite(renderer, ButtonState.Normal);
        SetDefaultSize();
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        _state = GetComponent<Button>()!.IsFocused ? ButtonState.Focused : ButtonState.Normal;
        Sprite currentSprite = _buttonSprites[(int)_state];
        GetComponent<Renderer>()?.SetSprite(currentSprite);
    }

    private void SetDefaultSize()
    {
        Sprite normalSprite = _buttonSprites[(int)ButtonState.Normal];
        int newWidth = normalSprite.Width;
        int newHeight = normalSprite.Height + 2;

        GetComponent<Button>()?.SetSize(newWidth, newHeight);
    }

    public void SetLabel(string text, ConsoleColor color = ConsoleColor.White)
    {
        _label?.SetText(text, color);
        SetButtonSprites(_type);
    }
    public void SetLabelColor(ConsoleColor color)
    {
        _label?.SetColor(color);
    }

    private void SetButtonSprites(ButtonType type)
    {
        switch (type)
        {
            case ButtonType.Default:
                CreateDefaultButtonSprites();
                break;
            case ButtonType.Emphasized:
                CreateEmphasizedButtonSprites();
                break;
        }
    }

    private void CreateDefaultButtonSprites()
    {
        int width = _label?.ToString().Sum(c =>
            char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter ? 2 : 1
        ) ?? 0;
        string padding = new string(' ', width);
        _buttonSprites[(int)ButtonState.Normal] = new Sprite(new[] { $"[ {padding} ]" });
        _buttonSprites[(int)ButtonState.Focused] = new Sprite(new[] { $">>[ {padding} ]<<" });
    }

    private void CreateEmphasizedButtonSprites()
    {
        Vector2<int> size = GetComponent<Button>()?.Size ?? new Vector2<int>(1, 1);
        string horizontalBorder = new string('#', size.X);

        string[] sprite = new string[size.Y];
        sprite[0] = horizontalBorder;
        for (int i = 1; i < size.Y - 1; i++)
            sprite[i] = $"#{new string(' ', size.X - 2)}#";
        sprite[size.Y - 1] = horizontalBorder;

        Sprite newSprite = new Sprite(sprite);
        _buttonSprites[(int)ButtonState.Normal] = newSprite;
        _buttonSprites[(int)ButtonState.Focused] = newSprite;
    }

    private void SetRendererSprite(Renderer renderer, ButtonState state)
    {
        renderer.SetSprite(_buttonSprites[(int)state]);
    }

    public enum ButtonState { Normal, Focused }
    public enum ButtonType { Default, Emphasized }
}