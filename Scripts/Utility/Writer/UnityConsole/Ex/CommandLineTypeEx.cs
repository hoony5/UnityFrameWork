using UnityEngine;

namespace Writer.CommandConsole
{
    public static class CommandLineTypeEx
    {
        public static string GetSuffix(this CommandLineType type)
        {
            switch (type)
            {
                case CommandLineType.User:
                    return "User";
                case CommandLineType.Info:
                    return "Info";
                case CommandLineType.Error:
                    return "Error";
                case CommandLineType.Warning:
                    return "Warning";
                default:
                    return "User";
            }
        }
        public static Color GetColor(this CommandLineType type)
        {
            switch (type)
            {
                case CommandLineType.User:
                    return Color.black;
                case CommandLineType.Info:
                    return Color.white;
                case CommandLineType.Error:
                    return Color.red;
                case CommandLineType.Warning:
                    return Color.yellow;
                default:
                    return Color.black;
            }
        }
    }
}