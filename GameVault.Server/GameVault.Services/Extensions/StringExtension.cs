namespace GameVault.Services.Extensions
{
    internal static class StringExtension
    {
        public static byte[] GetBytesUTF8(this string str) => System.Text.Encoding.UTF8.GetBytes(str);
    }
}
