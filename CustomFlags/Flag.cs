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
                throw new ArgumentOutOfRangeException(nameof(length), "Length can't be zero.");

            int l = FlagHelper.FlagLengthToArrayLength(length);
            this._flags = new byte[l];
            for (int i = 0; i < l; i++)
            {
                this._flags[i] = allTrue ? (byte)0xFF : (byte)0x00;
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
                throw new ArgumentOutOfRangeException(nameof(length), "Length can't be zero.");

            if (indices.Length > 0)
            {
                int minIndex = indices.Min();
                if (minIndex < 0)
                    throw new ArgumentOutOfRangeException(nameof(indices), "Index can't be negative number.");

                int maxIndex = indices.Max();
                if (maxIndex >= length)
                    throw new ArgumentOutOfRangeException(nameof(indices), "Max index must be less than given length.");
            }

            int l = FlagHelper.FlagLengthToArrayLength(length);
            this._flags = new byte[l];
            for (int i = 0; i < indices.Length; i++)
            {
                var index = indices[i];
                var flagIndex = index / 8;
                this._flags[flagIndex] |= FlagHelper.BitIndexToByte(index);
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
                throw new ArgumentOutOfRangeException(nameof(length), "Length can't be zero.");

            if (startIndex < 0 && endIndex < 0)
                throw new ArgumentException("Start\\End index can't be negative.");

            if(startIndex > endIndex)
                throw new ArgumentException("End index can't be less than start index.");

            if(endIndex >= length)
                throw new ArgumentOutOfRangeException(nameof(endIndex), "Max index must be less than given length.");

            int l = FlagHelper.FlagLengthToArrayLength(length);
            this._flags = new byte[l];
            for (int i = 0; i < length; i++)
            {
                var flagIndex = i / 8;
                if (i >= startIndex && i <= endIndex)
                {
                    if (trueInRange)
                    {
                        this._flags[flagIndex] |= FlagHelper.BitIndexToByte(i);
                    }
                    else
                    {
                        this._flags[flagIndex] &= FlagHelper.BitIndexToByteReverse(i);
                    }
                }
                else
                {
                    if (trueInRange)
                    {
                        this._flags[flagIndex] &= FlagHelper.BitIndexToByteReverse(i);
                    }
                    else
                    {
                        this._flags[flagIndex] |= FlagHelper.BitIndexToByte(i);
                    }
                }
            }
        }
        /// <summary>
        ///     Check if flag bytes exceed max integer value.
        /// </summary>
        /// <returns></returns>
        protected bool ExceedMaxInt()
        {
            return this._flags.Length > 4 && this._flags.Skip(4).Any(b => b > 0);
        }
        /// <summary>
        ///     Convert flag to integer value.
        /// </summary>
        /// <returns>Integer value of the flag, int.MaxInt if it exceed the maximum integer value.</returns>
        protected int ToInteger()
        {
            int index = 0;
            return this._flags.SelectMany(FlagHelper.ToBitArray).Aggregate(0, (sum, b) => (int)(sum + b * Math.Pow(2, index++)));
        }
        /// <summary>
        ///     Get string representation of the flag.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var bits = _flags.SelectMany(b => FlagHelper.ToBitString(b).ToCharArray().Reverse());
            return string.Concat(bits);
        }
        /// <summary>
        ///     Get hash code of the flag.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
        /// <summary>
        ///     Check if two flags are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case null:
                    return false;
                case Flag other:
                    return this._flags.SequenceEqual(other._flags);
                default:
                    return false;
            }
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
                throw new ArgumentNullException("Operand", "Can't bitwise null objects");

            if (first._flags.Length != other._flags.Length)
                throw new ArgumentException("Both flags must be same length", "Operand");

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
                throw new ArgumentNullException(nameof(flag), "Can't compare null flag.");

            if (flag.ExceedMaxInt())
                return true;

            return flag.ToInteger() > integer;
        }
        public static bool operator >=(Flag flag, int integer)
        {
            if (flag == null)
                throw new ArgumentNullException(nameof(flag), "Can't compare null flag.");

            if (flag.ExceedMaxInt())
                return true;

            return flag.ToInteger() >= integer;
        }
        public static bool operator <(Flag flag, int integer)
        {
            if (flag == null)
                throw new ArgumentNullException(nameof(flag), "Can't compare null flag.");

            if (flag.ExceedMaxInt())
                return false;

            return flag.ToInteger() < integer;
        }
        public static bool operator <=(Flag flag, int integer)
        {
            if (flag == null)
                throw new ArgumentNullException(nameof(flag), "Can't compare null flag.");

            if (flag.ExceedMaxInt())
                return false;

            return flag.ToInteger() <= integer;
        }

        public static explicit operator bool(Flag flag)
        {
            return flag ? true : false;
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
