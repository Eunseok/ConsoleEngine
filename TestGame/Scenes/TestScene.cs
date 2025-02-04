using System.Numerics;
using Core;
using Core.Components;
using Core.Graphics;
using Core.MyMath;
using Core.Objects;
using Core.Scenes;
using TestGame.Scripts;

namespace TestGame.Scenes
{
    public class TestScene : Scene
    {
        public TestScene() : base("TestScene")
        {
        }

        // TestScene의 초기화 코드
        public override void Initialize()
        {
            //Test Object생성
            GameObject obj = new GameObject("TestObject");
            AddObject(obj, new Vector2<int>(10, 10));
            Animation anim = new Animation(new List<AnimationFrame>
            {
                new AnimationFrame(SpriteLoader.LoadFromFile("Assets/Player_Walk1.txt"), 1),
                new AnimationFrame(SpriteLoader.LoadFromFile("Assets/Player_Walk2.txt"), 1),
                new AnimationFrame(SpriteLoader.LoadFromFile("Assets/Player_Walk3.txt"), 1),
            });
            
            obj.AddComponent<Animator>().SetAnimation(anim);
            obj.AddComponent<TestScript>();
            
            //TestObject2
            var obj2 = new LabelObject();
            //obj2.Parent = obj;
            AddObject(obj2, new Vector2<int>(0, 2));
            obj2.Parent = obj;
            obj2.SetText("윤서진🍔", ConsoleColor.Red);

           //ButtonObject
           var button = new ButtonObject();
           AddObject(button, Game.ConsoleCenter);
        }

        // 특정 로직 실행 (예: 업데이트 로직)
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            
        }

        // 필요에 따라 추가 커스터마이징 가능
    }
}