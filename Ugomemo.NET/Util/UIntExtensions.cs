namespace Ugomemo.NET.Util
{
    internal static class UIntExtensions
    {
        public static uint SwapEndianness(this uint value)
        {
            value = (value >> 16) | (value << 16);
            return ((value & 0xFF00FF00) >> 8) | ((value & 0x00FF00FF) << 8);
        }
    }
}
