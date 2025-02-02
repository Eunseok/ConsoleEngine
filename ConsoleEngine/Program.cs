using Core;
using Core.Input;
using TestGame;

namespace ConsoleEngine;

class Program
{
    static async Task Main(string[] args) // Main을 비동기로 변경
    {
        Console.CursorVisible = false;

        // KeyTracker 비동기 시작
        var keyTrackerTask = InputListener.StartKeyTrackerAsync(ConsoleKey.Q);

        // 씬 로드
        LoadManager.LoadScenes();

        // 게임 초기화
        Game game = new Game();
        
        InputListener.StartKeyTrackerAsync();

        game.Run(); // 게임 실행

        // 게임 종료
        Console.WriteLine("게임 종료!");
        InputListener.StopKeyTracker();

        // KeyTracker 종료 대기
        await keyTrackerTask;
    }
}