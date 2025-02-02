
namespace Core.Graphics;

public class Animation
{
    private List<AnimationFrame> _frames;
    private int _currentFrame;
    private float _timeSinceLastFrame;

    public Sprite CurrentSprite => _frames[_currentFrame].Sprite;
    public Animation(List<AnimationFrame> frames)
    {
        _frames = frames;
        _currentFrame = 0;
        _timeSinceLastFrame = 0;
    }



    public void Update(float deltaTime)
    {
        _timeSinceLastFrame += deltaTime;

        if (_timeSinceLastFrame >= _frames[_currentFrame].Duration)
        {
            _timeSinceLastFrame = 0;
            _currentFrame = (_currentFrame + 1) % _frames.Count; // 다음 프레임으로 이동
        }
    }
}