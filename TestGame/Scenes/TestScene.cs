using Core.Graphics;
using Core.Scenes;
using Core.Components;
using Core.Math;
using Core.Objects;
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

        // Player GameObject 생성
        GameObject player = new GameObject("Player");
        player.AddComponent<Transform>().Position = new Vector2(10, 10);
        player.AddComponent<Rigidbody>();
        player.AddComponent<Renderer>();
        player.AddComponent<PlayerScript>();
        player.AddComponent<Animator>().SetAnimation(playerWalkAnimation);

        AddObject(player);
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