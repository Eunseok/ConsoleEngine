using Core.Scenes;

namespace Core.Scenes;

// SceneManager: 여러 Scene을 관리하고 전환
public static class SceneManager
{
    //public static readonly SceneManager Instance = new SceneManager();
    
    private static readonly Dictionary<string, Scene> _scenes = new Dictionary<string, Scene>();
    private static Scene? _currentScene;
    
    
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
        if (!_scenes.TryGetValue(name, out _currentScene))
        {
            Console.WriteLine($"Scene '{name}'이(가) 존재하지 않습니다.");
        }
    }

    public static void Initialize()
    {
        _currentScene?.Initialize();
    }
    
    public static void Update(float deltaTime)
    {
        _currentScene?.Update(deltaTime);
    }

    public static void Render()
    {
        _currentScene?.Render();
    }
}