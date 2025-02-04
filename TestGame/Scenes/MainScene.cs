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
            //playerScript.Inventory = DataLoader.Items;
            GameManager.Instance.Player = playerScript;

            player.RegisterEventHandler("ShowShop", _ => CreateShop());

            //User Info
            var name = Instantiate<LabelObject>(new Vector2<int>(0, 2), player);
            name.SetText(playerScript.PlayerName);
            var job = Instantiate<LabelObject>(new Vector2<int>(0, 3), player);
            job.SetText(playerScript.PlayerJob);
            var level = Instantiate<LabelObject>(new Vector2<int>(0, 4), player);
            level.SetText("LV." + playerScript.Level.ToString());

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
                btn.RegisterEventHandler("OnClick", (o => { menu.BroadcastEvent("Menu", label); }));
            }

            ///상점 화살표
            {
                var arrow = Instantiate<GameObject>(new Vector2<int>(0, Game.ConsoleCenter.Y));
                Sprite sprite = SpriteLoader.LoadFromFile("Assets/LeftArrow.txt");
                arrow.GetComponent<Renderer>().SetSprite(sprite);
                arrow.SetOrder(-1);
            }
            ///던전 화살표
            {
                var arrow = Instantiate<GameObject>(new Vector2<int>(Console.WindowWidth - 5, Game.ConsoleCenter.Y));
                Sprite sprite = SpriteLoader.LoadFromFile("Assets/RightArrow.txt");
                arrow.GetComponent<Renderer>().SetSprite(sprite);
                arrow.SetOrder(-1);
            }
            ObjectSort();

            GameManager.Instance.Owner?.RegisterEventHandler("ShowStatus", _ => CreateStatus());
            GameManager.Instance.Owner?.RegisterEventHandler("ShowInventory", _ => CreateInventory());
            GameManager.Instance.Owner?.RegisterEventHandler("Buying", _ => CreateBuying());
            GameManager.Instance.Owner?.RegisterEventHandler("Selling", _ => CreateSelling());
        }

        public void CreateStatus()
        {
            var box = Instantiate<BoxObject>(Game.ConsoleCenter);
            box.SetSize(30, 20);
            box.SetOrder(10);
            var status = Instantiate<GameObject>(box);
            status.SetOrder(100);
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

            int i = 0;
            foreach (var item in GameManager.Instance?.Player?.Inventory)
            {
                var itemBtn = Instantiate<ButtonObject>(new Vector2<int>(0, -10 + i++ * 2), box);
                string text = item.isEquipped ? "[E] " + item.ToString() : item.ToString();
                itemBtn.SetLabel(text);
                itemBtn.RegisterEventHandler("OnClick", _ =>
                {
                    GameManager.Instance.Owner?.BroadcastEvent("EquipItem", item);
                    Destroy(box);
                    CreateInventory();
                });
            }

            if (GameManager.Instance?.Player?.Inventory.Count == 0)
                Instantiate<LabelObject>(box).SetText("보유중인 아이템이 없습니다.");

            var btn = Instantiate<ButtonObject>(new Vector2<int>(0, 10), box);
            btn.RegisterEventHandler("OnClick", _ =>
            {
                Destroy(box);
                GameManager.Instance.Owner?.BroadcastEvent("CloseMenu");
            });

            btn.SetLabel("닫기");
            ObjectSort();
        }

        public void CreateShop()
        {
            var box = Instantiate<BoxObject>(Game.ConsoleCenter);
            box.SetSize(70, 24);
            box.SetOrder(10);
            var shopScript = box.AddComponent<ShopScript>();
            var shop = Instantiate<LabelObject>(new Vector2<int>(0,-10),box);
            shop.SetText("[ 상 점 ]");
            foreach (var item in DataLoader.Items)
            {
                var itemBtn = Instantiate<LabelObject>(new Vector2<int>(0, -10 + item.ID * 2), box);
                // string text = item.isEquipped ?  item.ToString() : item.ToString();
                string text = item.ToString() + "| ";
                string infoText = item.iPrice.ToString() + "G";
                if (GameManager.Instance?.Player?.Inventory.Find(i => i.ID == item.ID) != null)
                    infoText = "[구매 완료]";
                text += infoText;
                itemBtn.SetText(text);
            }

            var gold = Instantiate<LabelObject>(new Vector2<int>(20,-10),box);
            gold.SetText("현재 골드: "+GameManager.Instance?.Player.Gold.ToString() +"G", ConsoleColor.Yellow);
            
            

            string[] str =
            {
                "구매",
                "판매",
                "나가기"
            };
            for (int i = 0; i < str.Length; i++)
            {
                var label = str[i];
                var btn = Instantiate<ButtonObject>(new Vector2<int>(-20 + i*20, 10), box);
                btn.RegisterEventHandler("OnClick", _ =>
                {
                    Destroy(box);
                    shopScript.Owner.BroadcastEvent("Shop", label);
                   // GameManager.Instance.Owner?.BroadcastEvent("CloseMenu");
                });
                btn.SetLabel(label);
            }

            ObjectSort();
        }

        public void CreateBuying()
        {
            var box = Instantiate<BoxObject>(Game.ConsoleCenter);
            box.SetSize(70, 24);
            box.SetOrder(10);
            var inventoryScript = box.AddComponent<InventoryScript>();
            var shop = Instantiate<LabelObject>(new Vector2<int>(0,-10),box);
            shop.SetText("[ 상 점 ] - 구매");
            foreach (var item in DataLoader.Items)
            {
                var itemBtn = Instantiate<ButtonObject>(new Vector2<int>(0, -10 + item.ID * 2), box);
                string text = item.ToString() + "| ";
                string infoText = item.iPrice.ToString() + "G";
                if (GameManager.Instance?.Player?.Inventory.Find(i => i.ID == item.ID) != null)
                    infoText = "[구매 완료]";
                text += infoText;
                
                itemBtn.SetLabel(text);
                itemBtn.RegisterEventHandler("OnClick", _ =>
                {
                    GameManager.Instance.Owner?.BroadcastEvent("BuyItem", item);
                    Destroy(box);
                    CreateBuying();
                });
            }
            
            var gold = Instantiate<LabelObject>(new Vector2<int>(20,-10),box);
            gold.SetText("현재 골드: "+GameManager.Instance?.Player.Gold.ToString() +"G", ConsoleColor.Yellow);
            
            var btn = Instantiate<ButtonObject>(new Vector2<int>(0, 10), box);
            btn.RegisterEventHandler("OnClick", _ =>
            {
                Destroy(box);
                CreateShop();
            });

            btn.SetLabel("취소");
            ObjectSort();
        }
        public void CreateSelling()
        {
            var box = Instantiate<BoxObject>(Game.ConsoleCenter);
            box.SetSize(70, 24);
            box.SetOrder(10);
            var inventoryScript = box.AddComponent<InventoryScript>();
            var shop = Instantiate<LabelObject>(new Vector2<int>(0,-10),box);
            shop.SetText("[ 상 점 ] - 판매");
            int i = 1;
            foreach (var item in GameManager.Instance.Player?.Inventory)
            {
                var itemBtn = Instantiate<ButtonObject>(new Vector2<int>(0, -10 + i++ * 2), box);
                string text = item.ToString() + "| ";
                string infoText = (item.iPrice*0.85).ToString() + "G";
     
                text += infoText;
                itemBtn.SetLabel(text);
                itemBtn.RegisterEventHandler("OnClick", _ =>
                {
                    GameManager.Instance.Owner?.BroadcastEvent("SellItem", item);
                    Destroy(box);
                    CreateSelling();
                });
            }
            
            if (GameManager.Instance?.Player?.Inventory.Count == 0)
                Instantiate<LabelObject>(box).SetText("보유중인 아이템이 없습니다.");
            
            var gold = Instantiate<LabelObject>(new Vector2<int>(20,-10),box);
            gold.SetText("현재 골드: "+GameManager.Instance?.Player.Gold.ToString() +"G", ConsoleColor.Yellow);
            
            var btn = Instantiate<ButtonObject>(new Vector2<int>(0, 10), box);
            btn.RegisterEventHandler("OnClick", _ =>
            {
                Destroy(box);
                CreateShop();
            });

            btn.SetLabel("취소");
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