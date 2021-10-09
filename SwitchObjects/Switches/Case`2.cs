using System;

namespace SwitchObjects.Components
{
    public class Case<T, R> : IEquatable<Case<T, R>> where T : class
    {
        public Case(T option)
        {
            this.Option = option;
        }
        //
        public T Option { get; }
        public Func<R> Func { set; get; }



        public override bool Equals(object obj)
        {
            if (obj is Case<T, R> _case)
            {
                return this.Equals(_case);
            }
            else
            {
                return false;
            }
        }
        public bool Equals(Case<T, R> other)
        {
            return this.Option.Equals(other.Option);
        }
    }
}
