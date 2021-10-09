using System;

namespace CustomFlags
{
    public class Case
    {
        public Case(Flag flag) 
        {
            this.Flag = flag;
            this.BreakAfter = true;
        }
        //
        internal Flag Flag { get; }
        public Action Action { get; set; }
        public bool BreakAfter { get; set; }
    }
}
