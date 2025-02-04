using Core;
using Core.Graphics;
using Core.Scenes;
using Core.Components;
using Core.MyMath;
using Core.Objects;
using TestGame.Scripts;

namespace TestGame.Scenes;

public class CreationScene : Scene
{
    public CreationScene() : base("CreationScene")
    {

    }

    public override void Initialize()
    {
        base.Initialize();
        //GameObject obj = new Text("스파르타 던전에 오신 여러분 환영합니다", ConsoleColor.Yellow);
        //AddObject(obj, Game.ConsoleCenter);
        //obj.AddComponent<TextScript>();
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime); //Scene 기본 로직 호출
        
        
        //추가 로직 구현
    }

    // public override void Render()
    // {
    //     base.Render(); //Scene 기본 로직 호출
    //     //추가 로직 구현
    // }
}