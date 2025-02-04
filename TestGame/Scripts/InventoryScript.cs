
using Core.Components;
using Core.Input;
using Core.MyMath;
using Core;
using TestGame.Singletons;

namespace TestGame.Scripts;

public class InventoryScript : Script
{
    private int _menuIndex = 0;

    public void InitMenuIndex()
    {
        _menuIndex =  Owner?.GetChild().Count - 1 ?? 0;
    }
    
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
    
}