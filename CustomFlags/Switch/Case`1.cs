using System;

namespace CustomFlags
{
    public class Case<T>
    {
        public Case(Flag flag)
        {
            this.Flag = flag;
        }
        //
        internal Flag Flag { get; }
        public Func<T> Func { set; get; }
    }
}
