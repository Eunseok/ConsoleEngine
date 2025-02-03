using Core.Math;
using Core.Graphics;
using Core.Components;

namespace Core.Objects;

// GameObject: 게임 내 모든 객체의 기본 클래스
public  class GameObject : Entity
{

    
    private readonly Dictionary<string, Component> _components = new Dictionary<string, Component>();
    
    public GameObject? Parent { get;  set; }
    private List<GameObject> Children = new List<GameObject>();
    
    public Vector2<int> GlobalPosition() => Parent == null ? GetComponent<Transform>().Position : Parent.GetComponent<Transform>().Position + GetComponent<Transform>().Position;
    
    public GameObject(string name) : base(name)
    {

    }

    public  void AddChild(GameObject child)
    {
        child.Parent = this;
        child.Initialize();
        Children.Add(child);
    }

    public virtual void Initialize()
    {
        AddComponent<Transform>();
        AddComponent<Renderer>();
    }

    //public abstract void Initialize(); // 각 객체의 초기화시 처리할 작업
    public virtual void Update(float deltaTime)
    {
        foreach (var comp in _components.Values)
        {
            comp.Update(deltaTime);
        }

        foreach (var child in Children)
        {
            child.Update(deltaTime);
        }
    }

    public virtual void Render() // 각 객체가 매 프레임마다 그려질 작업
    {
        // int x = System.Math.Clamp(GlobalPosition().X, 0, Console.WindowWidth - Sprite.Width);
        // int y = System.Math.Clamp(GlobalPosition().Y, 0, Console.WindowHeight - Sprite.Height);
        // Console.SetCursorPosition(x, y);
        
        foreach (var comp in _components.Values)
        {
            comp.Render();
        }

        foreach (var child in Children)
        {
            Transform transform = child.GetComponent<Transform>();
            Vector2<int> pos = child.GlobalPosition();
            Renderer renderer = child.GetComponent<Renderer>();
            
           //Console.SetCursorPosition(pos.X, pos.Y);
            child.Render();
        }
    }
    
    

    
    public T AddComponent<T>() where T : Component, new()
    {
        T comp = new T(); // 인스턴스 생성
        comp.Owner = this;
        comp.Initialize();
        _components.TryAdd(comp.Name, comp);
        
        return comp;   // 생성한 씬 반환
    }
    public T AddComponent<T>(T component) where T : Component, new()
    {
        component.Owner = this;
        component.Initialize();
        _components.TryAdd(component.Name, component);

        return component;   // 생성한 씬 반환
    }

    public T GetComponent<T>() where T : Component, new()
    {
        T comp = new T();
        if(!_components.TryGetValue(comp.Name, out Component? component))
            return null;
        
        return (T)component;
    }
}
