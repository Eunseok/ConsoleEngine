using Core.Components;
using Core.Input;
using Core.MyMath;
using Core;
using TestGame.Singletons;

namespace TestGame.Scripts;

public class MenuScript : Script
{
    private int _menuIndex = 0;
    
    protected override void OnUpdate(float deltaTime)
    {
        
        int max = Owner?.GetChild().Count - 1 ?? 0;
        if (InputManager.GetKey("UpArrow"))
        {
            _menuIndex--;
            if(_menuIndex < 0)
                _menuIndex = max;
        }
        if (InputManager.GetKey("DownArrow"))
        {
            _menuIndex++;
            if(_menuIndex > max)
                _menuIndex = 0;
        }
        
        Vector2<int> pos = Owner?.GetChild()[_menuIndex]?.GlobalPosition ?? Vector2<int>.Zero();
        Game.CursorPosition = pos;
    }

    public override void OnMessageReceived(string eventKey, object data)
    {
        if (eventKey != "Menu") return;
        switch (data)
        {
            case "상태보기":
                GameManager.Instance.Owner.BroadcastEvent("ShowStatus");
                break;
            case "인벤토리":
                GameManager.Instance.Owner.BroadcastEvent("ShowInventory");
                break;
            case "휴식하기":
                GameManager.Instance.Owner.BroadcastEvent("StartRest");
                break;
            case "저장/종료":
                GameManager.Instance.Owner.BroadcastEvent("Save");
                break;
            case "닫기":
                GameManager.Instance.Owner.BroadcastEvent("CloseMenu");
            
                break;
            default:
                break;
        }
        Owner.SetActive(false);
    }
}