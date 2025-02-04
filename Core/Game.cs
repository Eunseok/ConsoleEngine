using Core.Scenes;
using Core.Input;
using Core.MyMath;

namespace Core;

// Game: 게임 전체 관리
public class Game
{
    private const int TargetFps = 30;
    public static Vector2<int> ConsoleCenter = new Vector2<int>(Console.WindowWidth/2-1, Console.WindowHeight/2-1);
    public static Vector2<int> CursorPosition = ConsoleCenter;
    public Game()
    {
        InputManager.Initialize();

        SceneManager.SetActiveScene("MainScene");
    }

    public void Run()
    {
        bool isRunning = true;
        int frameTime = 1000 / TargetFps;

        double deltaTime = 0.0; // double로 변경하여 정밀도 향상
        DateTime _previousTime = DateTime.Now;
        
 
        // 게임 메인 루프
        while (isRunning)
        {
            // 현재 시간 가져오기
            DateTime start = DateTime.Now;

            // DeltaTime 계산
            deltaTime = (start - _previousTime).TotalSeconds; // 밀리초 대신 초 단위로 바로 계산
            _previousTime = start; // 이전 시간 갱신

            // 입력 + 게임 로직 + 렌더링 처리
            InputManager.Update();
            SceneManager.Update((float)deltaTime);

            // FPS 제한
            int elapsedTime = (int)(DateTime.Now - start).TotalMilliseconds;
            int sleepTime = frameTime - elapsedTime;
            if (sleepTime > 0)
            {
                Thread.Sleep(sleepTime); // 남은 시간 동안 대기
            }
        }
    }
}