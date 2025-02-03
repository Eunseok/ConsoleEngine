using Core.Graphics;
using Core.Scenes;
using Core.Components;
using Core.Math;
using Core.Objects;
using Core.Input;
using TestGame.Scripts;

namespace TestGame.Scenes;

public class TestScene : Scene
{
    public TestScene() : base("TestScene")
    {
        
    }

    public override void Initialize()
    {
        base.Initialize();

        // Sprite 로드
        Sprite walk1 = SpriteLoader.LoadFromFile("Assets/Player_Walk1.txt");
        Sprite walk2 = SpriteLoader.LoadFromFile("Assets/Player_Walk2.txt");
        Sprite walk3 = SpriteLoader.LoadFromFile("Assets/Player_Walk3.txt");

        Animation playerWalkAnimation = new Animation(new List<AnimationFrame>
        {
            new AnimationFrame(walk1, 1),
            new AnimationFrame(walk2, 1),
            new AnimationFrame(walk3, 1)
        });

       //  // Player GameObject 생성
       //  GameObject player = new GameObject("Player");
       //  AddObject(player, new Vector2<int>(10, 10));
       //  player.AddComponent<PlayerScript>();
       //  player.AddComponent<Rigidbody>();
       //  player.AddComponent<Animator>().SetAnimation(playerWalkAnimation);
       //  
       //  Text name = new Text("안은석");
       // player.AddChild(name);
       // name.GetComponent<Transform>().Position = new Vector2<int>(0, 1);

       Button btn1= new Button(new Vector2<int>(20, 5), "Button1");
       AddObject(btn1);

    }
    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);  //Scene 기본 로직 호출
        //추가 로직 구현
    }

    public override void Render()
    {
        base.Render();  //Scene 기본 로직 호출
        //추가 로직 구현
    }
}