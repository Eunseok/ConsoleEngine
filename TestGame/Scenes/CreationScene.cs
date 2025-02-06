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
    GameObject creationObject;
    public CreationScene() : base("CreationScene")
    {
    }

    public override void Initialize()
    {
         creationObject = Instantiate<GameObject>(Vector2<int>.Zero());
         creationObject.AddComponent<CreationScript>();
    }


    public override void Update(float deltaTime)
    {
        base.Update(deltaTime); // Scene 기본 로직 호출
        // 추가 로직 구현

        if (creationObject.GetComponent<CreationScript>()?.IsInputEnabled ?? false)
        {
            Console.CursorVisible = true;
           
            string? name = "";
            while (string.IsNullOrEmpty(name))
            {
                Console.SetCursorPosition(Game.ConsoleCenter.X - 4, Game.ConsoleCenter.Y);
                name = Console.ReadLine().Replace(" ", "");
            }
            creationObject.GetComponent<CreationScript>()?.SetPlayerName(name);
            Console.CursorVisible = false;
        }
    }
}