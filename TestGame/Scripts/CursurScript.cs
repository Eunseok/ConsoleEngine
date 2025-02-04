
using Core.Components;
using Core.Input;
using Core.MyMath;
using Core;
using Core.Objects;
using TestGame.Singletons;

namespace TestGame.Scripts;

public class CursurScript : Script
{
    private int _menuIndex = 0;
    
    
    protected override void OnUpdate(float deltaTime)
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
    
}