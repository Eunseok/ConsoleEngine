using Core.Graphics;

namespace Core.Components
{
    public class Animator : Component
    {
        private  Animation? _animation;
        
        public void StartAnimation()
        {
            _animation?.StartAnimation();
        }
        public void StopAnimation()
        {
            _animation?.StopAnimation();
        }

        public void SetAnimation(Animation animation)
        {
            _animation = animation;
        }

        public override void Update(float deltaTime)
        {
            if (_animation == null)
                return;

            // Animation 업데이트
            _animation.Update(deltaTime);

            // 현재 프레임 데이터를 Renderer로 전송
            var renderer = Owner?.GetComponent<Renderer>();
            if (renderer != null && _animation.CurrentSprite != null)
            {
                renderer.SetSprite(_animation.CurrentSprite); // 현재 Sprite를 Renderer에 반영
            }
        }
    }
}