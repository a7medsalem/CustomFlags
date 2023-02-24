using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CustomFlags.Tests
{
    public class FlagHelperTests
    {
        [Test]
        public void FlagLengthToArrayLengthTest()
        {
            Assert.AreEqual(1, FlagHelper.FlagLengthToArrayLength(1));
            Assert.AreEqual(1, FlagHelper.FlagLengthToArrayLength(8));
            Assert.AreEqual(2, FlagHelper.FlagLengthToArrayLength(9));
            Assert.AreEqual(2, FlagHelper.FlagLengthToArrayLength(16));
            Assert.AreEqual(3, FlagHelper.FlagLengthToArrayLength(20));
        }
        
        [Test]
        public void BitIndexToByteTest()
        {
            Assert.AreEqual(1, FlagHelper.BitIndexToByte(0));
            Assert.AreEqual(2, FlagHelper.BitIndexToByte(1));
            Assert.AreEqual(4, FlagHelper.BitIndexToByte(2));
            Assert.AreEqual(8, FlagHelper.BitIndexToByte(3));
            Assert.AreEqual(16, FlagHelper.BitIndexToByte(4));
            Assert.AreEqual(32, FlagHelper.BitIndexToByte(5));
            Assert.AreEqual(64, FlagHelper.BitIndexToByte(6));
            Assert.AreEqual(128, FlagHelper.BitIndexToByte(7));
        }

        [Test]
        public void BitIndexToByteReverseTest()
        {
            Assert.AreEqual(254, FlagHelper.BitIndexToByteReverse(0));
            Assert.AreEqual(253, FlagHelper.BitIndexToByteReverse(1));
            Assert.AreEqual(251, FlagHelper.BitIndexToByteReverse(2));
            Assert.AreEqual(247, FlagHelper.BitIndexToByteReverse(3));
            Assert.AreEqual(239, FlagHelper.BitIndexToByteReverse(4));
            Assert.AreEqual(223, FlagHelper.BitIndexToByteReverse(5));
            Assert.AreEqual(191, FlagHelper.BitIndexToByteReverse(6));
            Assert.AreEqual(127, FlagHelper.BitIndexToByteReverse(7));
        }

        [Test]
        public void ToBitStringTest()
        {
            Assert.AreEqual("00000000", FlagHelper.ToBitString(0));
            Assert.AreEqual("00000001", FlagHelper.ToBitString(1));
            Assert.AreEqual("00000010", FlagHelper.ToBitString(2));
            Assert.AreEqual("00000011", FlagHelper.ToBitString(3));
            Assert.AreEqual("00000100", FlagHelper.ToBitString(4));
            Assert.AreEqual("00000101", FlagHelper.ToBitString(5));
            Assert.AreEqual("00000110", FlagHelper.ToBitString(6));
            Assert.AreEqual("00000111", FlagHelper.ToBitString(7));
            Assert.AreEqual("00001000", FlagHelper.ToBitString(8));
            Assert.AreEqual("00001001", FlagHelper.ToBitString(9));
            Assert.AreEqual("00001010", FlagHelper.ToBitString(10));
            Assert.AreEqual("00001011", FlagHelper.ToBitString(11));
            Assert.AreEqual("00001100", FlagHelper.ToBitString(12));
            Assert.AreEqual("00001101", FlagHelper.ToBitString(13));
            Assert.AreEqual("00001110", FlagHelper.ToBitString(14));
            Assert.AreEqual("00001111", FlagHelper.ToBitString(15));
            Assert.AreEqual("00010000", FlagHelper.ToBitString(16));
            Assert.AreEqual("00010001", FlagHelper.ToBitString(17));
            Assert.AreEqual("00010010", FlagHelper.ToBitString(18));
            Assert.AreEqual("00010011", FlagHelper.ToBitString(19));
            Assert.AreEqual("00010100", FlagHelper.ToBitString(20));
            Assert.AreEqual("00010101", FlagHelper.ToBitString(21));
            Assert.AreEqual("00010110", FlagHelper.ToBitString(22));
            Assert.AreEqual("00010111", FlagHelper.ToBitString(23));
            Assert.AreEqual("00011000", FlagHelper.ToBitString(24));
            Assert.AreEqual("00011001", FlagHelper.ToBitString(25));
            Assert.AreEqual("00011010", FlagHelper.ToBitString(26));
            Assert.AreEqual("00011011", FlagHelper.ToBitString(27));
            Assert.AreEqual("00011100", FlagHelper.ToBitString(28));
            Assert.AreEqual("00011101", FlagHelper.ToBitString(29));
            Assert.AreEqual("00011110", FlagHelper.ToBitString(30));
            Assert.AreEqual("00011111", FlagHelper.ToBitString(31));
            Assert.AreEqual("00100000", FlagHelper.ToBitString(32));
            Assert.AreEqual("00100001", FlagHelper.ToBitString(33));
            Assert.AreEqual("00100010", FlagHelper.ToBitString(34));
            Assert.AreEqual("00100011", FlagHelper.ToBitString(35));
            Assert.AreEqual("00100100", FlagHelper.ToBitString(36));
            Assert.AreEqual("00100101", FlagHelper.ToBitString(37));
            Assert.AreEqual("00100110", FlagHelper.ToBitString(38));
            Assert.AreEqual("00100111", FlagHelper.ToBitString(39));
            Assert.AreEqual("00101000", FlagHelper.ToBitString(40));
            Assert.AreEqual("00101001", FlagHelper.ToBitString(41));
            Assert.AreEqual("00101010", FlagHelper.ToBitString(42));
            Assert.AreEqual("00101011", FlagHelper.ToBitString(43));
            Assert.AreEqual("00101100", FlagHelper.ToBitString(44));
            Assert.AreEqual("00101101", FlagHelper.ToBitString(45));
            Assert.AreEqual("00101110", FlagHelper.ToBitString(46));
            Assert.AreEqual("00101111", FlagHelper.ToBitString(47));
            Assert.AreEqual("00110000", FlagHelper.ToBitString(48));
            Assert.AreEqual("00110001", FlagHelper.ToBitString(49));
            Assert.AreEqual("00110010", FlagHelper.ToBitString(50));
            Assert.AreEqual("00110011", FlagHelper.ToBitString(51));
            Assert.AreEqual("00110100", FlagHelper.ToBitString(52));
            Assert.AreEqual("00110101", FlagHelper.ToBitString(53));
            Assert.AreEqual("00110110", FlagHelper.ToBitString(54));
            Assert.AreEqual("00110111", FlagHelper.ToBitString(55));
            Assert.AreEqual("00111000", FlagHelper.ToBitString(56));
            Assert.AreEqual("00111001", FlagHelper.ToBitString(57));
            Assert.AreEqual("00111010", FlagHelper.ToBitString(58));
            Assert.AreEqual("00111011", FlagHelper.ToBitString(59));
            Assert.AreEqual("00111100", FlagHelper.ToBitString(60));
            Assert.AreEqual("00111101", FlagHelper.ToBitString(61));
            Assert.AreEqual("00111110", FlagHelper.ToBitString(62));
            Assert.AreEqual("00111111", FlagHelper.ToBitString(63));

            Assert.AreEqual("11111111", FlagHelper.ToBitString(255));
            Assert.AreEqual("11111110", FlagHelper.ToBitString(254));
            Assert.AreEqual("11111101", FlagHelper.ToBitString(253));
            Assert.AreEqual("11111100", FlagHelper.ToBitString(252));
            Assert.AreEqual("11111011", FlagHelper.ToBitString(251));
            Assert.AreEqual("11111010", FlagHelper.ToBitString(250));
            Assert.AreEqual("11111001", FlagHelper.ToBitString(249));
            Assert.AreEqual("11111000", FlagHelper.ToBitString(248));
            Assert.AreEqual("11110111", FlagHelper.ToBitString(247));
            Assert.AreEqual("11110110", FlagHelper.ToBitString(246));
            Assert.AreEqual("11110101", FlagHelper.ToBitString(245));
            Assert.AreEqual("11110100", FlagHelper.ToBitString(244));
            Assert.AreEqual("11110011", FlagHelper.ToBitString(243));
            Assert.AreEqual("11110010", FlagHelper.ToBitString(242));
            Assert.AreEqual("11110001", FlagHelper.ToBitString(241));
            Assert.AreEqual("11110000", FlagHelper.ToBitString(240));
        }
    }
}
