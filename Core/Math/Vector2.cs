namespace Core.Math;

// Vector2: 2D 좌표를 나타내는 클래스
public class Vector2
{
    public int X { get; set; }
    public int Y { get; set; }

    public Vector2(int x = 0, int y = 0)
    {
        X = x;
        Y = y;
    }

    public Vector2 Zero()
    {
        return new Vector2(0, 0);
    }
    
    public Vector2 One()
    {
        return new Vector2(1, 1);
    }
    
    public Vector2 Left()
    {
        return new Vector2(-1, 0);
    }
    public Vector2 Right()
    {
        return new Vector2(1, 0);
    }
    public Vector2 Up()
    {
        return new Vector2(0, -1);
    }
    public Vector2 Down()
    {
        return new Vector2(0, 1);
    }
    
    public static Vector2 operator +(Vector2 v1, Vector2 v2)
    {
        return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
    }
    
    public static Vector2 operator -(Vector2 v1, Vector2 v2)
    {
        return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
    }
    
    public static Vector2 operator *(Vector2 v1, Vector2 v2)
    {
        return new Vector2(v1.X * v2.X, v1.Y * v2.Y);
    }
    
    public static Vector2 operator /(Vector2 v1, Vector2 v2)
    {
        return new Vector2(v1.X / v2.X, v1.Y / v2.Y);
    }
}
