using Core.Input;
using Core.MyMath;
using Core.Graphics;
using Core.Objects;

namespace Core.Components
{
    public class Button : Component
    {
        
        public string Label { get; private set; } = "Button";
        public bool IsFocused { get; private set; } = false;
        public Vector2<int> Size { get; private set; } = new Vector2<int>(10, 3);
        

        public Button()
        {

        }
        public void SetFocus(bool isFocused)
        {
            IsFocused = isFocused;
        }

        public void SetLabel(string label)
        {
            Label = label;
        }
        public void SetSize(int width, int height)
        {
            Size = new Vector2<int>(width, height);
        }
        

        public override void Update(float deltaTime)
        {
            SetFocus(IsMouseOver());
            if (IsFocused)
            {
                // 포커스된 상태에서 클릭 감지
                if (InputManager.GetKey("Enter"))
                {
                    SendMessage("OnClick", this);
                }
            }
        }

        private bool IsMouseOver()
        {
            Vector2<int> cursorPos = Game.CursorPosition;
            Vector2<int> position = Owner.GlobalPosition;

            return cursorPos.X >= position.X && cursorPos.X < position.X + Size.X &&
                   cursorPos.Y >= position.Y && cursorPos.Y < position.Y + Size.Y;
        }
        
    }
}