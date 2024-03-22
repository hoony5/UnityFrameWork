using UnityEngine;

namespace Share
{
    public static class ColorEx
    {
        public static Color SetAlpha(this Color color, float alpha)
        {
            return new Color(color.r, color.g, color.b, alpha);
        }
        public static Color SetRed(this Color color, float red)
        {
            return new Color(red, color.g, color.b, color.a);
        }
        public static Color SetGreen(this Color color, float green)
        {
            return new Color(color.r, green, color.b, color.a);
        }
        public static Color SetBlue(this Color color, float blue)
        {
            return new Color(color.r, color.g, blue, color.a);
        }
        public static Color SetRGB(this Color color, float red, float green, float blue)
        {
            return new Color(red, green, blue, color.a);
        }
        public static Color SetRGB(this Color color, Color rgb)
        {
            return new Color(rgb.r, rgb.g, rgb.b, color.a);
        }
        public static Color SetAlpha(this Color color, Color alpha)
        {
            return new Color(color.r, color.g, color.b, alpha.a);
        }
        public static Color SetAlpha(this Color color, int alpha)
        {
            return new Color(color.r, color.g, color.b, alpha);
        }
    
        #region Color Map
        public static Color SetRed()
        {
            return Color.red;
        }
        public static Color SetGreen()
        {
            return Color.green;
        }
        public static Color SetBlue()
        {
            return Color.blue;
        }
        public static Color SetCharcoal()
        {
            return new Color( 0.21f, 0.27f, 0.31f, 1f);
        }
        public static Color SetCobalt()
        {
            return new Color( 0f, 0.28f, 0.67f, 1f);
        }
        public static Color SetCrimson()
        {
            return new Color( 0.86f, 0.08f, 0.24f, 1f);
        }
        public static Color SetBlueViolet()
        {
            return new Color( 0.47f, 0.57f, 0.89f, 1f);
        }

        public static Color SetRedViolet()
        {
            return new Color(0.77f, 0.57f, 0.89f, 1f);
        }
        public static Color SetCream()
        {
            return new Color( 1f, 0.99f, 0.82f, 1f);
        }
        public static Color SetMustard()
        {
            return new Color( 1f, 0.86f, 0.35f, 1f);
        }
        public static Color SetJade()
        {
            return new Color( 0f, 0.66f, 0.42f, 1f);
        }
        public static Color SetSalmon()
        {
            return new Color( 0.98f, 0.5f, 0.45f, 1f);
        }
        public static Color SetOcher()
        {
            return new Color( 0.8f, 0.47f, 0.13f, 1f);
        }
        public static Color SetBronze() 
        {
            return new Color( 0.8f, 0.5f, 0.2f, 1f);
        }
        public static Color SetPistachio()
        {
            return new Color( 0.58f, 0.77f, 0.45f, 1f);
        }
        public static Color SetPeagreen()
        {
            return new Color( 0.2f, 0.8f, 0.2f, 1f);
        }
        public static Color SetLilac()
        {
            return new Color( 0.78f, 0.64f, 0.78f, 1f);
        }
        public static Color SetGrape()
        {
            return new Color( 0.56f, 0.37f, 0.57f, 1f);
        }
        public static Color SetPlum()
        {
            return new Color( 0.56f, 0.27f, 0.52f, 1f);
        }
        public static Color SetPeach()
        {
            return new Color( 1f, 0.7f, 0.7f, 1f);
        }
        public static Color SetIvory()
        {
            return new Color( 1f, 1f, 0.85f, 1f);
        }
    
        public static Color SetAzure()
        {
            return new Color( 0f, 0.5f, 1f, 1f);
        }

        public static Color SetBeige()
        {
            return new Color( 0.96f, 0.96f, 0.86f, 1f);
        }
    
