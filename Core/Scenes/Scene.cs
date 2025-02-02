using Core.Components;
using Core.Math;
using Core.Objects;
    
namespace Core.Scenes;

// Scene: 게임의 상태(레벨, 화면)를 소유
public class Scene : Entity
{
    private readonly List<GameObject> _gameObjects;
    
    public Scene(string name) : base(name) 
    {
        _gameObjects = new List<GameObject>();
    }

    public void AddObject(GameObject obj)
    {
        //obj.AddComponent<Transform>();
        _gameObjects.Add(obj);
    }

    public virtual void Initialize()
    {
        // foreach (var gameObject in _gameObjects)
        // {
        //     gameObject.Initialize();
        // }
    }
    public virtual void Update(float deltaTime)
    {
        foreach (var gameObject in _gameObjects)
        {
            gameObject.Update(deltaTime);
        }
    }

    public virtual void Render()
    {
        Console.Clear(); // 화면 초기화

        foreach (var gameObject in _gameObjects)
        {
            Transform transform = gameObject.GetComponent<Transform>();
            Vector2 pos = transform.Position;
            
            Console.SetCursorPosition(pos.X, pos.Y);
            gameObject.Render();
        }
    }
}
