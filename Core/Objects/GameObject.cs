using Core.Math;
using Core.Graphics;
using Core.Components;

namespace Core.Objects;

// GameObject: 게임 내 모든 객체의 기본 클래스
public  class GameObject : Entity
{

    
    private readonly Dictionary<string, Component> _components = new Dictionary<string, Component>();
    
    public GameObject(string name) : base(name)
    {

    }

    //public abstract void Initialize(); // 각 객체의 초기화시 처리할 작업
    public virtual void Update(float deltaTime)
    {
        foreach (var comp in _components.Values)
        {
            comp.Update(deltaTime);
        }
    }

    public virtual void Render() // 각 객체가 매 프레임마다 그려질 작업
    {
        foreach (var comp in _components.Values)
        {
            comp.Render();
        }

    }
    
    public T AddComponent<T>() where T : Component, new()
    {
        T comp = new T(); // 인스턴스 생성
        comp.Owner = this;
        _components.TryAdd(comp.Name, comp);

        return comp;   // 생성한 씬 반환
    }
    public T AddComponent<T>(T component) where T : Component, new()
    {
        component.Owner = this;
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
