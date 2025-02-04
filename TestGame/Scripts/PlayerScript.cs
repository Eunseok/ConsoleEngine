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

        public string PlayerName { get; set; } = string.Empty; // 플레이어 이름
        public string PlayerJob { get; set; } = string.Empty; // 플레이어 직업
        public int Level { get; set; } = 1; // 현재 레벨
        public int Gold { get; set; } = 1500;

        public float CurHp { get; set; } = 100;
        public int Exp { get; set; } = 0;
        public int MaxExp { get; set; } = 1;
        public Stats PlayerStats { get; set; } = new Stats(10, 5, 100);
        public Stats AddStats { get; set; } = new Stats(0, 0, 0);
        public List<ItemScript> Inventory { get; set; } = new List<ItemScript>();

        private bool _canMove = true;

        public enum State
        {
            Idle,
            Walking
        };

        State _state = State.Idle;

        public override void Initialize()
        {
            GameManager.Instance.Owner?.RegisterEventHandler("ShowMenu", o => { Owner.SetActive(false); });
            GameManager.Instance.Owner?.RegisterEventHandler("ShowInventory", o => { Owner.SetActive(false); });
            GameManager.Instance.Owner?.RegisterEventHandler("CloseMenu", o => { Owner.SetActive(true); });
            GameManager.Instance.Owner?.RegisterEventHandler("EquipItem", (object data) =>
            {
                if (data is ItemScript item)
                {
                    EquipItem(item);
                }
            });
        }

        protected override void OnUpdate(float deltaTime)
        {
            Input();
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

            Move(velocity);
        }

        private void Move(Vector2<int> velocity)
        {
            if (velocity == Vector2<int>.Zero())
            {
                _state = State.Idle;
                return;
            }

            _state = State.Walking;
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

        private void CalculateAddStats()
        {
            Stats stats = new Stats(0, 0, 0);
            foreach (var item in Inventory.FindAll(i => i.isEquipped))
            {
                Stats itemStats = ApplyItemEffect(item);
                stats = new PlayerScript.Stats(stats.Atk+itemStats.Atk,
                    stats.Def+itemStats.Def, stats.Hp+itemStats.Hp);
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

        public override string ToString()
        {
            return $"{PlayerName} : {PlayerJob}\n\n" +
                   $"Level : {Level}\n\n" +
                   $"HP : {CurHp} / {PlayerStats.Hp}" + ( AddStats.Hp > 0 ? $" (+{ AddStats.Hp})" : "") + "\n\n" +
                   $"Atk : {PlayerStats.Atk}" + (AddStats.Atk > 0 ? $" (+{AddStats.Atk})" : "") + "\n\n" +
                   $"Def : {PlayerStats.Def}" + (AddStats.Def > 0 ? $" (+{AddStats.Def})" : "") + "\n\n" +
                   $"Exp : {Exp} / {MaxExp}\n\n" +
                   $"Gold : {Gold}G\n\n";
        }
    }
}