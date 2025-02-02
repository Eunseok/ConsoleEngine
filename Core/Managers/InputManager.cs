using System.Collections.Concurrent;

public enum KeyState
{
    None, // 기본 상태
    Pressed, // 처음 눌렀을 때
    Held, // 키를 계속 누르고 있는 상태
    Released // 키를 뗐을 때
}

public static class InputManager
{
    private static readonly ConcurrentDictionary<ConsoleKey, KeyState> KeyStates = new();

    public static void KeyPressed(ConsoleKey key)
    {
        // 누른 키가 "None"이거나 "Released" 상태였던 경우
        if (!KeyStates.ContainsKey(key) || KeyStates[key] == KeyState.None || KeyStates[key] == KeyState.Released)
        {
            KeyStates[key] = KeyState.Pressed; // Pressed로 전환
        }
    }

    public static void KeyReleased(ConsoleKey key)
    {
        // 누르고 있던 키를 Released 상태로 전환
        if (KeyStates.ContainsKey(key))
        {
            KeyStates[key] = KeyState.Released;
        }
    }

    public static void UpdateStates()
    {
        foreach (var key in KeyStates.Keys)
        {
            if (KeyStates[key] == KeyState.Pressed)
            {
                KeyStates[key] = KeyState.Held; // Pressed 상태는 Held로 전환
            }
            else if (KeyStates[key] == KeyState.Released)
            {
                KeyStates[key] = KeyState.None; // Released는 None으로 초기화
            }
        }
    }

    public static KeyState GetKeyState(ConsoleKey key)
    {
        // 해당 키 상태 반환 (없으면 기본값 None)
        return KeyStates.ContainsKey(key) ? KeyStates[key] : KeyState.None;
    }
}