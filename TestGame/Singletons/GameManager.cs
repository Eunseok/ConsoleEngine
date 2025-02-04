using Core;
using Core.Components;
using Core.Input;
using Core.MyMath;
using Core.Objects;
using Core.Scenes;
using TestGame.Scripts;
using static Core.Objects.Object;

namespace TestGame.Singletons;

public class GameManager : Script
{
    public enum State
    {
        Default,
        Menu,
        Status,
        Inventory,
        Rest,
        Save,
        Exit
    }
    
    // 싱글톤 인스턴스
    private static GameManager? _instance;
    public static GameManager Instance => _instance ??= new GameManager();
    
    public State CurrentState { get; set; } = State.Default;

    public BoxObject? Menu;
    public PlayerScript? Player;
    


    public GameManager()
    {

    }
    public override void Initialize()
    {
       Owner?.RegisterEventHandler("CloseMenu", _ => SetState(State.Default));
       Owner?.RegisterEventHandler("ShowMenu", _ => SetState(State.Menu));
       Owner?.RegisterEventHandler("ShowStatus", _ => SetState(State.Status));
       Owner?.RegisterEventHandler("ShowInventory", _ => SetState(State.Inventory));
       Owner?.RegisterEventHandler("StartRest", _ => SetState(State.Rest));
       Owner?.RegisterEventHandler("Save", _ => SetState(State.Save));;
    }

    public void SetState(State state)
    {
        CurrentState = state;
    }
    protected override void OnUpdate(float deltaTime)
    {
        switch (CurrentState)
        {
            case State.Default:
                DefaltUpdate(deltaTime);
                break;
            case State.Menu:
                MenuUpdate(deltaTime);
                break;
            case State.Status:
                StatusUpdate(deltaTime);
                break;
            case State.Inventory:
                InventoryUpdate(deltaTime);
                break;
            case State.Rest:
                RestUpdate(deltaTime);
                break;
            case State.Save:
                SaveUpdate(deltaTime);
                break;
            case State.Exit:
                ExitUpdate(deltaTime);
                break;
        }
    }

    public void DefaltUpdate(float deltaTime)
    {
        if (InputManager.GetKey("Menu"))
        {
            Instance.Menu?.SetActive(!Instance.Menu.IsActive());
            if (Instance.Menu?.IsActive() == true)
            {
                Owner.BroadcastEvent( "ShowMenu");
                Game.CursorPosition = Instance.Menu?.GetChild()[0]?.GlobalPosition ?? Vector2<int>.Zero();
                CurrentState = State.Menu;
            }
            else
            {
                GameManager.Instance.Owner.BroadcastEvent("CloseMenu");
            }
        }

    }

    public void MenuUpdate(float deltaTime)
    {
        if (InputManager.GetKey("Menu"))
        {
            Instance.Menu?.SetActive(false);
            Owner.BroadcastEvent("CloseMenu");
        }
    }

    public void StatusUpdate(float deltaTime)
    {
        
    }

    public void InventoryUpdate(float deltaTime)
    {
    }

    public void RestUpdate(float deltaTime)
    {
        
    }

    public void SaveUpdate(float deltaTime)
    {
        
    }

    public void ExitUpdate(float deltaTime)
    {
        
    }
    private void Input()
    {

    }
}