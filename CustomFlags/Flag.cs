using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomFlags
{
    public class Flag
    {
        /// <summary>
        ///     Switch over custom flags using Switch object.
        /// </summary>
        public static void Switch(Switch switchCases)
        {
            SwitchObjects.Switcher.Switch(switchCases);
        }
        /// <summary>
        ///     Switch over custom flags using Switch object that have return type.
        /// </summary>
        public static RT Switch<RT>(Switch<RT> switchCases) where RT : class
        {
            return SwitchObjects.Switcher.Switch(switchCases);
        }

        private byte[] _flags;
        protected Flag(byte[] flags)
        {
            this._flags = flags;
        }
        /// <summary>
        ///     Create a flag with all its bit set to 0 or 1;
        /// </summary>
        /// <param name="length">Flag length</param>
        /// <param name="allTrue">Set flag bit to 1 if true</param>
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
        /// <summary>
        ///     Create a flag with given bit indices setted to 1;
        /// </summary>
        /// <param name="length">Flag length.</param>
        /// <param name="indices">True bit indices.</param>
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
        /// <summary>
        ///     Create a flag with given bit withen range setted to 0 or 1;
        /// </summary>
        /// <param name="length">Flag length.</param>
        /// <param name="startIndex">Range start index.</param>
        /// <param name="endIndex">Range end index.</param>
        /// <param name="trueInRange">Set indices inside the range to 1 or 0, out of the range will be inversed.</param>
        public Flag(int length, int startIndex, int endIndex, bool trueInRange)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException("Lenght can't be zero.");

            if(startIndex < 0 && endIndex < 0)
                throw new ArgumentException("Start\\End index can't be negative.");

            if(startIndex > endIndex)
                throw new ArgumentException("End index can't be less than start index.");

            if(endIndex >= length)
                throw new ArgumentOutOfRangeException("Max index must be less than given length.");

            this._flags = new byte[length];
            for (int i = 0; i < length; i++)
            {
                if(i >= startIndex && i <= endIndex)
                {
                    this._flags[i] = (byte)(trueInRange ? 1 : 0);
                }
                else
                {
                    this._flags[i] = (byte)(trueInRange ? 0 : 1);
                }
            }
        }
        /// <summary>
        ///     Check if flag bytes exceed max integer value.
        /// </summary>
        /// <returns></returns>
        protected bool ExceedMaxInt()
        {
            return this._flags.Length > 64 && this._flags.TakeWhile((b, i) => i > 64 && b > 0).Any();
        }
        /// <summary>
        ///     Convert flag to integer value.
        /// </summary>
        /// <returns>Integer value of the flag, int.MaxInt if it exceed the maximum integer value.</returns>
        protected int ToInteger()
        {
            int index = 0;
            return this._flags.Aggregate(0, (sum, b) => (int)(sum + b * Math.Pow(2, index++)));
        }
        /// <summary>
        ///     Get string representation of the flag.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Concat(_flags);
        }
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
        /// <summary>
        ///    Get a copy of internal array.
        /// </summary>
        /// <returns></returns>
        protected byte[] GetInternalArray()
        {
            return this._flags.ToArray();
        }

        #region Overriden operators
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

        #endregion
    }
}
