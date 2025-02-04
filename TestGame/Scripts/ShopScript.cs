using Core.Components;
using Core.Objects;
using Core.Input;
using Core.MyMath;
using Core;
using TestGame.Singletons;

namespace TestGame.Scripts;

public class ShopScript : Script
{
    enum State
    {
        Default,
        Buying,
        Selling
    }
    
    private State _state = State.Default;
    private int _menuIndex = 0;
    protected override void OnUpdate(float deltaTime)
    {
        switch (_state)
        {
            case State.Default:
                DefaultUpdate();
                break;
            case State.Buying:
            case State.Selling:
                BuyingUpdate();
                break;
        }
    }

    private void DefaultUpdate()
    {
        List<GameObject> btns = Owner?.GetChild()?.FindAll(c => c is ButtonObject) ?? new  ();
        int max = btns?.Count-1 ?? 0;
        if (InputManager.GetKey("LeftArrow"))
        {
            _menuIndex--;
            if(_menuIndex < 0)
                _menuIndex = max;
        }
        if (InputManager.GetKey("RightArrow"))
        {
            _menuIndex++;
            if(_menuIndex > max)
                _menuIndex = 0;
        }
        
        Vector2<int> pos = btns?[_menuIndex]?.GlobalPosition ?? Vector2<int>.Zero();
        Game.CursorPosition = pos;
    }
    
    private void BuyingUpdate()
    {
        List<GameObject> btns = Owner?.GetChild()?.FindAll(c => c is ButtonObject) ?? new  ();
        int max = btns?.Count-1 ?? 0;
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
        
        Vector2<int> pos = btns?[_menuIndex]?.GlobalPosition ?? Vector2<int>.Zero();
        Game.CursorPosition = pos;
    }
    public override void OnMessageReceived(string eventKey, object data)
    {
        if (eventKey != "Shop") return;
        switch (data)
        {
            case "구매":
                _state = State.Buying;
                GameManager.Instance.Owner.BroadcastEvent("Buying");
                break;
            case "판매":
                _state = State.Selling;
                GameManager.Instance.Owner.BroadcastEvent("Selling");
                break;
            case "나가기":
                _state = State.Default;
               GameManager.Instance.Owner.BroadcastEvent("CloseMenu");
                break;
        }
    }
    
}