        public static Color SetWhite()
        {
            return Color.white;
        }
        public static Color SetBlack()
        {
            return Color.black;
        }
        public static Color SetOrange()
        {
            return new Color(1f, 0.5f, 0f, 1f);
        }
        public static Color SetYellow()
        {
            return Color.yellow;
        }
        public static Color SetEmerald()
        {
            return new Color(0.31f, 0.78f, 0.47f, 1f);
        }
        public static Color SetForestGreen()
        {
            return new Color(0.13f, 0.55f, 0.13f, 1f);
        }
        public static Color SetLimeGreen()
        {
            return new Color(0.2f, 0.8f, 0.2f, 1f);
        }
        public static Color SetOliveGreen()
        {
            return new Color(0.33f, 0.42f, 0.18f, 1f);
        }
        public static Color SetSeaGreen()
        {
            return new Color(0.18f, 0.55f, 0.34f, 1f);
        }
        public static Color SetSpringGreen()
        {
            return new Color(0f, 1f, 0.5f, 1f);
        }
        public static Color SetDarkTurquoise()
        {
            return new Color(0f, 0.81f, 0.82f, 1f);
        }
        public static Color SetMediumTurquoise()
        {
            return new Color(0.28f, 0.82f, 0.8f, 1f);
        }
        public static Color SetLightSeaGreen()
        {
            return new Color(0.13f, 0.7f, 0.67f, 1f);
        }
        public static Color SetDarkSlateGray()
        {
            return new Color(0.18f, 0.31f, 0.31f, 1f);
        }
        public static Color SetAquamarine()
        {
            return new Color( 0.5f, 1f, 1f, 1f);
        }
        public static Color SetBrown()
        {
            return new Color(0.5f, 0.25f, 0f, 1f);
        }
        public static Color SetGold()
        {
            return new Color(1f, 0.84f, 0f, 1f);
        }

        public static Color SetPastelRed()
        {
            return new Color( 1f, 0.41f, 0.38f, 1f);
        }
        public static Color SetPastelOrange()
        {
            return new Color(1f, 0.7f, 0.28f, 1f);
        }
        public static Color SetPastelYellow()
        {
            return new Color(1f, 0.93f, 0.55f, 1f);
        }
        public static Color SetPastelGreen()
        {
            return new Color(0.47f, 0.87f, 0.47f, 1f);
        }
        public static Color SetPastelBlue()
        {
            return new Color(0.68f, 0.78f, 0.81f, 1f);
        }
        public static Color SetPastelPurple()
        {
            return new Color(0.7f, 0.62f, 0.87f, 1f);
        }
        public static Color SetPastelPink()
        {
            return new Color(1f, 0.68f, 0.79f, 1f);
        }
        public static Color SetPastelBrown()
        {
            return new Color(0.51f, 0.41f, 0.33f, 1f);
        }
        public static Color SetPastelGray()
        {
            return new Color(0.81f, 0.81f, 0.77f, 1f);
        }
        public static Color SetPastelCyan()
        {
            return new Color(0.54f, 0.87f, 0.81f, 1f);
        }
        public static Color SetPastelMagenta()
        {
            return new Color(0.98f, 0.51f, 0.78f, 1f);
        }
        public static Color SetPastelTeal()
        {
            return new Color(0.36f, 0.7f, 0.66f, 1f);
        }
        public static Color SetPastelViolet()
        {
            return new Color(0.8f, 0.6f, 0.79f, 1f);
        }
        public static Color SetPastelIndigo()
        {
            return new Color(0.5f, 0.69f, 0.89f, 1f);
        }
        public static Color SetPastelMaroon()
        {
            return new Color(0.76f, 0.31f, 0.34f, 1f);
        }
        public static Color SetPastelOlive()
        {
            return new Color(0.6f, 0.6f, 0.42f, 1f);
        }
        public static Color SetPastelNavy()
        {
            return new Color(0.4f, 0.4f, 0.6f, 1f);
        }
        public static Color SetPastelLime()
        {
            return new Color(0.6f, 0.8f, 0.2f, 1f);
        }
        public static Color SetPastelSkyBlue()
        {
            return new Color(0.47f, 0.8f, 0.89f, 1f);
        }
        public static Color SetPastelDarkGreen()
        {
            return new Color(0.01f, 0.75f, 0.24f, 1f);
        }
        public static Color SetPastelDarkBlue()
        {
            return new Color(0.01f, 0.24f, 0.75f, 1f);
        }
        public static Color SetPastelDarkRed()
        {
            return new Color(0.75f, 0.01f, 0.01f, 1f);
        }
        public static Color SetPastelDarkGray()
        {
            return new Color(0.5f, 0.5f, 0.5f, 1f);
        }
        public static Color SetPastelDarkGrey()
        {
            return new Color(0.5f, 0.5f, 0.5f, 1f);
        }
        public static Color SetPastelLightGray()
        {
            return new Color(0.75f, 0.75f, 0.75f, 1f);
        }
        public static Color SetPastelDarkCyan()
        {
            return new Color(0.01f, 0.5f, 0.5f, 1f);
        }
        public static Color SetPastelDarkMagenta()
        {
            return new Color(0.5f, 0.01f, 0.5f, 1f);
        }
        public static Color SetPastelDarkOrange()
        {
            return new Color(0.75f, 0.5f, 0.01f, 1f);
        }
        public static Color SetPastelDarkYellow()
        {
            return new Color(0.5f, 0.5f, 0.01f, 1f);
        }
        public static Color SetPastelLightCyan()
        {
            return new Color(0.5f, 0.75f, 0.75f, 1f);
        }
        public static Color SetPastelLightMagenta()
        {
            return new Color(0.75f, 0.5f, 0.75f, 1f);
        }
        public static Color SetPastelLightOrange()
        {
            return new Color(0.75f, 0.5f, 0.01f, 1f);
        }
        public static Color SetPastelLightYellow()
        {
            return new Color(0.75f, 0.75f, 0.5f, 1f);
        }
        
