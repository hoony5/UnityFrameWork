using UnityEngine;

public static class Vector3Ex 
{
    public static bool ApproximatelyX(this Vector3 a, Vector3 b, float tolerance = 0.001f)
    {
        return Mathf.Abs(a.x - b.x) < tolerance;
    }
    
    public static bool ApproximatelyY(this Vector3 a, Vector3 b, float tolerance = 0.001f)
    {
        return Mathf.Abs(a.y - b.y) < tolerance;
    }
    
    public static bool ApproximatelyZ(this Vector3 a, Vector3 b, float tolerance = 0.001f)
    {
        return Mathf.Abs(a.z - b.z) < tolerance;
    }
    
    public static bool ApproximatelyXY(this Vector3 a, Vector3 b, float tolerance = 0.001f)
    {
        return Mathf.Abs(a.x - b.x) < tolerance &&
               Mathf.Abs(a.y - b.y) < tolerance;
    }
    
    public static bool ApproximatelyXZ(this Vector3 a, Vector3 b, float tolerance = 0.001f)
    {
        return Mathf.Abs(a.x - b.x) < tolerance &&
               Mathf.Abs(a.z - b.z) < tolerance;
    }
    
    public static bool ApproximatelyYZ(this Vector3 a, Vector3 b, float tolerance = 0.001f)
    {
        return Mathf.Abs(a.y - b.y) < tolerance &&
               Mathf.Abs(a.z - b.z) < tolerance;
    }
    
    
    public static bool Approximately(this Vector3 a, Vector3 b, float tolerance = 0.001f)
    {
        return Mathf.Abs(a.x - b.x) < tolerance &&
               Mathf.Abs(a.y - b.y) < tolerance &&
               Mathf.Abs(a.z - b.z) < tolerance;
    }
    
    public static Vector3 SetX(this Vector3 a, float x)
    {
        return new Vector3(x, a.y, a.z);
    }
    
    public static Vector3 SetY(this Vector3 a, float y)
    {
        return new Vector3(a.x, y, a.z);
    }
    
    public static Vector3 SetZ(this Vector3 a, float z)
    {
        return new Vector3(a.x, a.y, z);
    }
    
    // Additional extension methods for Vector3
    public static Vector3 AddX(this Vector3 a, float x)
    {
        return new Vector3(a.x + x, a.y, a.z);
    }
    
    public static Vector3 AddY(this Vector3 a, float y)
    {
        return new Vector3(a.x, a.y + y, a.z);
    }
    
    public static Vector3 AddZ(this Vector3 a, float z)
    {
        return new Vector3(a.x, a.y, a.z + z);
    }
    
    public static Vector3 MultiplyX(this Vector3 a, float x)
    {
        return new Vector3(a.x * x, a.y, a.z);
    }
    
    public static Vector3 MultiplyY(this Vector3 a, float y)
    {
        return new Vector3(a.x, a.y * y, a.z);
    }
    
    public static Vector3 MultiplyZ(this Vector3 a, float z)
    {
        return new Vector3(a.x, a.y, a.z * z);
    }
    
    public static Vector3 DivideX(this Vector3 a, float x)
    {
        return new Vector3(a.x / x, a.y, a.z);
    }
    
    public static Vector3 DivideY(this Vector3 a, float y)
    {
        return new Vector3(a.x, a.y / y, a.z);
    }
    
    public static Vector3 DivideZ(this Vector3 a, float z)
    {
        return new Vector3(a.x, a.y, a.z / z);
    }
    
    public static Vector3 Add(this Vector3 a, Vector3 b)
    {
        return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
    }
    
    public static Vector3 Subtract(this Vector3 a, Vector3 b)
    {
        return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
    }
    
    public static Vector3 Multiply(this Vector3 a, Vector3 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }
    
    public static Vector3 Divide(this Vector3 a, Vector3 b)
    {
        return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
    }
    
    public static Vector3 Abs(this Vector3 a)
    {
        return new Vector3(Mathf.Abs(a.x), Mathf.Abs(a.y), Mathf.Abs(a.z));
    }
    
