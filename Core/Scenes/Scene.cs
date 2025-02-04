using Core.Components;
using Core.Input;
using Core.MyMath;
using Core.Objects;

namespace Core.Scenes;

// Scene: 게임의 상태(레벨, 화면)를 소유
public class Scene : Entity
{
    private readonly List<GameObject> _gameObjects = new();     

    protected Scene(string name) : base(name)
    {
    }

    // 게임 오브젝트 추가 (위치 미설정)
    public void AddObject(GameObject? obj)
    {
        if (obj == null) return;
        _gameObjects.Add(obj);
        obj.Initialize();
    }

    // 게임 오브젝트 추가 (위치 설정 포함)
    public void AddObject(GameObject? obj, Vector2<int> position)
    {
        if (obj == null) return;
        AddObject(obj);
       
        obj.GetComponent<Transform>()?.SetPosition(position);
    }

    // 초기화 메서드 (상속 가능)
    public virtual void Initialize()
    {
        // foreach (var obj in _gameObjects)
        // {
        //     obj.Initialize();
        // }
    }

    // 매 프레임 업데이트 메서드 (상속 가능)
    public virtual void Update(float deltaTime)
    {
        // 화면 초기화
        Console.Clear();
        foreach (var obj in _gameObjects)
        {
            obj.Update(deltaTime);
        }

        HandleInput();
        RenderCursor(); // 커서 그리기
    }
    

    // 입력 처리 (상속 가능)
    protected virtual void HandleInput()
    {
        if (InputManager.GetKey("LeftArrow"))
            Game.CursorPosition.X -= 1;
        if (InputManager.GetKey("RightArrow"))
            Game.CursorPosition.X += 1;
        if (InputManager.GetKey("UpArrow"))
            Game.CursorPosition.Y -= 1;
        if (InputManager.GetKey("DownArrow"))
            Game.CursorPosition.Y += 1;

        // 커서 위치 제한 (콘솔 창 범위 내로 제한)
        Game.CursorPosition.X = System.Math.Clamp(Game.CursorPosition.X, 0, Console.WindowWidth - 1);
        Game.CursorPosition.Y = System.Math.Clamp(Game.CursorPosition.Y, 0, Console.WindowHeight - 1);
    }

    // 커서 렌더링
    private void RenderCursor()
    {
        Console.SetCursorPosition(Game.CursorPosition.X, Game.CursorPosition.Y-1);
        Console.Write("👆");
        Console.ResetColor(); // 색상 초기화
    }
}