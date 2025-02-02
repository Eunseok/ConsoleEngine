using System;
using System.Collections.Concurrent;
using System.Threading;

class Program
{
    // 키 상태를 추적하는 Dictionary
    private static readonly ConcurrentDictionary<ConsoleKey, DateTime> KeyTimeouts = new();

    // 타임아웃 시간 (키를 감지하는 간격)
    private static readonly TimeSpan Timeout = TimeSpan.FromMilliseconds(200);

    static void Main()
    {
        Console.WriteLine("Start input test with timeout-based key management. Press 'Q' to quit.");

        while (true)
        {
            // 키 입력 감지
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(intercept: true).Key;

                if (!KeyTimeouts.ContainsKey(key))
                {
                    // 처음 눌린 키는 "Pressed"로 처리
                    Console.WriteLine($"Key {key} Pressed");
                }
                else
                {
                    // 같은 키가 다시 눌린 경우 "Held" 처리
                    Console.WriteLine($"Key {key} Held");
                }

                // 현재 시간으로 타임아웃 갱신
                KeyTimeouts[key] = DateTime.Now;
            }

            // 타임아웃 처리 (키 릴리즈 상태 감지)
            foreach (var key in KeyTimeouts.Keys)
            {
                // 특정 키의 타임아웃이 지났다면 "Released" 처리
                if (DateTime.Now - KeyTimeouts[key] > Timeout)
                {
                    Console.WriteLine($"Key {key} Released");

                    // Released 상태를 처리 후 제거
                    KeyTimeouts.TryRemove(key, out _);
                }
            }

            // Q 키가 감지되면 종료
            if (KeyTimeouts.ContainsKey(ConsoleKey.Q))
            {
                Console.WriteLine("Exiting program...");
                break;
            }

            Thread.Sleep(50); // CPU 사용량 절감을 위한 짧은 대기
        }
    }
}