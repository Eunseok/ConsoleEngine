using Core.Scenes;
using Core.Input;

namespace Core;

// Game: 게임 전체 관리
public class Game
{
    private const int TargetFps = 30;

    public Game()
    {
        InputManager.Initialize();

        SceneManager.SetActiveScene("TestScene");
        SceneManager.Initialize();
    }

    public void Run()
    {
        bool isRunning = true;
        int frameTime = 1000 / TargetFps;

        float deltaTime = 0.0f;
        DateTime _previousTime = DateTime.Now;
        // 게임 메인루프
        while (isRunning)
        {
            // 현재 시간 가져오기
            var start = DateTime.Now;

            // DeltaTime 계산
            TimeSpan delta = start - _previousTime; // 이전 프레임과 현재 프레임 간 시간 차이를 계산
            deltaTime = (float)delta.TotalMilliseconds / 1000.0f; // 밀리초를 초 단위로 변환
            _previousTime = start; // 현재 시간을 다음 프레임의 이전 시간으로 갱신

            // 입력 + 게임 로직 + 렌더링 처리
            InputManager.Update();
            SceneManager.Update(deltaTime);
            SceneManager.Render();

            // FPS 제한
            int elapsedTime = (int)(DateTime.Now - start).TotalMilliseconds;
            int sleepTime = frameTime - elapsedTime;
            if (sleepTime > 0)
            {
                Thread.Sleep(sleepTime);
            }
        }
    }
}