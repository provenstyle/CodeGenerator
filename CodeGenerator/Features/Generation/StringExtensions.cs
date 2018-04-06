namespace CodeGenerator.Features
{
    public static class StringExtensions
    {
        public static string LowerFirstLetter(this string target)
        {
            return string.IsNullOrEmpty(target)
                       ? string.Empty
                       : char.ToLower(target[0]) + target.Substring(1);
        }

        public static string UpperFirstLetter(this string target)
        {
            return string.IsNullOrEmpty(target)
                       ? string.Empty
                       : char.ToUpper(target[0]) + target.Substring(1);
        }
    }
}