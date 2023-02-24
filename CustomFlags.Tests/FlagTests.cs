using NUnit.Framework;

namespace CustomFlags.Tests
{
    public class FlagTests
    {
        class TestFlag : Flag
        {
            private const int LENGTH = 16;
            public static TestFlag ALL = new TestFlag(true);
            public static TestFlag NON = new TestFlag();
            public static TestFlag _00 = new TestFlag(0);
            public static TestFlag _01 = new TestFlag(1);
            public static TestFlag _02 = new TestFlag(2);
            public static TestFlag _03 = new TestFlag(3);
            public static TestFlag _04 = new TestFlag(4);
            public static TestFlag _05 = new TestFlag(5);
            public static TestFlag _06 = new TestFlag(6);
            public static TestFlag _07 = new TestFlag(7);
            public static TestFlag _08 = new TestFlag(8);
            public static TestFlag _09 = new TestFlag(9);
            public static TestFlag _10 = new TestFlag(10);
            public static TestFlag _11 = new TestFlag(11);
            public static TestFlag _12 = new TestFlag(12);
            public static TestFlag _13 = new TestFlag(13);
            public static TestFlag _14 = new TestFlag(14);
            public static TestFlag _15 = new TestFlag(15);
            
            private TestFlag(bool allTrue = false) 
                : base(LENGTH, allTrue)
            {
            }
            public TestFlag(params int[] indices) 
                : base(LENGTH, indices)
            {
            }
            private TestFlag(int startIndex, int endIndex, bool trueInRange) 
                : base(LENGTH, startIndex, endIndex, trueInRange)
            {
            }
        }
        
        [Test]
        public void BooleanConverter()
        {
            Assert.True((bool)TestFlag.ALL);
            Assert.True((bool)TestFlag._00);
            Assert.True((bool)TestFlag._08);
            Assert.True((bool)TestFlag._15);
            Assert.False((bool)TestFlag.NON);
        }

        [Test]
        public void ByteConverter()
        {
            Assert.AreEqual(0x00, (byte)TestFlag.NON);
            Assert.AreEqual(0x01, (byte)TestFlag._00);
            Assert.AreEqual(0x80, (byte)TestFlag._07);
            Assert.AreEqual(0x00, (byte)TestFlag._15);
            Assert.AreEqual(0xFF, (byte)TestFlag.ALL);
        }

        [Test]
        public void UShortConverter()
        {
            Assert.AreEqual(0x0000, (ushort)TestFlag.NON);
            Assert.AreEqual(0x0001, (ushort)TestFlag._00);
            Assert.AreEqual(0x0080, (ushort)TestFlag._07);
            Assert.AreEqual(0x8000, (ushort)TestFlag._15);
            Assert.AreEqual(0xFFFF, (ushort)TestFlag.ALL);
        }

        [Test]
        public void IntConverter()
        {
            Assert.AreEqual(0x00000000, (int)TestFlag.NON);
            Assert.AreEqual(0x00000001, (int)TestFlag._00);
            Assert.AreEqual(0x00000080, (int)TestFlag._07);
            Assert.AreEqual(0x00008000, (int)TestFlag._15);
            Assert.AreEqual(0x0000FFFF, (int)TestFlag.ALL);
        }

        [Test]
        public void IfCondition()
        {
            if (TestFlag._00)
            {
            }
            else
            {
                Assert.Fail();
            }

            if (TestFlag._15)
            {
            }
            else
            {
                Assert.Fail();
            }

            if (TestFlag.NON)
            {
                Assert.Fail();
            }

            if (TestFlag.ALL)
            {
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void BitwiseAND()
        {
            Assert.AreEqual(TestFlag._00, TestFlag._00 & TestFlag._00);
            Assert.AreEqual(TestFlag.NON, TestFlag._00 & TestFlag._01);
            Assert.AreEqual(TestFlag.NON, TestFlag._00 & TestFlag._15);
            Assert.AreEqual(TestFlag.NON, TestFlag._15 & TestFlag._00);
            Assert.AreEqual(TestFlag._15, TestFlag._15 & TestFlag._15);
            Assert.AreEqual(TestFlag.NON, TestFlag._15 & TestFlag._00);
            Assert.AreEqual(TestFlag.NON, TestFlag._15 & TestFlag._01);
            Assert.AreEqual(TestFlag._15, TestFlag._15 & TestFlag._15);
        }

        [Test]
        public void BitwiseOR()
        {
            Assert.AreEqual(new TestFlag(0), TestFlag._00 | TestFlag._00);
            Assert.AreEqual(new TestFlag(0, 01), TestFlag._00 | TestFlag._01);
            Assert.AreEqual(new TestFlag(0, 15), TestFlag._00 | TestFlag._15);
            Assert.AreEqual(new TestFlag(0, 15), TestFlag._15 | TestFlag._00);
            Assert.AreEqual(new TestFlag(15), TestFlag._15 | TestFlag._15);
            Assert.AreEqual(new TestFlag(1, 07), TestFlag._01 | TestFlag._07);
        }

        [Test]
        public void ToString()
        {
            Assert.AreEqual("0000000000000000", TestFlag.NON.ToString());
            Assert.AreEqual("1000000000000000", TestFlag._00.ToString());
            Assert.AreEqual("0100000000000000", TestFlag._01.ToString());
            Assert.AreEqual("0010000000000000", TestFlag._02.ToString());
            Assert.AreEqual("0001000000000000", TestFlag._03.ToString());
            Assert.AreEqual("0000100000000000", TestFlag._04.ToString());
            Assert.AreEqual("0000010000000000", TestFlag._05.ToString());
            Assert.AreEqual("0000001000000000", TestFlag._06.ToString());
            Assert.AreEqual("0000000100000000", TestFlag._07.ToString());
            Assert.AreEqual("0000000010000000", TestFlag._08.ToString());
            Assert.AreEqual("0000000001000000", TestFlag._09.ToString());
            Assert.AreEqual("0000000000100000", TestFlag._10.ToString());
            Assert.AreEqual("0000000000010000", TestFlag._11.ToString());
            Assert.AreEqual("0000000000001000", TestFlag._12.ToString());
            Assert.AreEqual("0000000000000100", TestFlag._13.ToString());
            Assert.AreEqual("0000000000000010", TestFlag._14.ToString());
            Assert.AreEqual("0000000000000001", TestFlag._15.ToString());
            Assert.AreEqual("1111111111111111", TestFlag.ALL.ToString());
            Assert.AreEqual("1000000000000001", (TestFlag._00 | TestFlag._15).ToString());
        }
    }
}