using Core.Objects;

namespace Core.Components
{
    public abstract class Component
    {
        public GameObject Owner { get; private set; } = null!;// 이 컴포넌트가 속한 GameObject
        public bool IsActive { get; set; } = true; // 활성화 여부

        // GameObject에 추가될 때 호출
        public virtual void OnAttach(GameObject owner)
        {
            Owner = owner;
        }

        // GameObject에서 제거될 때 호출
        public virtual void OnDetach()
        {
        }

        // 매 프레임 호출될 업데이트(필요할 경우 오버라이드)
        public virtual void Update(float deltaTime)
        {
        }
        
        // 매 프레임 호출될 업데이트(필요할 경우 오버라이드)
        public virtual void Render()
        {
        }

        public virtual void OnMessageReceived(string eventKey, object data) { }

    }
}