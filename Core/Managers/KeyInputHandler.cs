using System.Collections.Generic;
using System.Threading.Tasks;

public static class KeyInputHandler
{
    private static readonly HashSet<ConsoleKey> ActiveKeys = new(); // 현재 눌려 있는 키
    private static bool _isRunning = false;

    public static void Start()
    {
        if (_isRunning) return; // 이미 실행 상태라면 작업 종료

        _isRunning = true;

        Task.Run(async () =>
        {
            while (_isRunning)
            {
                // 키 감지 루프
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(intercept: true).Key;

                    // 새로운 키일 경우 키 입력 처리
                    if (!ActiveKeys.Contains(key))
                    {
                        ActiveKeys.Add(key); // 키 활성화 집합에 추가
                        InputManager.KeyPressed(key); // 신규 키 입력 처리
                    }
                }

                // 활성화된 키 상태 업데이트
                foreach (var key in new List<ConsoleKey>(ActiveKeys))
                {
                    if (!Console.KeyAvailable) // 키를 떼었는지 확인
                    {
                        InputManager.KeyReleased(key); // 릴리즈 처리
                        ActiveKeys.Remove(key); // 활성화 키에서 제거
                    }
                }

                await Task.Delay(10); // 비동기 대기 (지연 시간 축소 가능)
            }
        });
    }

    public static void Stop()
    {
        _isRunning = false; // 루프 종료
    }
}