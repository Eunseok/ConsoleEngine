

namespace Core.Graphics
{
    public class Sprite 
    {
        public string[] Data { get; private set; } // 문자열 배열로 Sprite 구성
        public int Width => Data[0].Length; // Sprite의 가로 크기
        public int Height => Data.Length; // Sprite의 세로 크기

        public Sprite(string[] data)
        {
            // 스프라이트 데이터 초기화
            Data = data;
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