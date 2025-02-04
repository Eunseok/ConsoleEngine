
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
        

        // GameObject의 메시지를 보냄
        protected void SendMessage(string eventKey, object? data = null)
        {
            Owner?.BroadcastEvent(eventKey, data);
        }

        // 메시지 수신
        public override void OnMessageReceived(string eventKey, object data)
        {
            // Script에서 받을 메시지에 대한 처리 로직
            Console.WriteLine($"Received {eventKey} with data: {data}");
        }

    }
}