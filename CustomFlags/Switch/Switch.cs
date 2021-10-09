using System.Collections.Generic;

namespace CustomFlags
{
    public class Switch : List<Case>
    {
        public Switch(Flag flag)
        {
            this.Flag = flag;
        }
        public Flag Flag { get; }
    }
}
