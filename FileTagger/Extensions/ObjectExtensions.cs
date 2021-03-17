namespace FileTagger.Extensions
{
    public static class ObjectExtensions
    {
        public static void CheckWhetherArgumentIsNull(this object self, string argumentName)
        {
            if (self == null)
            {
                throw new System.ArgumentNullException(argumentName);
            }
        }
    }
}
