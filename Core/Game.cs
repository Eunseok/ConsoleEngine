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
            InputManager.Update(); // 입력 업데이트
            
            SceneManager.Update();  // 활성씬 업데이트
            SceneManager.Render();  // 활성씬 랜더
            
            // ESC 키로 게임 종료
             if (InputManager.GetKey("Escape"))
             {
                 isRunning = false;
                 break;
             }
            
            Thread.Sleep(60); // 프레임 속도 조정
        }
    }
    
}