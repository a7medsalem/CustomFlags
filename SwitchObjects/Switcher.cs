namespace SwitchObjects
{
    using System.Linq;
    using SwitchObjects.Components;

    public static class Switcher
    {
        public static void Switch<T>(Switch<T> switchCases) where T : class
        {
            var selectedCase = switchCases.Distinct().FirstOrDefault(c => c.Option == switchCases.Selected);
            if (selectedCase != null)
            {
                selectedCase.Action?.Invoke();
                if (selectedCase.BreakAfter)
                {
                    return;
                }
            }

            var defaultCase = switchCases.FirstOrDefault(c => c is Default<T>);
            if (defaultCase != null)
            {
                defaultCase.Action?.Invoke();
            }
        }

        public static RT Switch<T, RT>(Switch<T, RT> switchCases) where T : class where RT : class
        {
            var selectedCase = switchCases.FirstOrDefault(c => c.Option == switchCases.Selected);
            if (selectedCase != null)
            {
                return selectedCase.Func?.Invoke();
            }

            var defaultCase = switchCases.FirstOrDefault(c => c is Default<RT>);
            if (defaultCase != null)
            {
                return defaultCase.Func?.Invoke();
            }

            return default(RT);
        }
    }
}
