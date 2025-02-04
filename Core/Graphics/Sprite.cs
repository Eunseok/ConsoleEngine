using Core.MyMath;

namespace Core.Graphics
{
    public class Sprite
    {
        public string[] Data { get; private set; } // 스프라이트 데이터를 문자열 배열로 보관
        public int Width => CalculateWidth(); // 스프라이트의 가로 폭 계산
        public int Height => Data?.Length ?? 0; // 스프라이트의 세로 크기

        public Vector2<int> Offset { get; set; } = new Vector2<int>(0, 0); // 오프셋 중심 설정

        public Sprite(string[] data)
        {
            if (data == null || data.Length == 0)
                throw new ArgumentException("Sprite data cannot be null or empty");

            Data = data; // 초기 데이터 설정
            UpdateOffsetToCenter(); // 초기화 후 중앙 위치 정의
        }

        public void SetOffset(int x, int y)
        {
            Offset = new Vector2<int>(x, y);
        }
        private int CalculateWidth()
        {
            if (Data == null || Data.Length == 0) return 0;

            // 첫 번째 줄의 문자열 폭 계산
            int width = Data[0].Sum(c =>
                char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter ? 2 : 1
            );

            return width;
        }

        private void UpdateOffsetToCenter()
        {
            Offset = new Vector2<int>(Width / 2, Height / 2);
        }

        public static Sprite FromString(string spriteString)
        {
            if (string.IsNullOrWhiteSpace(spriteString))
                throw new ArgumentException("Sprite string cannot be null or empty!");

            var lines = spriteString.Split('\n');
            return new Sprite(lines);
        }

        public void Print()
        {
            foreach (var line in Data)
            {
                Console.WriteLine(line);
            }
        }
    }
}