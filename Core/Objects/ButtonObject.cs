using Core.Components;
using Core.MyMath;
using Core.Graphics;

namespace Core.Objects;

public class ButtonObject : GameObject
{
    
    public Vector2<int> Size { get; set; } = new Vector2<int>(10, 3);
    public ButtonObject() : base("Button")
    {
        
    }

    public override void Initialize()
    {
        AddComponent<Transform>();
        AddComponent<Button>();
        Sprite sprite = new Sprite(GenerateSprite());
        AddComponent<Renderer>()?.SetSprite(sprite);
    }
    public void SetLabel(string text, ConsoleColor color = ConsoleColor.White)
    {
        GetComponent<Button>()?.SetLabel("");
    }
    
    // 스프라이트 생성
    private string[] GenerateSprite()
    {
        {
            string[] sprite = new string[Size.Y];
            string horizontalBorder = new string('#', Size.X); // 상단/하단 테두리

            sprite[0] = horizontalBorder; // 상단 테두리

            for (int i = 1; i < Size.Y - 1; i++)
            {
                string padding = new string(' ', Size.X - 2); // 기본 여백
                sprite[i] = $"#{padding}#";
            }

            sprite[Size.Y - 1] = horizontalBorder; // 하단 테두리
            return sprite;
        }
    }
    
    // // 스프라이트 생성: 텍스트 데이터를 기반으로 Sprite 정의
    // private string[] GenerateSprite()
    // {
    //     {
    //         string[] sprite = new string[Size.Y];
    //         string horizontalBorder = new string('#', Size.X); // 상단/하단 테두리
    //
    //         sprite[0] = horizontalBorder; // 상단 테두리
    //
    //         for (int i = 1; i < Size.Y - 1; i++)
    //         {
    //             string padding = new string(' ', Size.X - 2); // 기본 여백
    //
    //             if (i == Size.Y / 2) // 중간 라인에 텍스트 삽입
    //             {
    //                 // 텍스트의 좌우 패딩 계산
    //                 int totalPadding = Size.X - 2 - text.Length; // 총 여백 = 박스 내부 폭 - 텍스트 길이
    //                 int leftPaddingSize = totalPadding / 2;
    //                 int rightPaddingSize = totalPadding - leftPaddingSize;
    //
    //                 // 왼쪽과 오른쪽에 패딩 추가
    //                 string leftPadding = new string(' ', leftPaddingSize);
    //                 string rightPadding = new string(' ', rightPaddingSize);
    //
    //                 // 텍스트 삽입
    //                 sprite[i] = $"#{leftPadding}{text}{rightPadding}#";
    //             }
    //             else // 일반 여백 줄
    //             {
    //                 sprite[i] = $"#{padding}#";
    //             }
    //         }
    //
    //         sprite[Size.Y - 1] = horizontalBorder; // 하단 테두리
    //         return sprite;
    //     }
    // }
}