namespace SwitchObjects.Components
{
    public class Default<T> : Case<T> where T : class
    {
        public Default() : base(null) { }
    }
}
