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

    // 키 상태 저장 (키 -> 입력 상태)
    private static readonly Dictionary<string, KeyInfo> Keys = new();

    // 사용자 입력 매핑 (액션 -> 키 목록)
    public static Dictionary<string, List<string>> MappingKeys { get; set; } = new();

    public static void Initialize()
    {
        CreateKeys();
    }

    private static void CreateKeys()
    {
        foreach (var action in MappingKeys)
        {
            foreach (var key in action.Value)
            {
                Keys.TryAdd(key, new KeyInfo());
            }
        }
    }

    private static void HandleKeyPress(KeyInfo keyInfo)
    {
        keyInfo.State = keyInfo.IsPressed ? KeyState.Held : KeyState.Pressed;

        // 키 타임아웃 갱신
        keyInfo.KeyTimeouts = DateTime.Now;
    }

    private static void HandleKeyReleases()
    {
        foreach (var keyInfo in Keys.Values)
        {
            if (!keyInfo.IsPressed) continue;

            keyInfo.State = keyInfo.IsPressed ? KeyState.Released : KeyState.None;
        }
    }

    public static void Update()
    {
        // 키 입력 감지
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(intercept: true).Key.ToString(); // 문자열로 키 이름 변환

            if (!Keys.TryGetValue(key, out var keyInfo)) return; // 맵핑된 키가 아니면 return

            HandleKeyPress(keyInfo); // 키 눌림 처리
        }
        else
        {
            HandleKeyReleases(); // 릴리스된 키 처리
        }
    }

    public static bool GetKey(string action) // Held 상태 확인
    {
        if (!MappingKeys.TryGetValue(action, out List<string>? keys)) return false;

        foreach (var key in keys)
        {
            if (Keys.TryGetValue(key, out var keyInfo) && keyInfo.IsPressed)
                return true;
        }

        return false;
    }

    public static bool GetKeyDown(string action) // Pressed 상태 확인
    {
        if (!MappingKeys.TryGetValue(action, out List<string>? keys)) return false;

        foreach (var key in keys)
        {
            if (Keys.TryGetValue(key, out var keyInfo) && keyInfo.State == KeyState.Pressed)
                return true;
        }

        return false;
    }

    public static bool GetKeyUp(string action) // Released 상태 확인
    {
        if (!MappingKeys.TryGetValue(action, out List<string>? keys)) return false;

        foreach (var key in keys)
        {
            if (Keys.TryGetValue(key, out var keyInfo) && keyInfo.State == KeyState.Released)
                return true;
        }

        return false;
    }

    public static void RebindKey(string action, string newKey) // 키 추가
    {
        if (MappingKeys.TryGetValue(action, out var keys))
        {
            if (!keys.Contains(newKey)) keys.Add(newKey);
        }
        else
        {
            MappingKeys[action] = new List<string> { newKey };
        }

        // KeyInfo에도 새로운 키 추가
        if (!Keys.ContainsKey(newKey))
            Keys[newKey] = new KeyInfo();
    }

    public static void UnbindKey(string action, string keyToRemove) // 키 제거
    {
        if (MappingKeys.TryGetValue(action, out var keys))
        {
            keys.Remove(keyToRemove);
            if (keys.Count == 0) MappingKeys.Remove(action);
        }

        // KeyInfo에서도 제거
        if (Keys.ContainsKey(keyToRemove))
            Keys.Remove(keyToRemove);
    }
}