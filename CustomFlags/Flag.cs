using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomFlags
{
    public class Flag
    {
        private byte[] _flags;
        private Flag(byte[] flags)
        {
            this._flags = flags;
        }
        public Flag(int length, bool allTrue = false)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException("Lenght can't be zero.");

            this._flags = new byte[length];
            for (int i = 0; i < length; i++)
            {
                this._flags[i] = (byte)(allTrue ? 1 : 0);
            }
        }
        public Flag(int length, params int[] indices)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException("Lenght can't be zero.");

            if (indices.Length > 0)
            {
                int minIndex = indices.Min();
                if (minIndex < 0)
                    throw new ArgumentOutOfRangeException("Index can't be negative number.");

                int maxIndex = indices.Max();
                if (maxIndex >= length)
                    throw new ArgumentOutOfRangeException("Max index must be less than given length.");
            }

            this._flags = new byte[length];
            for (int i = 0; i < indices.Length; i++)
            {
                this._flags[indices[i]] = 1;
            }
        }
        protected bool ExceedMaxInt()
        {
            return this._flags.Length > 64 && this._flags.TakeWhile((b, i) => i > 64 && b > 0).Any();
        }
        protected int ToInteger()
        {
            int index = 0;
            return this._flags.Aggregate(0, (sum, b) => (int)(sum + b * Math.Pow(2, index++)));
        }
        public override string ToString()
        {
            return string.Concat(_flags);
        }
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
        
        
        public static void Switch(Switch switchCases)
        {
            SwitchObjects.Switcher.Switch(switchCases);
        }
        public static RT Switch<RT>(Switch<RT> switchCases) where RT : class
        {
            return SwitchObjects.Switcher.Switch(switchCases);
        }


        private static Flag Bitwise(Flag first, Flag other, Func<byte, byte, byte> operation)
        {
            if (first == null || other == null)
                throw new ArgumentNullException("Can't bitwise null objects");

            if (first._flags.Length != other._flags.Length)
                throw new ArgumentException("Both flags must be same length");

            byte[] resultOnes = new byte[first._flags.Length];
            for (int i = 0; i < first._flags.Length; i++)
            {
                resultOnes[i] = operation(first._flags[i], other._flags[i]);
            }

            return new Flag(resultOnes);
        }
        public static Flag operator |(Flag self, Flag flag)
        {
            return Bitwise(self, flag, (f, o) => (byte)(f | o));
        }
        public static Flag operator &(Flag self, Flag flag)
        {
            return Bitwise(self, flag, (f, o) => (byte)(f & o));
        }
        public static bool operator true(Flag flag)
        {
            if (flag == null) return false;
            //
            return flag._flags.Any(f => f > 0);
        }
        public static bool operator false(Flag flag)
        {
            if (flag == null) return false;
            //
            return flag._flags.All(f => f == 0);
        }
        public static bool operator >(Flag flag, int integer)
        {
            if (flag == null)
                throw new ArgumentNullException("Can't compare null flag.");

            if (flag.ExceedMaxInt())
                return true;

            return flag.ToInteger() > integer;
        }
        public static bool operator >=(Flag flag, int integer)
        {
            if (flag == null)
                throw new ArgumentNullException("Can't compare null flag.");

            if (flag.ExceedMaxInt())
                return true;

            return flag.ToInteger() >= integer;
        }
        public static bool operator <(Flag flag, int integer)
        {
            if (flag == null)
                throw new ArgumentNullException("Can't compare null flag.");

            if (flag.ExceedMaxInt())
                return false;

            return flag.ToInteger() < integer;
        }
        public static bool operator <=(Flag flag, int integer)
        {
            if (flag == null)
                throw new ArgumentNullException("Can't compare null flag.");

            if (flag.ExceedMaxInt())
                return false;

            return flag.ToInteger() <= integer;
        }
        
        public static explicit operator int (Flag flag)
        {
            if (flag.ExceedMaxInt())
                return int.MaxValue;
            else
                return flag.ToInteger();
        }
    }
}
