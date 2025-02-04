using Core.MyMath;
using System;

namespace Core.Components
{
    public class Rigidbody : Component
    {
        public Vector2<int> Velocity { get; private set; } // 속도
        public Vector2<int> Acceleration { get; private set; } // 가속도
        public bool UseGravity { get; set; } // 중력 사용 여부
        private int Friction { get; set; } // 마찰 계수

        public Vector2<int> Gravity { get; set; } // 중력 값

        public Rigidbody()
        {
            Velocity = new Vector2<int>(0, 0);
            Acceleration = new Vector2<int>(0, 0);
            Gravity = new Vector2<int>(0, -1); // 아래 방향 중력
            UseGravity = true;
            Friction = 1; // 기본 마찰력 적용
        }

        public override void Update(float deltaTime)
        {
            if (UseGravity)
            {
                AddForce(Gravity); // 중력 적용
            }

            // 가속도로 인해 속도 증가
            Velocity += Acceleration;

            // 마찰력 적용
            ApplyFriction();

            // Transform 컴포넌트를 호출해 위치 변경
            var transform = Owner?.GetComponent<Transform>();
            if (transform != null)
            {
                transform.Translate(Velocity);
            }
        }

        public void AddForce(Vector2<int> force)
        {
            Acceleration += force; // 외부 힘으로 가속도 증가
        }

        private void ApplyFriction()
        {
            // 마찰력을 적용해 속도를 줄인다
            Velocity = new Vector2<int>(
                Math.Max(0, Math.Abs(Velocity.X) - Friction) * Math.Sign(Velocity.X),
                Math.Max(0, Math.Abs(Velocity.Y) - Friction) * Math.Sign(Velocity.Y)
            );

            // 속도가 0이 되면 가속도도 초기화
            if (Velocity.X == 0 && Velocity.Y == 0)
            {
                Acceleration = new Vector2<int>(0, 0);
            }
        }

        public override string ToString()
        {
            return $"Velocity: {Velocity}, Acceleration: {Acceleration}";
        }
    }
}