        public static Color SetPastelCoral()
        {
            return new Color(1f, 0.5f, 0.31f, 1f);
        }
        public static Color SetPastelTealBlue()
        {
            return new Color(0.21f, 0.46f, 0.53f, 1f);
        }
        public static Color SetPastelLavender()
        {
            return new Color(0.7f, 0.61f, 0.76f, 1f);
        }
        public static Color SetPastelPinkOrange()
        {
            return new Color(1f, 0.7f, 0.68f, 1f);
        }
        public static Color SetPastelPinkYellow()
        {
            return new Color(1f, 0.93f, 0.68f, 1f);
        }
        public static Color SetPastelPinkPurple()
        {
            return new Color(0.7f, 0.61f, 0.76f, 1f);
        }
        public static Color SetPastelPinkGreen()
        {
            return new Color(0.7f, 0.87f, 0.76f, 1f);
        }
        public static Color SetPastelPinkBlue()
        {
            return new Color(0.68f, 0.78f, 0.81f, 1f);
        }
        public static Color SetPastelPinkBrown()
        {
            return new Color(0.76f, 0.61f, 0.53f, 1f);
        }
        public static Color SetPastelPinkGray()
        {
            return new Color(0.81f, 0.81f, 0.77f, 1f);
        }
        public static Color SetPastelPinkCyan()
        {
            return new Color(0.68f, 0.87f, 0.81f, 1f);
        }
        public static Color SetPastelPinkMagenta()
        {
            return new Color(0.98f, 0.61f, 0.78f, 1f);
        }
        public static Color SetPastelPinkTeal()
        {
            return new Color(0.66f, 0.87f, 0.81f, 1f);
        }
        public static Color SetPastelPinkViolet()
        {
            return new Color(0.8f, 0.6f, 0.79f, 1f);
        }
        
        public static Color SetPastelCoralRed()
        {
            return new Color(1f, 0.5f, 0.31f, 1f);
        }
        public static Color SetPastelCoralOrange()
        {
            return new Color(1f, 0.7f, 0.28f, 1f);
        }
        public static Color SetPastelCoralYellow()
        {
            return new Color(1f, 0.93f, 0.55f, 1f);
        }
        public static Color SetPastelCoralGreen()
        {
            return new Color(0.47f, 0.87f, 0.47f, 1f);
        }
        public static Color SetPastelCoralBlue()
        {
            return new Color(0.68f, 0.78f, 0.81f, 1f);
        }
        public static Color SetPastelCoralPurple()
        {
            return new Color(0.7f, 0.62f, 0.87f, 1f);
        }
        public static Color SetPastelCoralPink()
        {
            return new Color(1f, 0.68f, 0.79f, 1f);
        }
        public static Color SetPastelCoralBrown()
        {
            return new Color(0.51f, 0.41f, 0.33f, 1f);
        }
        public static Color SetPastelCoralGray()
        {
            return new Color(0.81f, 0.81f, 0.77f, 1f);
        }
        
