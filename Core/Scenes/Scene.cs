using Core.Input;
using Core.MyMath;
using Core.Objects;

namespace Core.Scenes;

// Scene: 게임의 상태(레벨, 화면)를 소유
public class Scene : Entity
{
    private readonly HashSet<GameObject> _gameObjects = new();
    private readonly HashSet<GameObject> _deleteObjects = new();
    public readonly HashSet<GameObject> DontDestroyObjects = new(); // 삭제되지 않는 오브젝트 리스트

    protected Scene(string name) : base(name)
    {
    }

    // 특정 오브젝트를 씬이 변경되더라도 유지
    public void DontDestroyOnLoad(GameObject obj)
    {
        if (obj == null) return;
        DontDestroyObjects.Add(obj);
    }

    public void ClearObject()
    {
        _gameObjects.RemoveWhere(obj => !DontDestroyObjects.Contains(obj)); // 🔹 조건부 삭제
        _deleteObjects.Clear();
    }

// 게임 오브젝트 추가
    public void AddObject(GameObject? obj)
    {
        if (obj == null) return;
        obj.Initialize();
        _gameObjects.Add(obj);
    }

    // 정렬된 오브젝트 리스트 반환
    public List<GameObject> GetSortedObjects()
    {
        return _gameObjects.OrderBy(obj => obj.Order).ToList();
    }

    // 오브젝트 파괴 예약
    public void DestroyedObject(GameObject? obj, float delay)
    {
        if (obj == null) return;
        obj.LfeTime = delay;
        _deleteObjects.Add(obj);
        foreach (var child in obj.GetChild())
        {
            DestroyedObject(child, delay);
        }
    }

    // public void DestroyedObject(string name, float delay)
    // {
    //     GameObject? obj = _gameObjects.Find(obj => obj.Name == name);
    //     if (obj == null) return;
    //     obj.LfeTime = delay;
    //     _deleteObjects.Add(obj);
    //     foreach (var child in obj.GetChild())
    //     {    
    //         DestroyedObject(child, delay);
    //     }
    // }

    // 초기화 메서드 (상속 가능)
    public virtual void Initialize()
    {
        // 필요 시 자식 클래스에서 구현
    }

    // 매 프레임 업데이트 메서드
    public virtual void Update(float deltaTime)
    {
        Console.Clear(); // 화면 초기화
        UpdateActiveObjectsSafe(deltaTime);
        UpdateDestroyedObjects(deltaTime);
        HandleDestroyedObjects();
    }

    // 활성 오브젝트 업데이트 (안전한 컬렉션 복사를 사용)
    private void UpdateActiveObjectsSafe(float deltaTime)
    {
        // 복사본을 사용하여 컬렉션 수정 방지
        foreach (var obj in GetSortedObjects())
        {
            if (obj?.IsActive() ?? false)
                obj.Update(deltaTime);
        }
    }

    // 파괴 예정 오브젝트의 타이머 업데이트
    private void UpdateDestroyedObjects(float deltaTime)
    {
        foreach (var obj in _deleteObjects)
        {
            obj.DestroyedTimer(deltaTime);
        }
    }

    // 파괴 조건 만족 시 오브젝트 제거
    private void HandleDestroyedObjects()
    {
        // 복사본을 사용하여 컬렉션 수정 방지
        foreach (var obj in _deleteObjects.Where(obj => obj.IsDestroyed && !DontDestroyObjects.Contains(obj)).ToList())
        {
            obj.BroadcastEvent("OnDestroy"); // 삭제 이벤트 발생
            _gameObjects.Remove(obj); // _gameObjects에서 제거
            _deleteObjects.Remove(obj); // _deleteObjects에서 제거
        }
    }
}