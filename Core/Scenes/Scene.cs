using Core.Input;
using Core.MyMath;
using Core.Objects;

namespace Core.Scenes;

// Scene: 게임의 상태(레벨, 화면)를 소유
public class Scene : Entity
{
    private readonly List<GameObject> _gameObjects = new();
    private readonly HashSet<GameObject> _deleteObjects = new();

    protected Scene(string name) : base(name)
    {
    }

    // 게임 오브젝트 추가
    public void AddObject(GameObject? obj)
    {
        if (obj == null) return;
        obj.Initialize();
        _gameObjects.Add(obj);
        _gameObjects.Sort((l1, l2) => l1.Order.CompareTo(l2.Order)); // 렌더링 순서에 따라 정렬
    }

    // 오브젝트 파괴 예약
    public void DestroyedObject(GameObject? obj, float delay)
    {
        if (obj == null) return;
        obj.LfeTime = delay;
        _deleteObjects.Add(obj);
    }

    public void DestroyedObject(string name, float delay)
    {
        GameObject? obj = _gameObjects.Find(obj => obj.Name == name);
        if (obj == null) return;
        obj.LfeTime = delay;
        _deleteObjects.Add(obj);
    }

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
        foreach (var obj in _gameObjects.ToList())
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
        foreach (var obj in _deleteObjects.Where(obj => obj.IsDestroyed).ToList())
        {
            obj.BroadcastEvent("OnDestroy"); // 삭제 이벤트 발생
            _gameObjects.Remove(obj); // _gameObjects에서 제거
            _deleteObjects.Remove(obj); // _deleteObjects에서 제거
        }
    }
}