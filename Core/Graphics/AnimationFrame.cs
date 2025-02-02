namespace Core.Graphics;

public class AnimationFrame
{
    public Sprite Sprite { get; }
    public int Duration { get; } // 프레임 지속 시간 (밀리초)

    public AnimationFrame(Sprite sprite, int duration)
    {
        Sprite = sprite;
        Duration = duration;
    }
}