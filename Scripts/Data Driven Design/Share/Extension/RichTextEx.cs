namespace Share
{
    public static class RichTextEx
    {
        public static string Rotate(this string text, float angle)
        {
            return $"<rotate={angle}>{text}</rotate>";
        }
        public static string Alpha(this string text, float alpha)
        {
            return $"<alpha={alpha}>{text}</alpha>";
        }
        public static string Highlight(this string text, string hexColor)
        {
            return $"<highlight=#{hexColor}>{text}</highlight>";
        }
        
        public static string Color(this string text, string hexColor)
        {
            return $"<color=#{hexColor}>{text}</color>";
        }

        public static string Size(this string text, int size)
        {
            return $"<size={size}>{text}</size>";
        }

        public static string Bold(this string text)
        {
            return $"<b>{text}</b>";
        }

        public static string Italic(this string text)
        {
            return $"<i>{text}</i>";
        }

        public static string Underline(this string text)
        {
            return $"<u>{text}</u>";
        }

        public static string Strikethrough(this string text)
        {
            return $"<s>{text}</s>";
        }
        public static string UpperCase(this string text)
        {
            return $"<uppercase>{text}</uppercase>";
        }
        
        public static string LowerCase(this string text)
        {
            return $"<lowercase>{text}</lowercase>";
        }
        
        public static string SmallCaps(this string text)
        {
            return $"<smallcaps>{text}</smallcaps>";
        }

        public static string Material(this string text, string material)
        {
            return $"<material={material}>{text}</material>";
        }

        public static string Sprite(this string text, string sprite)
        {
            return $"<sprite={sprite}>";
        }

        public static string Sprite(this string text, int sprite)
        {
            return $"<sprite={sprite}>";
        }

        public static string Sprite(this string text, string sprite, string hexColor)
        {
            return $"<sprite={sprite} color=#{hexColor}>";
        }

        public static string Sprite(this string text, int sprite, string hexColor)
        {
            return $"<sprite={sprite} color=#{hexColor}>";
        }

        public static string Sprite(this string text, string sprite, string hexColor, int size)
        {
            return $"<sprite={sprite} color=#{hexColor} size={size}>";
        }

        public static string Sprite(this string text, int sprite, string hexColor, int size)
        {
            return $"<sprite={sprite} color=#{hexColor} size={size}>";
        }

        public static string Sprite(this string text, string sprite, string hexColor, int size, int width, int height)
        {
            return $"<sprite={sprite} color=#{hexColor} size={size} width={width} height={height}>";
        }

        public static string Sprite(this string text, int sprite, string hexColor, int size, int width, int height)
        {
            return $"<sprite={sprite} color=#{hexColor} size={size} width={width} height={height}>";
        }
    }

}