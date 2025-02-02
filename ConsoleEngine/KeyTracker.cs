using System.Collections.Concurrent;

public static class KeyTracker
{
    private static readonly ConcurrentDictionary<ConsoleKey, bool> KeyStates = new(); // 키 상태 저장 (true: 눌림)

    public static bool IsKeyDown(ConsoleKey key) =>
        KeyStates.ContainsKey(key) && KeyStates[key];

    public static void SetKeyDown(ConsoleKey key)
    {
        KeyStates[key] = true; // 눌림 상태 설정
    }

    public static void SetKeyUp(ConsoleKey key)
    {
        KeyStates[key] = false; // 떼어진 상태 설정
    }
}