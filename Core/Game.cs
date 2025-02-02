using Core.Scenes;
using Core.Input;

namespace Core;

// Game: 게임 전체 관리
public class Game
{
    public Game()
    {
        InputManager.Initialize();
        
        SceneManager.SetActiveScene("TestScene");
        SceneManager.Initialize();
    }
    public void Run()
    {
        bool isRunning = true;
        
        //게임 메인루프
        while (isRunning)  
        {
            InputManager.Update(); // 매 프레임 상태 업데이트
            SceneManager.Update();  // 활성씬 업데이트
            
            SceneManager.Render();  // 활성씬 랜더
            
            // ESC 키로 게임 종료
            if (InputListener.IsKeyPressed(ConsoleKey.Escape))
            {
                isRunning = false;
                break;
            }
            
            
            Thread.Sleep(16); // 프레임 속도 조정
        }
    }
    
}