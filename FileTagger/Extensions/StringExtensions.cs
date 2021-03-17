namespace FileTagger.Extensions
{
    public static class StringExtensions
    {
        public static void CheckWhetherArgumentIsNull(this string self, string argumentName)
        {
            if (self == null || string.IsNullOrWhiteSpace(self))
            {
                throw new System.ArgumentNullException(argumentName);
            }
        }
    }
}
