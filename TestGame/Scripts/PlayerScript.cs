using Core;
using Core.Components;
using Core.Input;
using Core.MyMath;
using Core.Objects;
using TestGame.Singletons;
using static Core.Objects.Object;

namespace TestGame.Scripts
{
    public class PlayerScript : Script
    {
        public struct Stats
        {
            public float Atk { get; set; } // 공격력
            public float Def { get; set; } // 방어력
            public float Hp { get; set; } // 체력

            // 생성자 (초기값 설정)
            public Stats(float atk, float def, float hp)
            {
                this.Atk = atk;
                this.Def = def;
                this.Hp = hp;
            }
        }

        public string PlayerName{ get; set; } = string.Empty; // 플레이어 이름
        public string PlayerJob { get; set; } = string.Empty; // 플레이어 직업

        public void SetPlayerInfo(string name, string job)
        {
            PlayerName = name;
            PlayerJob = job;
            _name?.SetText(name);
            _job?.SetText(job);
        }
        public int Level { get; set; } = 1; // 현재 레벨
        public int Gold { get; set; } = 1500;

        public float CurHp { get; set; } = 100;
        public int Exp { get; set; } = 0;
        public int MaxExp { get; set; } = 1;
        public Stats PlayerStats { get; set; } = new Stats(10, 5, 100);
        public Stats AddStats { get; set; } = new Stats(0, 0, 0);
        public List<ItemScript> Inventory { get; set; } = new List<ItemScript>();

        private bool _canMove = true;

        private LabelObject? _name;
        private LabelObject? _job;
        private LabelObject? _level;


        public override void Initialize()
        {
            //User Info
            _name = Instantiate<LabelObject>(new Vector2<int>(0, 2), Owner);
            _name.SetText(PlayerName);
            _job = Instantiate<LabelObject>(new Vector2<int>(0, 3), Owner);
            _job.SetText(PlayerJob);
            _level = Instantiate<LabelObject>(new Vector2<int>(0, 4), Owner);
            _level.SetText("LV." + Level.ToString());

            GameManager.Instance.Owner?.RegisterEventHandler("ShowMenu", o => { Owner.SetActive(false); });
            GameManager.Instance.Owner?.RegisterEventHandler("ShowInventory", o => { Owner.SetActive(false); });
            GameManager.Instance.Owner?.RegisterEventHandler("ShowRest", o => { Owner.SetActive(false); });
            Owner.RegisterEventHandler("ShowDungeon", o => { Owner.SetActive(false); });
            Owner.RegisterEventHandler("ShowShop", o => { Owner.SetActive(false); });
            GameManager.Instance.Owner?.RegisterEventHandler("PlayerCanMove", o => { Owner.SetActive(true); });
            GameManager.Instance.Owner?.RegisterEventHandler("Resting", o => Resting());
            GameManager.Instance.Owner?.RegisterEventHandler("EquipItem", (object data) =>
            {
                if (data is ItemScript item)
                {
                    EquipItem(item);
                }
            });
            GameManager.Instance.Owner?.RegisterEventHandler("BuyItem", (object data) =>
            {
                if (data is ItemScript item)
                {
                    BuyItem(item);
                }
            });
            GameManager.Instance.Owner?.RegisterEventHandler("SellItem", (object data) =>
            {
                if (data is ItemScript item)
                {
                    SellItem(item);
                }
            });
            GameManager.Instance.Owner?.RegisterEventHandler("DungeonEntry", (object data) =>
            {
                if (data is ValueTuple<string, int> tuple)
                {
                    DungeonEntry(tuple.Item1, tuple.Item2);
                }
            });
        }

        protected override void OnUpdate(float deltaTime)
        {
            Input();
            if (Owner.GlobalPosition.X < 10)
            {
                Owner.GetComponent<Transform>()?.SetPosition(Game.ConsoleCenter);
                SendMessage("ShowShop");
            }

            if (Owner.GlobalPosition.X > Console.WindowWidth - 10)
            {
                Owner.GetComponent<Transform>()?.SetPosition(Game.ConsoleCenter);
                SendMessage("ShowDungeon");
            }
        }

        private void Input()
        {
            if (!_canMove) return;

            Vector2<int> velocity = Vector2<int>.Zero();

            if (InputManager.GetKey("LeftArrow"))
                velocity.X -= 1;
            if (InputManager.GetKey("RightArrow"))
                velocity.X += 1;
            if (InputManager.GetKey("UpArrow"))
                velocity.Y -= 1;
            if (InputManager.GetKey("DownArrow"))
                velocity.Y += 1;

            Owner?.GetComponent<Transform>()?.Translate(velocity);
        }


        private void EquipItem(ItemScript item)
        {
            if (GameManager.Instance?.Player == null) return;

            // 기존 장착 해제
            foreach (var invItem in GameManager.Instance.Player.Inventory)
            {
                if (invItem.Type == item.Type && invItem.isEquipped)
                {
                    invItem.isEquipped = false;
                }
            }

            // 새 아이템 장착
            item.isEquipped = true;
            CalculateAddStats(); //추가 능력치 계싼
        }

