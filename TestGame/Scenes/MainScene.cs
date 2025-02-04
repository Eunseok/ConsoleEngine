using System.Numerics;
using Core;
using Core.Components;
using Core.Graphics;
using Core.MyMath;
using Core.Objects;
using Core.Scenes;
using TestGame.Scripts;
using TestGame.Singletons;
using TextRPG;
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
            gameManager.AddGameManager(GameManager.Instance);
            //Player Object생성
            GameObject player = Instantiate<GameObject>(Game.ConsoleCenter);
         
            
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
            playerScript.Inventory = DataLoader.Items;
            GameManager.Instance.Player = playerScript;
            
            //User Info
            var name =  Instantiate<LabelObject>(new Vector2<int>(0, 2),player);
            name.SetText(playerScript.PlayerName);
            var job =  Instantiate<LabelObject>(new Vector2<int>(0, 3),player);
            job.SetText(playerScript.PlayerJob);
            var level =  Instantiate<LabelObject>(new Vector2<int>(0, 4),player);
            level.SetText("LV."+playerScript.Level.ToString());

            Instantiate<LabelObject>(new Vector2<int>(3, 0)).SetText("[M]Menu");
            
            var menu = Instantiate<BoxObject>(new Vector2<int>(Game.ConsoleCenter.X, 10));
            menu.SetActive(false);
            menu.AddComponent<MenuScript>();
            GameManager.Instance.Menu = menu;
            GameManager.Instance.Menu.SetSize(Game.ConsoleCenter.X, 13);

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
                var label = str[i]; // i에 해당하는 값을 지역 변수로 저장
                var btn = Instantiate<ButtonObject>(new Vector2<int>(0, -4 + i * 2), menu);
                btn.SetLabel(label);
                btn.RegisterEventHandler("OnClick", (o =>
                {
                    menu.BroadcastEvent("Menu", label);
                }));
            }


            GameManager.Instance.Owner?.RegisterEventHandler("ShowStatus",_=>CreateStatus());
            GameManager.Instance.Owner?.RegisterEventHandler("ShowInventory",_=>CreateInventory());
        }

        public void CreateStatus()
        {
            var box = Instantiate<BoxObject>(Game.ConsoleCenter);
            box.SetSize(30, 20);
            box.SetOrder(10);
            var status = Instantiate<GameObject>(box);
            Renderer renderer = status.AddComponent<Renderer>();
            Sprite sprite = Sprite.FromString(GameManager.Instance.Player.ToString());
            renderer.SetSprite(sprite);
            var btn = Instantiate<ButtonObject>(new Vector2<int>(0, 7), box);
            btn.RegisterEventHandler("OnClick", _ =>
            {
                Destroy(box);
                GameManager.Instance.Owner?.BroadcastEvent("CloseMenu");
            });
            Game.CursorPosition = btn.GlobalPosition;
            
            btn.SetLabel("닫기");
            ObjectSort();
        }

        public void CreateInventory()
        {
            var box = Instantiate<BoxObject>(Game.ConsoleCenter);
            box.SetSize(70, 24);
            box.SetOrder(10);
            var inventoryScript = box.AddComponent<InventoryScript>();

            foreach (var item in GameManager.Instance?.Player?.Inventory)
            {
                var itemBtn = Instantiate<ButtonObject>(new Vector2<int>(0, -10 + item.ID*2), box);
                string text = item.isEquipped ? "[E] " + item.ToString() : item.ToString();
                itemBtn.SetLabel(text);
                itemBtn.RegisterEventHandler("OnClick", _ =>
                {
                    GameManager.Instance.Owner?.BroadcastEvent("EquipItem", item);
                    Destroy(box);
                    CreateInventory();
                });
            }
            
            var btn = Instantiate<ButtonObject>(new Vector2<int>(0, 10), box);
            btn.RegisterEventHandler("OnClick", _ =>
            {
                Destroy(box);
                GameManager.Instance.Owner?.BroadcastEvent("CloseMenu");
            });
            
            btn.SetLabel("닫기");
            ObjectSort();
        }

        // 특정 로직 실행 (예: 업데이트 로직)
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            
        }

        // 필요에 따라 추가 커스터마이징 가능
    }
}