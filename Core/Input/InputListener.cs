using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Input;

public static class InputListener
{
    // 키 상태를 저장하는 Dictionary (누르면 true, 떼면 false)
    private static readonly ConcurrentDictionary<ConsoleKey, bool> KeyStates = new();
    private static bool IsRunning { get; set; }

    /// <summary>
    /// 특정 키가 현재 눌려 있는지 확인
    /// </summary>
    public static bool IsKeyPressed(ConsoleKey key)
    {
        return KeyStates.TryGetValue(key, out var isPressed) && isPressed;
    }

    /// <summary>
    /// 키 입력 추적을 비동기로 종료
    /// </summary>
    public static void StopKeyTracker() => IsRunning = false;

    /// <summary>
    /// 키 입력을 추적하며 키 상태를 관리합니다 (비동기)
    /// </summary>
    public static Task StartKeyTrackerAsync(ConsoleKey exitKey = ConsoleKey.Q, CancellationToken? token = null)
    {
        IsRunning = true;

        return Task.Run(async () =>
        {
            while (IsRunning && (token == null || !token.Value.IsCancellationRequested))
            {
                if (Console.KeyAvailable)
                {
                    var keyInfo = Console.ReadKey(intercept: true);
                    HandleKeyState(keyInfo.Key, true); // 키 눌림 처리

                    // 10ms 후 키를 'Released'로 업데이트
                    _ = Task.Run(async () =>
                    {
                        await Task.Delay(30);
                        HandleKeyState(keyInfo.Key, false); // 키 떼어짐 처리
                    });

                    // Exit 키 처리
                    if (keyInfo.Key == exitKey)
                    {
                        Console.WriteLine("Exiting Key Tracker...");
                        StopKeyTracker();
                        break;
                    }
                }

                await Task.Delay(10); // 루프 주기 대기
            }
        }, token ?? CancellationToken.None);
    }

    /// <summary>
    /// 키 상태를 처리하며 KeyStates를 업데이트
    /// </summary>
    /// <param name="key">키</param>
    /// <param name="isPressed">키 상태 (눌림 또는 떼어짐)</param>
    private static void HandleKeyState(ConsoleKey key, bool isPressed)
    {
        KeyStates[key] = isPressed;
    }
}