        private void BuyItem(ItemScript item)
        {
            if (GameManager.Instance?.Player == null) return;


            if (Inventory.FindAll(i => i.ID == item.ID).FirstOrDefault() != null)
            {
                GameManager.Instance.Owner.BroadcastEvent("Already");
                return;
            }

            if (Gold < item.iPrice)
            {
                GameManager.Instance.Owner.BroadcastEvent("LessMoney");
                return;
            }
            //구매 성공

            Gold -= item.iPrice;
            GameManager.Instance.Owner.BroadcastEvent("BuySuccess");
            Inventory.Add(item);
        }

        private void SellItem(ItemScript item)
        {
            ItemScript sell = Inventory.Find(i => i.ID == item.ID);
            if (sell == null)
                return;


            if (sell.isEquipped)
            {
                sell.isEquipped = false;
                CalculateAddStats();
            }

            Gold += (int)(sell.iPrice * 0.85);
            Inventory.Remove(sell);
            GameManager.Instance.Owner.BroadcastEvent("SellSuccess");
        }

        private void CalculateAddStats()
        {
            Stats stats = new Stats(0, 0, 0);
            foreach (var item in Inventory.FindAll(i => i.isEquipped))
            {
                Stats itemStats = ApplyItemEffect(item);
                stats = new PlayerScript.Stats(stats.Atk + itemStats.Atk,
                    stats.Def + itemStats.Def, stats.Hp + itemStats.Hp);
            }

            AddStats = stats;
        }


        private Stats ApplyItemEffect(ItemScript item)
        {
            switch (item.Type)
            {
                case ItemScript.ItemType.Armor:
                    return new Stats(0, item.iEffect, 0);

                case ItemScript.ItemType.Weapon:
                    return new Stats(item.iEffect, 0, 0);

                case ItemScript.ItemType.Accessory:
                    return new Stats(0, 0, item.iEffect);
            }

            return new Stats();
        }

        private void Resting()
        {
            if (Gold < 500)
            {
                GameManager.Instance.Owner.BroadcastEvent("LessMoney");
                return;
            }

            Gold -= 500;
            GameManager.Instance.Owner.BroadcastEvent("RestingSuccess");
            CurHp = GetStats().Hp;
        }

        private void DungeonEntry(string name, int type)
        {
            if (CurHp <= 0)
            {
                GameManager.Instance.Owner.BroadcastEvent("Health");
                return;
            }
            
            int def = GameManager.Instance.DungeonDef[type];
            if (GetStats().Def < def)
            {
                int rand = new Random().Next(0, 100);
                if (rand < 40)
                {
                    //40% 확률로 던전 실패
                    float hp = GetStats().Hp / 2.0f;
                    string hpStr = "HP: " + CurHp + " -> ";
                    CurHp -= hp;
                    if (CurHp < 0) CurHp = 0;
                    hpStr += CurHp;

                    GameManager.Instance.Owner.BroadcastEvent("Failed", (name, hpStr));
                    return;
                }
            }

            //던전클리어
            {
                int rewardGold = ResultGoldCalc(type);
                string goldStr = "Gold: " + Gold.ToString() + "G" + " -> ";
                Gold += rewardGold;
                goldStr += Gold.ToString() + "G";

                float hp = ResultHpCalc(def - (int)GetStats().Def);
                string hpStr = "HP: " + CurHp + " -> ";
                CurHp -= hp;
                if (CurHp < 0) CurHp = 0;
                hpStr += CurHp;

                AddExp();
                GameManager.Instance.Owner.BroadcastEvent("Clear", (name, hpStr, goldStr));
            }
        }

        private int ResultHpCalc(int diffrent)
        {
            int min = 20 + (int)(diffrent);
            int max = 35 + (int)(diffrent);
            int rand = new Random().Next(min, max);
            return rand;
        }

        private int ResultGoldCalc(int diffculty)
        {
            int[] reward = GameManager.Instance.Reward;

            int min = (int)GetStats().Atk; //(공격력 ~ 공겨력*2)% 추가보상
            int max = (int)GetStats().Atk * 2;
            float rand = new Random().Next(min, max) / 100.0f;
            return reward[diffculty] + (int)(reward[diffculty] * rand);
        }

        private void AddExp() //레벨업
        {
            if (++Exp >= MaxExp)
            {
                Exp -= MaxExp++;
                Level++;
                _level?.SetText("LV." + Level.ToString());
                PlayerStats = new Stats(PlayerStats.Atk + 0.5f, PlayerStats.Def + 1.0f, PlayerStats.Hp + 5.0f);
            }
        }

        public Stats GetStats()
        {
            Stats stats = new Stats(PlayerStats.Atk + AddStats.Atk, PlayerStats.Def + AddStats.Def,
                PlayerStats.Hp + AddStats.Hp);
            return stats;
        }

        public override string ToString()
        {
            return $"{PlayerName} : {PlayerJob}\n\n" +
                   $"Level : {Level}\n\n" +
                   $"HP : {CurHp} / {PlayerStats.Hp}" + (AddStats.Hp > 0 ? $" (+{AddStats.Hp})" : "") + "\n\n" +
                   $"Atk : {PlayerStats.Atk}" + (AddStats.Atk > 0 ? $" (+{AddStats.Atk})" : "") + "\n\n" +
                   $"Def : {PlayerStats.Def}" + (AddStats.Def > 0 ? $" (+{AddStats.Def})" : "") + "\n\n" +
                   $"Exp : {Exp} / {MaxExp}\n\n" +
                   $"Gold : {Gold}G\n\n";
        }
    }
}