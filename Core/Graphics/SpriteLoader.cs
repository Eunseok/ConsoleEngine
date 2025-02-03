using System;
using System.IO;

namespace Core.Graphics
{
    public class SpriteLoader
    {
        public static Sprite LoadFromFile(string path)
        {
            try
            {
                // 파일에서 각 줄을 읽어와 Sprite 인스턴스 생성
                var lines = File.ReadAllLines(path);
                return new Sprite(lines);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Error: Sprite file not found at path '{path}'. {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: An unexpected error occurred. {ex.Message}");
            }

            // 기본 스프라이트를 반환하거나 null 반환 (Fallback 처리)
            return CreateDefaultSprite();
        }

        // 기본 스프라이트를 반환 - 예외 발생 시 임시 데이터로 활용
        private static Sprite CreateDefaultSprite()
        {
            string[] defaultSpriteData =
            {
                "???",
                "???",
                "???"
            };
            return new Sprite(defaultSpriteData);
        }
    }
}