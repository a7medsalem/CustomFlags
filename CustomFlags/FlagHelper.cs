using System;

namespace CustomFlags
{
    internal static class FlagHelper
    {
        public static int FlagLengthToArrayLength(int length)
        {
            int l = length / 8;
            return length % 8 == 0 ? l : l + 1;
        }

        public static byte BitIndexToByte(int index)
        {
            return (byte)(1 << (index % 8));
        }

        public static byte BitIndexToByteReverse(int index)
        {
            return (byte)~BitIndexToByte(index);
        }

        public static string ToBitString(byte b)
        {
            return Convert.ToString(b, 2).PadLeft(8, '0');
        }

        public static byte[] ToBitArray(byte b)
        {
            return new []
            {
                (b & 0x01) > 0 ? (byte)1 : (byte)0,
                (b & 0x02) > 0 ? (byte)1 : (byte)0,
                (b & 0x04) > 0 ? (byte)1 : (byte)0,
                (b & 0x08) > 0 ? (byte)1 : (byte)0,
                (b & 0x10) > 0 ? (byte)1 : (byte)0,
                (b & 0x20) > 0 ? (byte)1 : (byte)0,
                (b & 0x40) > 0 ? (byte)1 : (byte)0,
                (b & 0x80) > 0 ? (byte)1 : (byte)0,
            };
        }
    }
}