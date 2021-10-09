using System;

namespace SwitchObjects.Components
{
    public class Case<T> : IEquatable<Case<T>>
    {
        public Case(T option) 
        {
            this.Option = option;
            this.BreakAfter = true;
        }
        //
        public T Option { get; }
        public Action Action { get; set; }
        public bool BreakAfter { get; set; }


        public override bool Equals(object obj)
        {
            if(obj is Case<T> _case)
            {
                return this.Equals(_case);
            }
            else
            {
                return false;
            }
        }
        public bool Equals(Case<T> other)
        {
            return this.Option.Equals(other.Option);
        }
    }
}
