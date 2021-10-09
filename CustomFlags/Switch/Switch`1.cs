namespace CustomFlags
{
    public class Switch<T> : SwitchObjects.Components.Switch<Flag, T>
    {
        public Switch(Flag selected) : base(selected)
        {
        }
    }
}
