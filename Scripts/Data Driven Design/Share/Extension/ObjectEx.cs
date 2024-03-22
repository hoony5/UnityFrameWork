using UnityEngine;

namespace Share
{
    public static class ObjectEx
    {
        public static bool IsNull(this Object obj)
        {
            return (object)obj == null;
        }
    }
}