        public static Color SetPastelTealRed()
        {
            return new Color(1f, 0.5f, 0.31f, 1f);
        }
        public static Color SetPastelTealOrange()
        {
            return new Color(1f, 0.7f, 0.28f, 1f);
        }
        public static Color SetPastelTealYellow()
        {
            return new Color(1f, 0.93f, 0.55f, 1f);
        }
        public static Color SetPastelTealGreen()
        {
            return new Color(0.47f, 0.87f, 0.47f, 1f);
        }
        public static Color SetPastelTealPurple()
        {
            return new Color(0.7f, 0.62f, 0.87f, 1f);
        }
        public static Color SetPastelTealPink()
        {
            return new Color(1f, 0.68f, 0.79f, 1f);
        }
        public static Color SetPastelTealBrown()
        {
            return new Color(0.51f, 0.41f, 0.33f, 1f);
        }
        public static Color SetPastelTealGray()
        {
            return new Color(0.81f, 0.81f, 0.77f, 1f);
        }
        public static Color SetTintRed()
        {
            return new Color(1f, 0.5f, 0.31f, 1f);
        }
        public static Color SetTintOrange()
        {
            return new Color(1f, 0.7f, 0.28f, 1f);
        }
        public static Color SetTintYellow()
        {
            return new Color(1f, 0.93f, 0.55f, 1f);
        }
        public static Color SetTintGreen()
        {
            return new Color(0.47f, 0.87f, 0.47f, 1f);
        }
        public static Color SetTintBlue()
        {
            return new Color(0.68f, 0.78f, 0.81f, 1f);
        }
        public static Color SetTintPurple()
        {
            return new Color(0.7f, 0.62f, 0.87f, 1f);
        }
        public static Color SetTintPink()
        {
            return new Color(1f, 0.68f, 0.79f, 1f);
        }
        public static Color SetTintBrown()
        {
            return new Color(0.51f, 0.41f, 0.33f, 1f);
        }
        public static Color SetTintGray()
        {
            return new Color(0.81f, 0.81f, 0.77f, 1f);
        }
        public static Color SetTintCyan()
        {
            return new Color(0.54f, 0.87f, 0.81f, 1f);
        }
        public static Color SetTintMagenta()
        {
            return new Color(0.98f, 0.51f, 0.78f, 1f);
        }
        public static Color SetTintTeal()
        {
            return new Color(0.36f, 0.7f, 0.66f, 1f);
        }
        public static Color SetTintViolet()
        {
            return new Color(0.8f, 0.6f, 0.79f, 1f);
        }
        public static Color SetTintIndigo()
        {
            return new Color(0.5f, 0.69f, 0.89f, 1f);
        }
        public static Color SetTintMaroon()
        {
            return new Color(0.76f, 0.31f, 0.34f, 1f);
        }
        public static Color SetTintOlive()
        {
            return new Color(0.6f, 0.6f, 0.42f, 1f);
        }
        public static Color SetTintNavy()
        {
            return new Color(0.4f, 0.4f, 0.6f, 1f);
        }
        public static Color SetTintLime()
        {
            return new Color(0.6f, 0.8f, 0.2f, 1f);
        }
        public static Color SetTintSkyBlue()
        {
            return new Color(0.47f, 0.8f, 0.89f, 1f);
        }
        public static Color SetTintDarkGreen()
        {
            return new Color(0.01f, 0.75f, 0.24f, 1f);
        }
        public static Color SetVividRed()
        {
            return new Color(0.96f, 0.27f, 0.21f, 1f);
        }
        public static Color SetVividOrange()
        {
            return new Color(0.96f, 0.46f, 0.21f, 1f);
        }
        public static Color SetVividYellow()
        {
            return new Color(0.96f, 0.76f, 0.21f, 1f);
        }
        public static Color SetVividGreen()
        {
            return new Color(0.21f, 0.96f, 0.27f, 1f);
        }
        public static Color SetVividBlue()
        {
            return new Color(0.21f, 0.96f, 0.76f, 1f);
        }
        public static Color SetVividPurple()
        {
            return new Color(0.46f, 0.21f, 0.96f, 1f);
        }
        public static Color SetVividPink()
        {
            return new Color(0.96f, 0.21f, 0.46f, 1f);
        }
        public static Color SetVividBrown()
        {
            return new Color(0.46f, 0.27f, 0.21f, 1f);
        }
        public static Color SetVividGray()
        {
            return new Color(0.46f, 0.46f, 0.46f, 1f);
        }
        public static Color SetVividCyan()
        {
            return new Color(0.21f, 0.96f, 0.76f, 1f);
        }
        public static Color SetVividMagenta()
        {
            return new Color(0.96f, 0.21f, 0.76f, 1f);
        }
        public static Color SetVividTeal()
        {
            return new Color(0.21f, 0.76f, 0.96f, 1f);
        }
        public static Color SetVividViolet()
        {
            return new Color(0.76f, 0.21f, 0.96f, 1f);
        }
        public static Color SetVividIndigo()
        {
            return new Color(0.27f, 0.21f, 0.96f, 1f);
        }
        public static Color SetVividMaroon()
        {
            return new Color(0.76f, 0.21f, 0.27f, 1f);
        }
        public static Color SetVividOlive()
        {
            return new Color(0.76f, 0.76f, 0.21f, 1f);
        }
        public static Color SetVividNavy()
        {
            return new Color(0.21f, 0.21f, 0.76f, 1f);
        }
        public static Color SetVividLime()
        {
            return new Color(0.76f, 0.96f, 0.21f, 1f);
        }
        public static Color SetVividSkyBlue()
        {
            return new Color(0.21f, 0.76f, 0.96f, 1f);
        }
        public static Color SetVividDarkGreen()
        {
            return new Color(0.01f, 0.75f, 0.24f, 1f);
        }
        public static Color SetVividDarkBlue()
        {
            return new Color(0.01f, 0.24f, 0.75f, 1f);
        }
        public static Color SetVividDarkRed()
        {
            return new Color(0.75f, 0.01f, 0.01f, 1f);
        }
        public static Color SetVividDarkGray()
        {
            return new Color(0.5f, 0.5f, 0.5f, 1f);
        }
        public static Color SetVividDarkGrey()
        {
            return new Color(0.5f, 0.5f, 0.5f, 1f);
        }
        public static Color SetVividLightGray()
        {
            return new Color(0.75f, 0.75f, 0.75f, 1f);
        }
        public static Color SetVividDarkCyan()
        {
            return new Color(0.01f, 0.5f, 0.5f, 1f);
        }
        public static Color SetVividDarkMagenta()
        {
            return new Color(0.5f, 0.01f, 0.5f, 1f);
        }
        public static Color SetVividDarkOrange()
        {
            return new Color(0.75f, 0.5f, 0.01f, 1f);
        }
        public static Color SetVividDarkYellow()
        {
            return new Color(0.5f, 0.5f, 0.01f, 1f);
        }
        public static Color SetVividLightCyan()
        {
            return new Color(0.5f, 0.75f, 0.75f, 1f);
        }
        public static Color SetVividLightMagenta()
        {
            return new Color(0.75f, 0.5f, 0.75f, 1f);
        }
        public static Color SetVividLightOrange()
        {
            return new Color(0.75f, 0.5f, 0.01f, 1f);
        }   
        public static Color SetVividLightYellow()
        {
            return new Color(0.75f, 0.75f, 0.5f, 1f);
        }
        public static Color SetVividCoral()
        {
            return new Color(1f, 0.5f, 0.31f, 1f);
        }
        public static Color SetVividTealBlue()
        {
            return new Color(0.21f, 0.46f, 0.53f, 1f);
        }
        public static Color SetVividLavender()
        {
            return new Color(0.7f, 0.61f, 0.76f, 1f);
        }
        public static Color SetVividPinkOrange()
        {
            return new Color(1f, 0.7f, 0.68f, 1f);
        }
        public static Color SetVividPinkYellow()
        {
            return new Color(1f, 0.93f, 0.68f, 1f);
        }
        public static Color SetVividPinkPurple()
        {
            return new Color(0.7f, 0.61f, 0.76f, 1f);
        }
        public static Color SetVividPinkGreen()
        {
            return new Color(0.7f, 0.87f, 0.76f, 1f);
        }
        public static Color SetVividPinkBlue()
        {
            return new Color(0.68f, 0.78f, 0.81f, 1f);
        }
        public static Color SetVividPinkBrown()
        {
            return new Color(0.76f, 0.61f, 0.53f, 1f);
        }
        public static Color SetVividPinkGray()
        {
            return new Color(0.81f, 0.81f, 0.77f, 1f);
        }
        public static Color SetVividPinkCyan()
        {
            return new Color(0.68f, 0.87f, 0.81f, 1f);
        }
        public static Color SetVividPinkMagenta()
        {
            return new Color(0.98f, 0.61f, 0.78f, 1f);
        }
        public static Color SetVividPinkTeal()
        {
            return new Color(0.66f, 0.87f, 0.81f, 1f);
        }
        public static Color SetCoralRed()
        {
            return new Color(1f, 0.5f, 0.31f, 1f);
        }
        public static Color SetCoralPink()
        {
            return new Color(1f, 0.5f, 0.64f, 1f);
        }
        public static Color SetCoralOrange()
        {
            return new Color(1f, 0.5f, 0.31f, 1f);
        }
        public static Color SetCoralYellow()
        {
            return new Color(1f, 0.93f, 0.55f, 1f);
        }
        public static Color SetCoralGreen()
        {
            return new Color(0.47f, 0.87f, 0.47f, 1f);
        }
        public static Color SetCoralBlue()
        {
            return new Color(0.68f, 0.78f, 0.81f, 1f);
        }
        public static Color SetCoralPurple()
        {
            return new Color(0.7f, 0.62f, 0.87f, 1f);
        }
        public static Color SetCoralBrown()
        {
            return new Color(0.51f, 0.41f, 0.33f, 1f);
        }
        public static Color SetCoralGray()
        {
            return new Color(0.81f, 0.81f, 0.77f, 1f);
        }
        public static Color SetCoralCyan()
        {
            return new Color(0.54f, 0.87f, 0.81f, 1f);
        }
        public static Color SetCoralMagenta()
        {
            return new Color(0.98f, 0.51f, 0.78f, 1f);
        }
        public static Color SetCoralTeal()
        {
            return new Color(0.36f, 0.7f, 0.66f, 1f);
        }
        public static Color SetCoralViolet()
        {
            return new Color(0.8f, 0.6f, 0.79f, 1f);
        }
    
