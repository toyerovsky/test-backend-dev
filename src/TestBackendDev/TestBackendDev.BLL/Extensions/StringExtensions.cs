namespace TestBackendDev.BLL.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string s)
        {
            return s == null || s.Trim().Equals("");
        }
    }
}