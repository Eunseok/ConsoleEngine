using Core.Components;

namespace Core.Objects;

public class LabelObject : GameObject
{
    public LabelObject() : base("Label")
    {
        Order = 2;
    }

    public override void Awake()
    {
        AddComponent<Transform>();
        AddComponent<LabelComponent>();
    }
    public void SetText(string text, ConsoleColor color = ConsoleColor.White)
    {
        GetComponent<LabelComponent>()?.SetLabel(text, color);
    }

    public override string ToString()
    {
        return GetComponent<LabelComponent>()?.ToString() ?? "no text";
    }

}