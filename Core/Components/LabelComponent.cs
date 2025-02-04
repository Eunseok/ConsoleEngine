using Core.Objects;
using Core.MyMath;

namespace Core.Components
{
    public class LabelComponent : Component
    {
        private string _label = "Text";
        private ConsoleColor _color;
        
        public void SetLabel(string label,
            ConsoleColor color = ConsoleColor.White)
        {
            _label = label;
            _color = color;
        }
        private int CalculateWidth()
        {

            // 첫 번째 줄의 문자열 폭 계산
            int width = _label.Sum(c =>
                char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter ? 2 : 1
            );

            return width;
        }

        public override void Update(float deltaTime)
        {
            if (!string.IsNullOrEmpty(_label))
            {
                Render(_label);
            }
        }

        private void Render(string text)
        {
            // 텍스트 렌더링 처리 
            Vector2<int> pos = Owner!.GlobalPosition;
    
            // System.Math.Clamp를 사용해서 위치 제한
            pos.X = System.Math.Clamp(pos.X-CalculateWidth()/2, 0, Console.WindowWidth - CalculateWidth()/2);
            pos.Y = System.Math.Clamp(pos.Y, 0, Console.WindowHeight);

            // Render Logic
            Console.SetCursorPosition(pos.X, pos.Y);
            Console.ForegroundColor = _color;
            Console.WriteLine(text);
    
            Console.ResetColor();
        }
    }
}