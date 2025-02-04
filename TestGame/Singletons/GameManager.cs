using System.Drawing;
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
       //Owner?.RegisterEventHandler("Save", _ => SetState(State.Save));;
       Owner?.RegisterEventHandler("Already", _ => 
           CreateInfoMessage("이미 보유한 장비입니다.",ConsoleColor.Red) );;
       Owner?.RegisterEventHandler("Already", _ => 
           CreateInfoMessage("이미 보유한 장비입니다.",ConsoleColor.Red) );;
       Owner?.RegisterEventHandler("LessMoney", _ => 
           CreateInfoMessage("골드가 부족합니다.",ConsoleColor.Red) );;
       Owner?.RegisterEventHandler("BuySuccess", _ => 
           CreateInfoMessage("구매에 성공했습니다.",ConsoleColor.Blue) );;
       Owner?.RegisterEventHandler("SellSuccess", _ => 
           CreateInfoMessage("판매에 성공했습니다.",ConsoleColor.Blue) );;
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

    public void CreateInfoMessage(string message, ConsoleColor color)
    {
        var box = Instantiate<BoxObject>(Game.ConsoleCenter);
        box.SetSize(30,5);
        box.SetOrder(999);
        var obj = Instantiate<LabelObject>(box);
        obj.SetText(message, color);
        
        Destroy(box, 1.5f);
        ObjectSort();
    }
}