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
    public static GameManager Instance { get; private set; }

    public string PlayerName { get; private set; } = "";
    public string PlayerJob { get; private set; } = "";

    public State CurrentState { get; set; } = State.Default;

    public BoxObject? Menu;
    public PlayerScript? Player;

    private string[] dungeonDiff = new string[] { "쉬운 던전", "일반 던전", "어려운 던전" };
    int[] dungeonDef = { 5, 11, 17 }; //요구 방어력
    int[] reward = new int[] { 1000, 1700, 2500 }; //gold보상

    public String[] DungeonDiff => dungeonDiff;
    public int[] DungeonDef => dungeonDef;
    public int[] Reward => reward;

    public GameManager()
    {
    }

    public override void Initialize()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Owner); // 씬 변경해도 유지됨
            Signal();
        }
        else
        {
            Destroy(Owner); // 기존 인스턴스가 있으면 삭제
        }
    }

    public void Signal()
    {
        Owner?.RegisterEventHandler("CloseMenu", _ => SetState(State.Default));
        Owner?.RegisterEventHandler("ShowMenu", _ => SetState(State.Menu));
        //Owner?.RegisterEventHandler("Save", _ => SetState(State.Save));;
        Owner?.RegisterEventHandler("Already", _ =>
            CreateInfoMessage("이미 보유한 장비입니다.", ConsoleColor.Red));
        ;
        Owner?.RegisterEventHandler("Already", _ =>
            CreateInfoMessage("이미 보유한 장비입니다.", ConsoleColor.Red));
        ;
        Owner?.RegisterEventHandler("LessMoney", _ =>
            CreateInfoMessage("골드가 부족합니다.", ConsoleColor.Red));
        ;
        Owner?.RegisterEventHandler("BuySuccess", _ =>
            CreateInfoMessage("구매에 성공했습니다.", ConsoleColor.Blue));
        ;
        Owner?.RegisterEventHandler("SellSuccess", _ =>
            CreateInfoMessage("판매에 성공했습니다.", ConsoleColor.Blue));
        ;
        Owner?.RegisterEventHandler("RestingSuccess", _ =>
            CreateInfoMessage("체력을 전부 회복했습니다.", ConsoleColor.Green)); 
        Owner?.RegisterEventHandler("Health", _ =>
            CreateInfoMessage("휴식이 필요합니다.", ConsoleColor.Yellow));
     
        ;
    }

    public void SetState(State state)
    {
        CurrentState = state;
    }

    protected override void OnUpdate(float deltaTime)
    {
        CurrentState = State.Default;
        switch (CurrentState)
        {
            case State.Default:
                DefaltUpdate(deltaTime);
                break;
            case State.Menu:
               // MenuUpdate(deltaTime);
                break;
        }
    }

    public void DefaltUpdate(float deltaTime)
    {
        if (InputManager.GetKeyDown("Menu"))
        {
            Instance.Menu?.SetActive(!Instance.Menu.IsActive());
            if (Instance.Menu?.IsActive() == true)
            {
                Owner.BroadcastEvent("ShowMenu");
                Game.CursorPosition = Instance.Menu?.GetChild()[0]?.GlobalPosition ?? Vector2<int>.Zero();
                CurrentState = State.Menu;
            }
            else
            {
                GameManager.Instance.Owner.BroadcastEvent("PlayerCanMove");
                GameManager.Instance.Owner.BroadcastEvent("CloseMenu");
            }
        }
    }

    public void MenuUpdate(float deltaTime)
    {
        if (InputManager.GetKeyDown("Menu"))
        {
            if (Instance.Menu?.IsActive() != true) return;

            Instance.Menu?.SetActive(false);
            Owner.BroadcastEvent("CloseMenu");
            GameManager.Instance.Owner.BroadcastEvent("PlayerCanMove");
        }
    }

    public void CreateInfoMessage(string message, ConsoleColor color)
    {
        var box = Instantiate<BoxObject>(Game.ConsoleCenter);
        box.SetSize(30, 5);
        box.SetOrder(999);
        var obj = Instantiate<LabelObject>(box);
        obj.SetText(message, color);

        Destroy(box, 1.5f);
    }

    public override void OnMessageReceived(string eventKey, object data)
    {
        switch (eventKey)
        {
            case "Title":
                switch (data)
                {
                    case "이어하기":
                        Player = LoadManager.LoadPlayerData();
                        SceneManager.SetActiveScene("MainScene");
                        break;
                    case "새로하기":
                        Player = new PlayerScript();
                        SceneManager.SetActiveScene("CreationScene");
                        break;
                    case "나가기":
                        Environment.Exit(0);
                        break;
                }

                break;
        }
    }
}