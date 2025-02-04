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
            GameObject gameManager = Instantiate<GameObject>(Vector2<int>.Zero());
            gameManager.AddComponent<GameManager>();

            // //Player Object생성
            // GameObject player = Instantiate<GameObject>(Game.ConsoleCenter);
            //
            // Animation anim = new Animation(new List<AnimationFrame>
            // {
            //     new AnimationFrame(SpriteLoader.LoadFromFile("Assets/Player_Walk1.txt"), 1),
            //     new AnimationFrame(SpriteLoader.LoadFromFile("Assets/Player_Walk2.txt"), 1),
            //     new AnimationFrame(SpriteLoader.LoadFromFile("Assets/Player_Walk3.txt"), 1),
            // });
            //
            // player.AddComponent<Animator>().SetAnimation(anim);
            // PlayerScript playerScript = player.AddComponent<PlayerScript>();
            // playerScript.PlayerName = CreationManager.Instance.PlayerName;
            // playerScript.PlayerJob = CreationManager.Instance.PlayerJob;
            //
            // //User Info
            // var name =  Instantiate<LabelObject>(new Vector2<int>(0, 2),player);
            // name.SetText(playerScript.PlayerName);
            // var job =  Instantiate<LabelObject>(new Vector2<int>(0, 3),player);
            // job.SetText(playerScript.PlayerJob);
            // var level =  Instantiate<LabelObject>(new Vector2<int>(0, 4),player);
            // level.SetText("LV."+playerScript.CurrentLevel.ToString());

            Instantiate<LabelObject>(new Vector2<int>(10, 0)).SetText("[M]Menu");
            
            var menu = Instantiate<BoxObject>(new Vector2<int>(10, 7));
            menu.SetActive(false);
            GameManager.Instance.Menu = menu;
            GameManager.Instance.Menu.SetSize(18, 13);

            string[] str =
            {
                "상태보기",
                "인벤토리",
                "휴식하기",
                "저장/종료",
                "닫기"
            };

            for (int i = 0; i < str.Length; i++)
            {
                Instantiate<ButtonObject>(new Vector2<int>(0, -4 + i * 2), menu).SetLabel(str[i]);
            }

            
        }

    

        // 특정 로직 실행 (예: 업데이트 로직)
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            
        }

        // 필요에 따라 추가 커스터마이징 가능
    }
}