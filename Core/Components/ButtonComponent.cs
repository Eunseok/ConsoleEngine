using Core.Input;
using Core.MyMath;
using Core.Graphics;
using Core.Objects;

namespace Core.Components
{
    public class Button : Script
    {
        public string Label { get; private set; } = "Button";
        public bool IsFocused { get; private set; } = false;
        public Vector2<int> Size { get; set; } = new Vector2<int>(10, 3);

        public Button()
        {

        }
        public void SetLabel(string label)
        {
            Label = label;
        }
        public void SetSize(int width, int height)
        {
            Size = new Vector2<int>(width, height);
        }

        public void SetFocus(bool isFocused)
        {
            IsFocused = isFocused;
        }

        public override void Update(float deltaTime)
        {
            if (IsMouseOver())
            {
                // 포커스된 상태에서 클릭 감지
                if (InputManager.GetKey("Enter"))
                {
                    SendMessage("OnClick", this);
                }
            }

            // // 렌더링 처리 - 포커스 여부에 따라 다르게 표시
            // Console.WriteLine(IsFocused
            //     ? $"> [ {Label} ] <"
            //     : $"  [ {Label} ]");
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