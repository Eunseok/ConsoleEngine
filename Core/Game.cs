using Core.Scenes;

namespace Core;

// Game: 게임 전체 관리
public class Game
{
    public Game()
    {
        SceneManager.SetActiveScene("TestScene");
        SceneManager.Initialize();
    }
    public void Run()
    {
        bool isRunning = true;

        //게임 메인루프
        while (isRunning)  
        {
            SceneManager.Update();
            SceneManager.Render();
            
            Thread.Sleep(100); // 간단히 100ms 대기 (프레임 속도 조정)

            // ESC 키로 게임 종료
            if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
            {
                isRunning = false;
            }
        }
    }
    
}