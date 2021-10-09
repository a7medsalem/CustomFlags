namespace SwitchObjects.Components
{
    public class Default<T, R> : Case<T, R> where T : class
    {
        public Default() : base(null) { }
    }
}
