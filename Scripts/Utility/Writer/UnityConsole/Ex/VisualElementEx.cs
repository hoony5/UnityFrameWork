using UnityEngine.UIElements;

namespace Writer.CommandConsole
{
    public static class VisualElementEx
    {
        public static K GetValue<T, K>(this T element) where T : VisualElement
        {
            return element switch
            {
                Foldout foldout => (K)(object)foldout.value,
                BaseField<K> field => field.value,
                _ => default
            };
        }
    }

}