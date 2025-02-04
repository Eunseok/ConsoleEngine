namespace Core.MyMath
{
    public struct Vector2<T> where T : struct, IComparable, IConvertible, IFormattable
    {
        public T X { get; set; }
        public T Y { get; set; }

        public Vector2(T x, T y)
        {
            X = x;
            Y = y;
        }

        public static Vector2<T> Zero()
        {
            return new Vector2<T>(default, default);
        }
        
        // 왼쪽 (-1, 0)
        public static Vector2<T> Left()
        {
            return new Vector2<T>(ConvertValue(-1), ConvertValue(0));
        }

        // 오른쪽 (1, 0)
        public static Vector2<T> Right()
        {
            return new Vector2<T>(ConvertValue(1), ConvertValue(0));
        }
        // 위쪽 (0, -1)
        public static Vector2<T> Up()
        {
            return new Vector2<T>(ConvertValue(0), ConvertValue(-1));
        }

        // 아래쪽 (0, 1)
        public static Vector2<T> Down()
        {
            return new Vector2<T>(ConvertValue(0), ConvertValue(1));
        }

        private static T ConvertValue(int value)
        {
            // Generic T를 int에서 변환하기 위해 Convert를 사용
            return (T)Convert.ChangeType(value, typeof(T));
        }

        
        public static bool operator ==(Vector2<T> a, Vector2<T> b)
        {
            return (
                (dynamic)a.X == (dynamic)b.X&&
                (dynamic)a.Y == (dynamic)b.Y
            );
        }
        public static bool operator !=(Vector2<T> a, Vector2<T> b)
        {
            return (
                (dynamic)a.X != (dynamic)b.X&&
                (dynamic)a.Y != (dynamic)b.Y
            );
        }
        public static Vector2<T> operator +(Vector2<T> a, Vector2<T> b)
        {
            return new Vector2<T>(
                (dynamic)a.X + (dynamic)b.X,
                (dynamic)a.Y + (dynamic)b.Y
            );
        }

        public static Vector2<T> operator -(Vector2<T> a, Vector2<T> b)
        {
            return new Vector2<T>(
                (dynamic)a.X - (dynamic)b.X,
                (dynamic)a.Y - (dynamic)b.Y
            );
        }

        public static Vector2<T> operator *(Vector2<T> a, Vector2<T> b)
        {
            return new Vector2<T>(
                (dynamic)a.X * (dynamic)b.X,
                (dynamic)a.Y * (dynamic)b.Y
            );
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}