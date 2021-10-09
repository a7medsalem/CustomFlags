namespace CustomFlags
{
    internal class ColorFlag : Flag
    {
        public static ColorFlag NON     = new ColorFlag(false);
        public static ColorFlag RED     = new ColorFlag(1);
        public static ColorFlag YELLOW  = new ColorFlag(2);
        public static ColorFlag BLUE    = new ColorFlag(3);
        public static ColorFlag BLACK   = new ColorFlag(4);
        public static ColorFlag WHITE   = new ColorFlag(5);
        public static ColorFlag ALL     = new ColorFlag(true);
        
        private const int LENGTH = 128;
        private ColorFlag(bool allTrue) : base(LENGTH, allTrue) { }
        private ColorFlag(params int[] indices) : base(LENGTH, indices) { }
    }
}
