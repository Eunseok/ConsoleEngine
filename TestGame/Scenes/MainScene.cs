using System.Numerics;
using Core;
using Core.Components;
using Core.Graphics;
using Core.MyMath;
using Core.Objects;
using Core.Scenes;
using TestGame.Scripts;
using TestGame.Singletons;
using static Core.Objects.Object;

namespace TestGame.Scenes
{
    public class MainScene : Scene
    {
        public MainScene() : base("MainScene")
        {
        }

        // TestScene의 초기화 코드
        public override void Initialize()
        {
            //Player Object생성
            GameObject player = Instantiate<GameObject>( new Vector2<int>(10, 10));
            
            Animation anim = new Animation(new List<AnimationFrame>
            {
                new AnimationFrame(SpriteLoader.LoadFromFile("Assets/Player_Walk1.txt"), 1),
                new AnimationFrame(SpriteLoader.LoadFromFile("Assets/Player_Walk2.txt"), 1),
                new AnimationFrame(SpriteLoader.LoadFromFile("Assets/Player_Walk3.txt"), 1),
            });
            
            player.AddComponent<Animator>().SetAnimation(anim);
            PlayerScript playerScript = player.AddComponent<PlayerScript>();
            playerScript.PlayerName = CreationManager.Instance.PlayerName;
            playerScript.PlayerJob = CreationManager.Instance.PlayerJob;
            
            //User Info
            var name =  Instantiate<LabelObject>(new Vector2<int>(0, 2),player);
            name.SetText(playerScript.PlayerName);
            var job =  Instantiate<LabelObject>(new Vector2<int>(0, 3),player);
            job.SetText(playerScript.PlayerJob);
            var level =  Instantiate<LabelObject>(new Vector2<int>(0, 4),player);
            level.SetText("LV."+playerScript.CurrentLevel.ToString());
        }

        // 특정 로직 실행 (예: 업데이트 로직)
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            
        }

        // 필요에 따라 추가 커스터마이징 가능
    }
}