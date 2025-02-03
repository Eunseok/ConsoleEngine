namespace Core.Math;

// T 제너릭 타입을 사용하는 Vector2
// 제한: T는 숫자 타입이어야 함
public class Vector2<T> where T : struct, IComparable, IComparable<T>, IEquatable<T>
{
    public T X { get; set; }
    public T Y { get; set; }

    public Vector2(T x, T y)
    {
        X = x;
        Y = y;
    }

    public Vector2() : this(default, default)
    {
    }

    public Vector2<T> Zero() => new Vector2<T>(default, default);
    public Vector2<T> One() {
        return new Vector2<T>(ConvertValue(1), ConvertValue(1));
    }

    // 왼쪽 방향 (-1, 0)
    public static Vector2<T> Left()
    {
        return new Vector2<T>(ConvertValue(-1), ConvertValue(0));
    }

    // 오른쪽 방향 (1, 0)
    public static Vector2<T> Right()
    {
        return new Vector2<T>(ConvertValue(1), ConvertValue(0));
    }
    
    // 위쪽 방향 (0, -1)
    public static Vector2<T> Up()
    {
        return new Vector2<T>(ConvertValue(0), ConvertValue(-1));
    }

    // 아래쪽 방향 (0, `)
    public static Vector2<T> Down()
    {
        return new Vector2<T>(ConvertValue(0), ConvertValue(1));
    }

    
    public static Vector2<float> AnchorRight()
    {
        return new Vector2<float>(1.0f, 0.0f);
    }

    public static Vector2<float> AnchorLeft()
    {
        return new Vector2<float>(0.0f, 0.0f);
    }

    public static Vector2<float> AnchorCenter()
    {
        return new Vector2<float>(0.5f, 0.5f);
    }




    
    // 연산자 오버로딩 (제너릭 타입간의 연산 지원)
    public static Vector2<T> operator +(Vector2<T> v1, Vector2<T> v2)
    {
        return new Vector2<T>(Add(v1.X, v2.X), Add(v1.Y, v2.Y));
    }

    public static Vector2<T> operator -(Vector2<T> v1, Vector2<T> v2)
    {
        return new Vector2<T>(Subtract(v1.X, v2.X), Subtract(v1.Y, v2.Y));
    }

    public static Vector2<T> operator *(Vector2<T> v1, T scalar)
    {
        return new Vector2<T>(Multiply(v1.X, scalar), Multiply(v1.Y, scalar));
    }

    public static Vector2<T> operator /(Vector2<T> v1, T scalar)
    {
        return new Vector2<T>(Divide(v1.X, scalar), Divide(v1.Y, scalar));
    }

    // 기본 수학 연산은 제너릭으로는 직접적으로 불가능하므로, 아래의 헬퍼 메서드로 처리
    private static T Add(T a, T b) => (dynamic)a + (dynamic)b;
    private static T Subtract(T a, T b) => (dynamic)a - (dynamic)b;
    private static T Multiply(T a, T b) => (dynamic)a * (dynamic)b;
    private static T Divide(T a, T b) => (dynamic)a / (dynamic)b;
    
    
    // 명시적 형변환: Vector2<float> -> Vector2<int>
    public static explicit operator Vector2<int>(Vector2<T> vector)
    {
        return new Vector2<int>(
            ConvertValue<int>(vector.X),
            ConvertValue<int>(vector.Y)
        );
    }
    public static explicit operator Vector2<float>(Vector2<T> vector)
    {
        return new Vector2<float>(
            ConvertValue<float>(vector.X),
            ConvertValue<float>(vector.Y)
        );
    }

   
    // 기본 값을 제너릭 타입 T로 변환하는 헬퍼 메서드
    private static T ConvertValue(int value)
    {
        return (T)Convert.ChangeType(value, typeof(T));
    }
    // 헬퍼 메서드: 타입 변환

    private static U ConvertValue<U>(T value) where U : struct
    {
        return (U)Convert.ChangeType(value, typeof(U));
    }
    

}