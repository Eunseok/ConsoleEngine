using Core.Components;
using Core.Input;
using Core.MyMath;



namespace TestGame.Scripts
{
    public class PlayerScript : Script
    {
        public string PlayerName { get; set; } = string.Empty; // 플레이어 이름
        public string PlayerJob { get; set; } = string.Empty;  // 플레이어 직업
        public int CurrentLevel { get; set; } = 1;            // 현재 레벨

        
        public enum State
        {
            Idle,
            Walking
        };
        State _state = State.Idle;
        protected override void OnUpdate(float deltaTime)
        {
            Input();

        }

        private void Input()
        {
            Vector2<int> velocity = Vector2<int>.Zero();
            
            if(InputManager.GetKey("LeftArrow"))
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

        
    }
}