    public static Vector3 Floor(this Vector3 a)
    {
        return new Vector3(Mathf.Floor(a.x), Mathf.Floor(a.y), Mathf.Floor(a.z));
    }
    
    public static Vector3 Ceil(this Vector3 a)
    {
        return new Vector3(Mathf.Ceil(a.x), Mathf.Ceil(a.y), Mathf.Ceil(a.z));
    }
    
    public static Vector3 Round(this Vector3 a)
    {
        return new Vector3(Mathf.Round(a.x), Mathf.Round(a.y), Mathf.Round(a.z));
    }
    
    public static Vector3 Clamp(this Vector3 a, Vector3 min, Vector3 max)
    {
        return new Vector3(Mathf.Clamp(a.x, min.x, max.x), Mathf.Clamp(a.y, min.y, max.y), Mathf.Clamp(a.z, min.z, max.z));
    }
    
    public static Vector3 Clamp01(this Vector3 a)
    {
        return new Vector3(Mathf.Clamp01(a.x), Mathf.Clamp01(a.y), Mathf.Clamp01(a.z));
    }
    
    public static Vector3 SwizzleXY(this Vector3 a)
    {
        return new Vector3(a.y, a.x, a.z);
    }
     
    public static Vector3 SwizzleXZ(this Vector3 a)
    {
        return new Vector3(a.z, a.y, a.x);
    }
    
    public static Vector3 SwizzleYZ(this Vector3 a)
    {
        return new Vector3(a.x, a.z, a.y);
    }
    
    public static Vector3 PointTo(this Vector3 a, Vector3 b)
    {
        return b - a;
    }
    
    public static Vector3 LerpX(this Vector3 a, Vector3 b, float t, float max = 1)
    {
        if (max <= 0) max = 1;
        return new Vector3(Mathf.Lerp(a.x, b.x, Mathf.Clamp01(t / max)), a.y, a.z);
    }
    
    public static Vector3 LerpY(this Vector3 a, Vector3 b, float t, float max = 1)
    {
        if (max <= 0) max = 1;
        return new Vector3(a.x, Mathf.Lerp(a.y, b.y, Mathf.Clamp01(t / max)), a.z);
    }
    
    public static Vector3 LerpZ(this Vector3 a, Vector3 b, float t, float max = 1)
    {
        if (max <= 0) max = 1;
        return new Vector3(a.x, a.y, Mathf.Lerp(a.z, b.z, Mathf.Clamp01(t / max)));
    }
    
    public static Vector3 LerpXY(this Vector3 a, Vector3 b, float t, float max = 1)
    {
        if (max <= 0) max = 1;
        float progress = Mathf.Clamp01(t / max);
        return new Vector3(Mathf.Lerp(a.x, b.x, progress), Mathf.Lerp(a.y, b.y, progress), a.z);
    }
    
    public static Vector3 LerpXZ(this Vector3 a, Vector3 b, float t, float max = 1)
    {
        if (max <= 0) max = 1;
        float progress = Mathf.Clamp01(t / max);
        return new Vector3(Mathf.Lerp(a.x, b.x, progress), a.y, Mathf.Lerp(a.z, b.z, progress));
    }
    
    public static Vector3 LerpYZ(this Vector3 a, Vector3 b, float t, float max = 1)
    {
        if (max <= 0) max = 1;
        float progress = Mathf.Clamp01(t / max);
        return new Vector3(a.x, Mathf.Lerp(a.y, b.y, progress), Mathf.Lerp(a.z, b.z, progress));
    }
    
    public static Vector3 Lerp(this Vector3 a, Vector3 b, float t, float max = 1)
    {
        if (max <= 0) max = 1;
        float progress = Mathf.Clamp01(t / max);
        return new Vector3(Mathf.Lerp(a.x, b.x, progress), Mathf.Lerp(a.y, b.y, progress), Mathf.Lerp(a.z, b.z, progress));
    }
}