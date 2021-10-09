using System.Collections.Generic;

namespace SwitchObjects.Components
{
    public class Switch<T, R> : List<Case<T, R>> where T : class
    {
        public Switch(T selected)
        {
            this.Selected = selected;
        }
        public T Selected { get; }
    }
}
