using Core.Objects;
using Core.Scenes;

namespace Core.Scenes;

// SceneManager: 여러 Scene을 관리하고 전환
public static class SceneManager
{
    //public static readonly SceneManager Instance = new SceneManager();
    
    private static readonly Dictionary<string, Scene> _scenes = new Dictionary<string, Scene>();
    private static Scene? _currentScene;
    
    public static Scene CurrentScene => _currentScene!;
    
    // 제네릭 메서드 사용하여 씬 생성
    public static T CreateScene<T>() where T : Scene, new()
    {
        T scene = new T(); // 인스턴스 생성
        if(!_scenes.TryAdd(scene.Name, scene)) // 씬 추가 시도
            return (T)_scenes[scene.Name]; // 기존 씬 반환

        return scene;   // 생성한 씬 반환
    }

    public static void SetActiveScene(string name)
    {
        //  기존 씬의 DontDestroy 오브젝트를 임시 저장
        HashSet<GameObject> tempDontDestroyObjects = _currentScene?.DontDestroyObjects ?? new HashSet<GameObject>();

        // 기존 씬 정리
        _currentScene?.ClearObject();

        // 새로운 씬 설정
        if (!_scenes.TryGetValue(name, out _currentScene))
        {
            Console.WriteLine($"Scene '{name}'이(가) 존재하지 않습니다.");
            return;
        }

        //  새로운 씬으로 DontDestroy 오브젝트 이동
        foreach (var obj in tempDontDestroyObjects)
        {
            _currentScene.DontDestroyOnLoad(obj);  // 새로운 씬에서도 유지되도록 등록
            _currentScene.AddObject(obj);  // 씬에 오브젝트 추가
        }

        _currentScene.Initialize(); // 씬 초기화
    }
    
    public static void Update(float deltaTime)
    {
        _currentScene?.Update(deltaTime);
    }

    // public static void Render()
    // {
    //     _currentScene?.Render();
    // }
}