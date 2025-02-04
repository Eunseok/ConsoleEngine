using Core.Objects;

namespace Core.Components
{
    public abstract class Component
    {
        public GameObject Owner { get; private set; } = null!; // 이 컴포넌트가 속한 GameObject
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

        // 초기화 함수(필요할 경우 오버라이드)
        public virtual void Initialize()
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


        // GameObject의 메시지를 보냄
        protected void SendMessage(string eventKey, object? data = null)
        {
            Owner?.BroadcastEvent(eventKey, data);
        }

        // 메시지 수신
        public virtual void OnMessageReceived(string eventKey, object data)
        {
            // Script에서 받을 메시지에 대한 처리 로직
        }

        
    }
}