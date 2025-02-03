using Core.Math;
using Core.Graphics;
using Core.Components;
using System;
using Core.Input;

namespace Core.Objects
{
    // Button은 GameObject를 상속받아 UI 버튼 역할을 수행
    public class Button : GameObject
    {
        // 버튼 텍스트
        public string Text { get; set; }

        // 클릭 이벤트
        public event Action? OnClick;

        // 버튼 크기
        public Vector2<int> Size { get; set; }

        // 버튼 활성화 상태
        public bool IsEnabled { get; set; } = true;

        // 생성자 - 버튼 이름, 위치, 크기, 텍스트 초기화
        public Button(Vector2<int> size, string text) : base("Button")
        {
            Size = size;
            Text = text;
        }

        // 초기화 메서드에서 기본 컴포넌트 추가
        public override void Initialize()
        {
            base.Initialize();

            // 버튼용 Renderer 설정
            Renderer renderer = GetComponent<Renderer>();

            // 텍스트 데이터를 Sprite로 변환
            var spriteData = GenerateSpriteFromText(Text);
            renderer.Sprite = new Sprite(spriteData);
        }

        // Update: 버튼 클릭 또는 업데이트 관련 작업 처리
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (!IsEnabled) return;

            // 클릭 이벤트 처리 (콘솔 입력 대용으로 예시)
            if (IsMouseOver() && InputManager.GetKey("Enter"))
            {
                OnClick?.Invoke(); // OnClick 이벤트 호출
            }
        }

        // Render: 버튼 렌더링
        public override void Render()
        {
            base.Render();

            if (!IsEnabled) return;

            // Console.ForegroundColor = ConsoleColor.Yellow; // 버튼 텍스트 색상 설정
            // Console.SetCursorPosition(GlobalPosition().X, GlobalPosition().Y);
            // Console.Write(Text); // 버튼 텍스트 출력
            // Console.ResetColor();
        }

        // 스프라이트 생성: 텍스트 데이터를 기반으로 Sprite 정의
        private string[] GenerateSpriteFromText(string text)
        {
            {
                string[] sprite = new string[Size.Y];
                string horizontalBorder = new string('#', Size.X); // 상단/하단 테두리

                sprite[0] = horizontalBorder; // 상단 테두리

                for (int i = 1; i < Size.Y - 1; i++)
                {
                    string padding = new string(' ', Size.X - 2); // 기본 여백

                    if (i == Size.Y / 2) // 중간 라인에 텍스트 삽입
                    {
                        // 텍스트의 좌우 패딩 계산
                        int totalPadding = Size.X - 2 - text.Length; // 총 여백 = 박스 내부 폭 - 텍스트 길이
                        int leftPaddingSize = totalPadding / 2;
                        int rightPaddingSize = totalPadding - leftPaddingSize;

                        // 왼쪽과 오른쪽에 패딩 추가
                        string leftPadding = new string(' ', leftPaddingSize);
                        string rightPadding = new string(' ', rightPaddingSize);

                        // 텍스트 삽입
                        sprite[i] = $"#{leftPadding}{text}{rightPadding}#";
                    }
                    else // 일반 여백 줄
                    {
                        sprite[i] = $"#{padding}#";
                    }
                }

                sprite[Size.Y - 1] = horizontalBorder; // 하단 테두리
                return sprite;
            }
        }

        // 마우스 포지션이 버튼 내에 있는지 확인 (예: 간단히 포지션 확인)
        private bool IsMouseOver()
        {
            Vector2<int> mousePosition = new Vector2<int>(Console.CursorLeft, Console.CursorTop);
            Vector2<int> position = GlobalPosition();

            return mousePosition.X >= position.X && mousePosition.X < position.X + Size.X &&
                   mousePosition.Y >= position.Y && mousePosition.Y < position.Y + Size.Y;
        }
    }
}