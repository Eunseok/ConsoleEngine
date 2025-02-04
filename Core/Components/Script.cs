
namespace Core.Components
{
    // 사용자 정의 스크립트의 기본 틀 제공
    public abstract class Script : Component
    {
        // 프레임별 호출
        public override void Update(float deltaTime)
        {
            OnUpdate(deltaTime);
        }

        // 사용자 스크립트에서 구현
        protected virtual void OnUpdate(float deltaTime)
        {
        }
        

    }
}