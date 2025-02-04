using Core.Components;
using Core.MyMath;
using Core.Graphics;

namespace Core.Objects;

public class BoxObject : GameObject
{
    public BoxObject() : base("Box") { }

    public override void Awake()
    {
        AddComponent<Transform>();
        AddComponent<Renderer>();

    }
    public override void Initialize()
    {
        
        SetSize(10, 3);
       
    }

    public void SetSize(int width, int height)
    {
        Renderer renderer = GetComponent<Renderer>()?? AddComponent<Renderer>();
        Sprite sprite = new Sprite(CreateSprites(width, height));
        renderer?.SetSprite(sprite);
    }
    
    

    private string[] CreateSprites(int width, int height)
    {
        string horizontalBorder = new string('#', width);

        string[] sprite = new string[height];
        sprite[0] = horizontalBorder;
        for (int i = 1; i < height - 1; i++)
            sprite[i] = $"#{new string(' ', width - 2)}#";
        sprite[height - 1] = horizontalBorder;

        return sprite;
    }
    
}