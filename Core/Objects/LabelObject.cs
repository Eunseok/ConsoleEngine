using Core.Components;

namespace Core.Objects;

public class LabelObject : GameObject
{
    public LabelObject() : base("Label")
    {
        
    }

    public override void Initialize()
    {
        AddComponent<Transform>();
        AddComponent<LabelComponent>();
    }
    public void SetText(string text, ConsoleColor color = ConsoleColor.White)
    {
        GetComponent<LabelComponent>()?.SetLabel(text, color);
    }
}