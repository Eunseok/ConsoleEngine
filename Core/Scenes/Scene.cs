using Core.Components;
using Core.Math;
using Core.Objects;
using Core.Input;
    
namespace Core.Scenes;

// Scene: 게임의 상태(레벨, 화면)를 소유
public class Scene : Entity
{
    private readonly List<GameObject> _gameObjects;
    
    private Vector2<int> curPos = new Vector2<int>(10, 1);
    
    public Scene(string name) : base(name) 
    {
        _gameObjects = new List<GameObject>();
    }

    public void AddObject(GameObject obj)
    {
        obj.Initialize();
        _gameObjects.Add(obj);
        
    }
    public void AddObject(GameObject obj, Vector2<int> pos)
    {
        AddObject(obj);
        obj.GetComponent<Transform>().Position = pos;
    }

    public virtual void Initialize()
    {
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
            gameObject.Render();
        }
        
                
        if (InputManager.GetKey("MoveLeft"))
            curPos += Vector2<int>.Left();
        if (InputManager.GetKey("MoveRight"))
            curPos += Vector2<int>.Right();
        if (InputManager.GetKey("MoveUp"))
            curPos += Vector2<int>.Up();
        if (InputManager.GetKey("MoveDown"))
            curPos += Vector2<int>.Down();

        // 커서 위치 제한 (0 이상, 콘솔 창 최대 크기 이하)
        curPos.X = System.Math.Clamp(curPos.X, 0, Console.WindowWidth - 1);
        curPos.Y = System.Math.Clamp(curPos.Y, 0, Console.WindowHeight - 1);
        
        Console.SetCursorPosition(curPos.X, curPos.Y);
        // 커서 위치와 색상 설정
        int cursorX = Console.CursorLeft;
        int cursorY = Console.CursorTop;

        Console.SetCursorPosition(cursorX, cursorY);
        Console.Write("👆"); // 공백 출력
        Console.ResetColor(); // 색상 초기화

        /// 커서를 원래 위치로 복구
        Console.SetCursorPosition(curPos.X, curPos.Y);
    }
}
