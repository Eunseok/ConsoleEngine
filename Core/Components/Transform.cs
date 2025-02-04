using Core.MyMath;


namespace Core.Components
{
    public class Transform : Component
    {
        public Vector2<int> Position { get; set; } // 현재 위치
        
        /* 지금은 필요없음
        public Vector2<int> Scale { get; set; } // 스케일 (크기)
        public float Rotation { get; set; } // 각도
        */
        public Vector2<int> BoundsMin { get; set; } // 경계값 (최대 및 최소값)
        public Vector2<int> BoundsMax { get; set; } // 경계값 (최대 및 최소값)
        
        public Transform()
        {
            Position = new Vector2<int>(0, 0);
            // Scale = new Vector2<int>(1, 1);
            // Rotation = 0;
            BoundsMin = new Vector2<int>(1, 1); // 기본 경계 설정
            BoundsMax = new Vector2<int>(Console.WindowWidth-1, Console.WindowHeight-1); // 기본 경계 설정
            
        }

        public void Translate(Vector2<int> delta)
        {
            // 위치 변화 적용
            Position += delta;

            // System.Math를 이용해 경계값을 제한
            Position = new Vector2<int>(
                Math.Clamp(Position.X,BoundsMin.X, BoundsMax.X),
                Math.Clamp(Position.Y,BoundsMin.Y, BoundsMax.Y)
            );
        }

        public void SetPosition(Vector2<int> position)
        {
            Position = position;
        }

        public override string ToString()
        {
            return $"Position: {Position}";
        }
    }
}