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

        public DateTime KeyTimeouts = new();
    }


    private static readonly TimeSpan Timeout = TimeSpan.FromMilliseconds(100); // 키 릴리스 타임아웃 설정

    //  키 상태 저장 (키 이름 -> 입력 상태)
    private static readonly Dictionary<ConsoleKey, KeyInfo> Keys = new();


    // 사용자 입력 매핑 (키 이름 -> 실제 키)
    public static Dictionary<string, ConsoleKey> MappingKeys { get; set; } = new Dictionary<string, ConsoleKey>();

    public static void Initialize()
    {
        CreateKeys();
    }

    private static void CreateKeys()
    {
        foreach (var vKey in MappingKeys.Values)
        {
            Keys.TryAdd(vKey, new KeyInfo());
        }
    }

    private static void HandleKeyPress(KeyInfo keyInfo)
    {
        keyInfo.State = keyInfo.IsPressed ? KeyState.Held : KeyState.Pressed; //State = isPressed ? Held : Pressed;

        // 키 타임아웃 갱신
        keyInfo.KeyTimeouts = DateTime.Now;
    }


    // 릴리스된 키 상태를 처리합니다.
    private static void HandleKeyReleases()
    {
        foreach (var keyInfo in Keys.Values)
        {
            if (!keyInfo.IsPressed) continue;

            // 타임아웃이 지난 키를 "Released" 처리
            // if (DateTime.Now - keyInfo.KeyTimeouts > Timeout)
            {
                keyInfo.State = keyInfo.IsPressed ? KeyState.Released : KeyState.None;
            }
        }
    }

    public static void Update()
    {
        // 키 입력 감지
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(intercept: true).Key;
            if (!MappingKeys.ContainsValue(key)) return; // 맵핑된 키가 아니면 return

            if (Keys.TryGetValue(key, out var keyInfo))
                HandleKeyPress(keyInfo); // 키 눌림 처리
        }
        else
            // 릴리스된 키 처리
            HandleKeyReleases();
    }

    public static bool GetKey(string action) // Held
    {
        if (!MappingKeys.TryGetValue(action, out ConsoleKey key)) return false; // 맵핑 여부 확인
        return Keys.TryGetValue(key, out var keyInfo) && keyInfo.IsPressed;
    }

    public static bool GetKeyDown(string action) // Pressed
    {
        if (!MappingKeys.TryGetValue(action, out ConsoleKey key)) return false; // 맵핑 여부 확인
        return Keys.TryGetValue(key, out var keyInfo) && keyInfo.State == KeyState.Pressed;
    }

    public static bool GetKeyUp(string action) // Release
    {
        if (!MappingKeys.TryGetValue(action, out ConsoleKey key)) return false; // 맵핑 여부 확인
        return Keys.TryGetValue(key, out var keyInfo) && !keyInfo.IsPressed;
    }

    public static void RebindKey(string action, ConsoleKey newKey) // 키 변경 기능 추가
    {
        if (MappingKeys.ContainsKey(action))
            MappingKeys[action] = newKey;
    }
}