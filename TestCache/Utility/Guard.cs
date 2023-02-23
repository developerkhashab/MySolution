using System.Text;

namespace TestCache.Utility
{
    public static class Guard
    {
        public static bool IsNotWhiteSpaceOrEmpty(this string value) => !string.IsNullOrWhiteSpace(value);
        public static bool IsNotNullOrEmptyList<T>(this List<T> value) => value != null && value.Count > 0;
        public static bool IsNotNull<T>(this T value) => value != null;
        public static bool KeyValueValid<T>(string Key, T model, out string errors)
        {
            var errorBuilder = new StringBuilder();
            bool valid = true;

            if (!Key.IsNotWhiteSpaceOrEmpty())
            {
                errorBuilder.Append("Key cannot be empty.");
                valid = false;
            }
            if (!model.IsNotNull())
            {
                if (!Key.IsNotWhiteSpaceOrEmpty())
                {
                    errorBuilder.Append(' ');
                }
                errorBuilder.Append("Value cannot be null.");
                valid = false;
            }

            errors = errorBuilder.ToString();
            return valid;
        }
    }
}