        public static Color SetPink()
        {
            return new Color(1f, 0.75f, 0.8f, 1f);
        }
        public static Color SetPurple()
        {
            return new Color(0.5f, 0f, 0.5f, 1f);
        }
        public static Color SetSilver()
        {
            return new Color(0.75f, 0.75f, 0.75f, 1f);
        }
        public static Color SetTeal()
        {
            return new Color(0f, 0.5f, 0.5f, 1f);
        }
        public static Color SetTurquoise()
        {
            return new Color(0.25f, 0.88f, 0.82f, 1f);
        }
        public static Color SetViolet()
        {
            return new Color(0.93f, 0.51f, 0.93f, 1f);
        }
        public static Color SetIndigo()
        {
            return new Color(0.29f, 0f, 0.51f, 1f);
        }
        public static Color SetMaroon()
        {
            return new Color(0.5f, 0f, 0f, 1f);
        }
        public static Color SetOlive()
        {
            return new Color(0.5f, 0.5f, 0f, 1f);
        }
        public static Color SetNavy()
        {
            return new Color(0f, 0f, 0.5f, 1f);
        }
        public static Color SetLime()
        {
            return new Color(0f, 1f, 0f, 1f);
        }
        public static Color SetSkyBlue()
        {
            return new Color(0.53f, 0.81f, 0.92f, 1f);
        }
        public static Color SetDarkGreen()
        {
            return new Color(0f, 0.39f, 0f, 1f);
        }
        public static Color SetDarkBlue()
        {
            return new Color(0f, 0f, 0.55f, 1f);
        }
        public static Color SetDarkRed()
        {
            return new Color(0.55f, 0f, 0f, 1f);
        }
        public static Color SetDarkGray()
        {
            return new Color(0.66f, 0.66f, 0.66f, 1f);
        }
        public static Color SetDarkGrey()
        {
            return new Color(0.66f, 0.66f, 0.66f, 1f);
        }
        public static Color SetLightGray()
        {
            return new Color(0.83f, 0.83f, 0.83f, 1f);
        }
        public static Color SetLightGrey()
        {
            return new Color(0.83f, 0.83f, 0.83f, 1f);
        }
        public static Color SetDarkCyan()
        {
            return new Color(0f, 0.55f, 0.55f, 1f);
        }
        public static Color SetDarkMagenta()
        {
            return new Color(0.55f, 0f, 0.55f, 1f);
        }
        public static Color SetDarkOrange()
        {
            return new Color(1f, 0.55f, 0f, 1f);
        }
        public static Color SetDarkYellow()
        {
            return new Color(0.55f, 0.55f, 0f, 1f);
        }
        public static Color SetLightCyan()
        {
            return new Color(0.88f, 1f, 1f, 1f);
        }
        public static Color SetLightMagenta()
        {
            return new Color(1f, 0.88f, 1f, 1f);
        }
        public static Color SetLightOrange()
        {
            return new Color(1f, 0.88f, 0f, 1f);
        }
        public static Color SetLightYellow()
        {
            return new Color(1f, 1f, 0.88f, 1f);
        }
        public static Color SetLightBlack()
        {
            return new Color(0.33f, 0.33f, 0.33f, 1f);
        }
        public static Color SetCyan()
        {
            return Color.cyan;
        }
        public static Color SetMagenta()
        {
            return Color.magenta;
        }
        public static Color SetGray()
        {
            return Color.gray;
        }
        public static Color SetGrey()
        {
            return Color.grey;
        }
        public static Color SetClear()
        {
            return Color.clear;
        }
        #endregion
    }
}
