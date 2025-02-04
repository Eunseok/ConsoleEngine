// using Core;
// using Core.Components;
// using Core.Input;
// using Core.MyMath;
// using Core.Objects;
// using Core.Scenes;
// using TestGame.Scripts;
// using static Core.Objects.Object;
//
// namespace TestGame.Singletons;
//
// public class GameManager : Script
// {
//     public enum State
//     {
//         Default,
//         Menu,
//         Status,
//         Inventory,
//         Rest,
//         Save,
//         Exit
//     }
//     
//     // 싱글톤 인스턴스
//     private static GameManager? _instance;
//     public static GameManager Instance => _instance ??= new GameManager();
//     
//     public State CurrentState { get; set; } = State.Default;
//
//     public BoxObject? Menu;
//     
//     private int _menuIndex = 0;
//
//     public GameManager()
//     {
//
//     }
//     public override void Initialize()
//     {
//        
//     }
//
//     protected override void OnUpdate(float deltaTime)
//     {
//         switch (CurrentState)
//         {
//             case State.Default:
//                 DefaltUpdate(deltaTime);
//                 break;
//             case State.Menu:
//                 MenuUpdate(deltaTime);
//                 break;
//             case State.Status:
//                 StatusUpdate(deltaTime);
//                 break;
//             case State.Inventory:
//                 InventoryUpdate(deltaTime);
//                 break;
//             case State.Rest:
//                 RestUpdate(deltaTime);
//                 break;
//             case State.Save:
//                 SaveUpdate(deltaTime);
//                 break;
//             case State.Exit:
//                 ExitUpdate(deltaTime);
//                 break;
//         }
//     }
//
//     public void DefaltUpdate(float deltaTime)
//     {
//         if (InputManager.GetKey("Menu"))
//         {
//             Instance.Menu?.SetActive(!Instance.Menu.IsActive());
//             if (Instance.Menu?.IsActive() == true)
//             {
//                 Game.CursorPosition = Instance.Menu?.GetChild()[0]?.GlobalPosition ?? Vector2<int>.Zero();
//                 CurrentState = State.Menu;
//             }
//         }
//
//     }
//     public void MenuUpdate(float deltaTime)
//     {
//         if (InputManager.GetKey("Menu"))
//         {
//             Instance.Menu?.SetActive(false);
//             CurrentState = State.Default;
//         }
//
//         if (InputManager.GetKey("UpArrow"))
//         {
//             _menuIndex--;
//         }
//         if (InputManager.GetKey("DownArrow"))
//         {
//             _menuIndex++;
//         }
//         _menuIndex = Math.Clamp(_menuIndex, 0, Instance.Menu?.GetChild().Count -1 ?? 0);
//         Vector2<int> pos = Instance.Menu?.GetChild()[_menuIndex]?.GlobalPosition ?? Vector2<int>.Zero();
//         Game.CursorPosition = pos;
//     }
//     
//     public void StatusUpdate(float deltaTime)
//     {
//         
//     }
//
//     public void InventoryUpdate(float deltaTime)
//     {
//     }
//
//     public void RestUpdate(float deltaTime)
//     {
//         
//     }
//
//     public void SaveUpdate(float deltaTime)
//     {
//         
//     }
//
//     public void ExitUpdate(float deltaTime)
//     {
//         
//     }
//     private void Input()
//     {
//
//     }
// }