using UnityEngine;

namespace Writer.Ex
{
    public static class ColorStringEx
    {
        public static string ToHtmlStringRGBA(this Color color)
        {
            return $"#{ColorUtility.ToHtmlStringRGBA(color)}";
        }
        public static string ToRGBA(this Color color)
        {
            return $"RGBA({color.r}, {color.g}, {color.b}, {color.a})";
        }
    }

}