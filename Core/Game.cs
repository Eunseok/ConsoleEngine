// using Core.Managers;
//
// namespace Core;
//
// // Game: 게임 전체 관리
// public class Game
// {
//     public Game()
//     {
//         SceneManager.SetActiveScene("TestScene");
//         SceneManager.Initialize();
//     }
//     public void Run()
//     {
//         bool isRunning = true;
//
//         Console.WriteLine("🎮 InputManager 비동기 테스트 시작!");
//         InputManager.StartListening();
//
//         //게임 메인루프
//         while (isRunning)  
//         {
//             InputManager.Update(); // 🔹 매 프레임 상태 업데이트
//
//             
//             if (InputManager.GetKeyDown("Jump"))
//                 Console.WriteLine("🟢 Spacebar 키가 눌렸습니다!");
//
//             if (InputManager.GetKey("Jump"))
//                 Console.WriteLine("🔵 Spacebar 키가 계속 눌려있습니다 (Held)");
//
//             if (InputManager.GetKeyUp("Jump"))
//                 Console.WriteLine("🔴 Spacebar 키가 떼어졌습니다!");
//             
//             // ESC 키로 게임 종료
//             if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
//             {
//                 isRunning = false;
//                 InputManager.StopListening();
//                 break;
//             }
//             
//             //SceneManager.Update();  // 활성씬 업데이트
//             //SceneManager.Render();  // 활성씬 랜더
//             
//             Thread.Sleep(100); // 프레임 속도 조정
//         }
//     }
//     
// }