using Core;
using Core.Graphics;
using Core.Scenes;
using Core.Components;
using Core.MyMath;
using Core.Objects;
using TestGame.Scripts;
using TestGame.Singletons;
using static Core.Objects.Object;

namespace TestGame.Scenes;

public class CreationScene : Scene
{
    public CreationScene() : base("CreationScene")
    {
    }

    public override void Initialize()
    {
        GameObject gameManager = new GameObject();
        gameManager.AddComponent<CreationManager>();
    }


    public override void Update(float deltaTime)
    {
        base.Update(deltaTime); // Scene 기본 로직 호출
        // 추가 로직 구현

        if (CreationManager.Instance.IsInputEnabled)
        {
      
            Console.CursorVisible = true;
            Console.SetCursorPosition(Game.ConsoleCenter.X - 4, Game.ConsoleCenter.Y);
            CreationManager.Instance.SetPlayerName(Console.ReadLine());
            Console.CursorVisible = false;
        }
    }
}