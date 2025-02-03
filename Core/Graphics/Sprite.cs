using Core.Math;
namespace Core.Graphics
{
    public class Sprite
    {
        public string[] Data { get; set; }// 문자열 배열로 Sprite 구성
        public int Width => CalculateWidth(); // Width는 계산 함수의 반환 값을 가리킵니다.
        public int Height => Data.Length; // Sprite의 세로 크기
        private int CalculateWidth()
        {
            if (Data == null) return 0;
            
            int width = 0;
    
            // Data 배열의 첫 번째 문자열의 폭 계산
            foreach (char c in Data[0])
            {
                if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
                {
                    // 한글, 한자 등 (2칸 폭)
                    width += 2;
                }
                else
                {
                    // 영어, 숫자 등 (1칸 폭)
                    width += 1;
                }
            }

            return width;
        }

        public Vector2<int> Offset { get;  set; } = new Vector2<int>().Zero(); // offset
        
        public Sprite(string[] data)
        {
            // 스프라이트 데이터 초기화
            Data = data;
            UpdateOffsetToCenter();
        }
        
        private void UpdateOffsetToCenter()
        {
            int centerX = Width / 2;  // 스프라이트 중심 X 값
            int centerY = Height / 2 ; // 스프라이트 중심 Y 값

            Offset = new Vector2<int>(centerX, centerY); // 중심값으로 Offset 설정
        }

        
        public static Sprite FromString(string spriteString)
        {
            // 줄바꿈(`\n`)으로 스프라이트를 분리하여 문자열 배열로 변환
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