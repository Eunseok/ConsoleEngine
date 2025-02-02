namespace Core.Input;

enum KeyState
{
    Pressed,
    Held,
    Released,
    None,
}

public static class InputManager
{
    class KeyInfo
    {
        public KeyState State { get; set; } = KeyState.None;
        public bool IsPressed => State == KeyState.Pressed || State == KeyState.Held;
        
    }
    

    // 사용자 입력 매핑 (키 이름 -> 실제 키)
    private static readonly Dictionary<string, ConsoleKey> InputMapping = new()
    {
        { "MoveUp", ConsoleKey.W },
        { "MoveDown", ConsoleKey.S },
        { "MoveLeft", ConsoleKey.A },
        { "MoveRight", ConsoleKey.D },
        { "Jump", ConsoleKey.Spacebar },
        { "Attack", ConsoleKey.J }
    };
    
    
    
    //  키 상태 저장 (키 이름 -> 입력 상태)
    private static readonly Dictionary<ConsoleKey, KeyInfo> Keys = new();

    public static void Initialize()
    {
        CreateKeys();
    }
    private static void CreateKeys()
    {
        foreach (var vKey in InputMapping.Values)
        {
            Keys.TryAdd(vKey, new KeyInfo());
        }
    }
    
    public static void Update()
    {
        foreach (var key in Keys)
        {
            KeyInfo keyInfo = key.Value;
            if (InputListener.IsKeyPressed(key.Key)) // Pressed, Held
            {
                keyInfo.State = keyInfo.IsPressed ? KeyState.Held : KeyState.Pressed;        
            }
            else
            {
                keyInfo.State = keyInfo.IsPressed ? KeyState.Released : KeyState.None;
            }
        }
    }

    public static bool GetKey(string action) // Held
    {
        if (!InputMapping.ContainsKey(action)) return false; // 맵핑 여부 확인
        return Keys.TryGetValue(InputMapping[action], out var keyInfo) && keyInfo.State == KeyState.Held;
    }

    public static bool GetKeyDown(string action)// Pressed
    {
        if (!InputMapping.ContainsKey(action)) return false; // 맵핑 여부 확인
        return Keys.TryGetValue(InputMapping[action], out var keyInfo) && keyInfo.IsPressed;
    }

    public static bool GetKeyUp(string action)// Release
    {
        if (!InputMapping.ContainsKey(action)) return false; // 맵핑 여부 확인
        return Keys.TryGetValue(InputMapping[action], out var keyInfo) && keyInfo.State == KeyState.Released;
    }
    
    public static void RebindKey(string action, ConsoleKey newKey) // 키 변경 기능 추가
    {
        if (InputMapping.ContainsKey(action))
            InputMapping[action] = newKey;
    }
}