using Core.Components;

using Core.Objects;

namespace TestGame.Scripts
{
    public class PlayerScript : Script
    {
        private int _health = 100;

        public override void OnAttach(GameObject owner)
        {
            base.OnAttach(owner);
            // 메시지 핸들러 등록
            SendMessage("PlayerSpawned", owner);
        }

        protected override void OnUpdate(float deltaTime)
        {
            // 단순 테스트용 메시지 예제
            if (_health <= 0)
            {
                SendMessage("PlayerDied", null);
            }
        }

        public override void OnMessageReceived(string eventKey, object data)
        {
            if (eventKey == "Damage")
            {
                int damage = (int)data;
                _health -= damage;
                Console.WriteLine($"Player took {damage} damage. Remaining Health: {_health}");
            }
        }
    }
}