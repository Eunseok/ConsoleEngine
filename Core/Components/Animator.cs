using Core.Graphics;

namespace Core.Components
{
    public class Animator : Component
    {
        private Animation _animation;

        public Animator() : base("Animator")
        {
          
        }

        public void SetAnimation(Animation animation)
        {
            _animation = animation;
        }
        public override void Update(float deltaTime)
        {
            _animation.Update(deltaTime);
            Owner!.GetComponent<Renderer>().Sprite = _animation.CurrentSprite;
        }
    }
}