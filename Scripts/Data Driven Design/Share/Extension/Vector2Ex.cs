using UnityEngine;

public static class Vector2Ex 
{
    public static bool ApproximatelyX(this Vector2 a, Vector2 b, float tolerance = 0.001f)
    {
        return Mathf.Abs(a.x - b.x) < tolerance;
    }
    
    public static bool ApproximatelyY(this Vector2 a, Vector2 b, float tolerance = 0.001f)
    {
        return Mathf.Abs(a.y - b.y) < tolerance;
    }
    public static bool Approximately(this Vector2 a, Vector2 b, float tolerance = 0.001f)
    {
        return Mathf.Abs(a.x - b.x) < tolerance &&
               Mathf.Abs(a.y - b.y) < tolerance;
    }
    
    public static Vector2 SetX(this Vector2 a, float x)
    {
        return new Vector2(x, a.y);
    }
    
    public static Vector2 SetY(this Vector2 a, float y)
    {
        return new Vector2(a.x, y);
    }
    
    // Additional extension methods for Vector2
    public static Vector2 AddX(this Vector2 a, float x)
    {
        return new Vector2(a.x + x, a.y);
    }
    
    public static Vector2 AddY(this Vector2 a, float y)
    {
        return new Vector2(a.x, a.y + y);
    }
    
    public static Vector2 MultiplyX(this Vector2 a, float x)
    {
        return new Vector2(a.x * x, a.y);
    }
    
    public static Vector2 MultiplyY(this Vector2 a, float y)
    {
        return new Vector2(a.x, a.y * y);
    }
    
    public static Vector2 DivideX(this Vector2 a, float x)
    {
        return x == 0 ? Vector2.zero : new Vector2(a.x / x, a.y);
    }
    
    public static Vector2 DivideY(this Vector2 a, float y)
    {
        return y == 0 ? Vector2.zero : new Vector2(a.x, a.y / y);
    }
    
    public static Vector2 Add(this Vector2 a, Vector2 b)
    {
        return new Vector2(a.x + b.x, a.y + b.y);
    }
    
    public static Vector2 Subtract(this Vector2 a, Vector2 b)
    {
        return new Vector2(a.x - b.x, a.y - b.y);
    }
    
    public static Vector2 Multiply(this Vector2 a, Vector2 b)
    {
        return new Vector2(a.x * b.x, a.y * b.y);
    }
    
    public static Vector2 Divide(this Vector2 a, Vector2 b)
    {
        return b.x == 0 || b.y == 0 ? Vector2.zero : new Vector2(a.x / b.x, a.y / b.y);
    }
    
    public static Vector2 Abs(this Vector2 a)
    {
        return new Vector2(Mathf.Abs(a.x), Mathf.Abs(a.y));
    }
    
    public static Vector2 Floor(this Vector2 a)
    {
        return new Vector2(Mathf.Floor(a.x), Mathf.Floor(a.y));
    }
    
    public static Vector2 Ceil(this Vector2 a)
    {
        return new Vector2(Mathf.Ceil(a.x), Mathf.Ceil(a.y));
    }
    
    public static Vector2 Round(this Vector2 a)
    {
        return new Vector2(Mathf.Round(a.x), Mathf.Round(a.y));
    }
    
    public static Vector2 Clamp(this Vector2 a, Vector2 min, Vector2 max)
    {
        return new Vector2(Mathf.Clamp(a.x, min.x, max.x), Mathf.Clamp(a.y, min.y, max.y));
    }
    
    public static Vector2 Clamp01(this Vector2 a)
    {
        return new Vector2(Mathf.Clamp01(a.x), Mathf.Clamp01(a.y));
    }
    
    public static Vector2 SwizzleXY(this Vector2 a)
    {
        return new Vector2(a.y, a.x);
    }
    
    public static Vector2 PointTo(this Vector2 a, Vector2 b)
    {
        return b - a;
    }
    
    public static Vector2 LerpX(this Vector2 a, Vector2 b, float t, float max = 1)
    {
        if (max <= 0) max = 1;
        return new Vector2(Mathf.Lerp(a.x, b.x, Mathf.Clamp01(t / max)), a.y);
    }
    
    public static Vector2 LerpY(this Vector2 a, Vector2 b, float t, float max = 1)
    {
        if (max <= 0) max = 1;
        return new Vector2(a.x, Mathf.Lerp(a.y, b.y, Mathf.Clamp01(t / max)));
    }
    
    public static Vector2 LerpXY(this Vector2 a, Vector2 b, float t, float max = 1)
    {
        if (max <= 0) max = 1;
        float progress = Mathf.Clamp01(t / max);
        return new Vector2(Mathf.Lerp(a.x, b.x, progress), Mathf.Lerp(a.y, b.y, progress));
    }
    
    public static Vector2 Lerp(this Vector2 a, Vector2 b, float t, float max = 1)
    {
        if (max <= 0) max = 1;
        float progress = Mathf.Clamp01(t / max);
        return new Vector2(Mathf.Lerp(a.x, b.x, progress), Mathf.Lerp(a.y, b.y, progress));
    }
}