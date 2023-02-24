using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomFlags.Demo
{
    class RangeFlag : Flag
    {
        public static RangeFlag NON     = new RangeFlag(false);
        public static RangeFlag RED     = new RangeFlag(1);
        public static RangeFlag YELLOW  = new RangeFlag(2);
        public static RangeFlag BLUE    = new RangeFlag(3);
        public static RangeFlag RED_YELLOW_BLUE = new RangeFlag(1,3,true);
        public static RangeFlag WHITE   = new RangeFlag(5);
        public static RangeFlag ALL     = new RangeFlag(true);
        
        private const int LENGTH = 128;
        private RangeFlag(bool allTrue) : base(LENGTH, allTrue) { }
        private RangeFlag(params int[] indices) : base(LENGTH, indices) { }
        private RangeFlag(int startRange, int endRange, bool trueInRange) : base(LENGTH, startRange, endRange, trueInRange) { }
    }

    class Program
    {
        private static string GetColor(Flag flag)
        {
            List<string> colors = new List<string>();

            if (flag & ColorFlag.NON)
                return "";

            if (flag & ColorFlag.RED)
                colors.Add("Red");

            if (flag & ColorFlag.BLUE)
                colors.Add("Blue");

            if (flag & ColorFlag.YELLOW)
                colors.Add("Yellow");

            if (flag & ColorFlag.WHITE)
                colors.Add("White");

            if (flag & ColorFlag.BLACK)
                colors.Add("Black");

            string str = string.Concat(colors.Select(s => s + " and "));
            return str.Substring(0, str.Length - 5);
        }

        static void Main(string[] args)
        {

            Console.WriteLine("============= Test Bitwise, true/false and compare operators =============\n");

            var f_n = new Flag(10, false);
            var f_5 = new Flag(10, 5);
            var f_6 = new Flag(10, 6);
            var f56 = new Flag(10, 5, 6);
            var f_7 = new Flag(10, 7);

            var selected = f_6;
            if (selected & f56)
            {
                Console.WriteLine("[✓] Selected flag is 5 or 6, using true/false operator.");
            }
            if ((selected & f56) > 0)
            {
                Console.WriteLine("[✓] Selected flag is 5 or 6, using > operator.");
            }

            

            Console.WriteLine("\n\n");
            Console.WriteLine("=================== Test ToString function  ===================\n");
            Console.WriteLine("[✓] Index number 5 is selected, result is " + f_5.ToString());
            Console.WriteLine("[✓] Index number 5 and 6 are selected, result is " + f56.ToString());




            Console.WriteLine("\n\n");
            Console.WriteLine("=================== Test if/else operators  ===================\n");
            Console.WriteLine("[✓] Excepted are Blue and Black, result is " + GetColor(ColorFlag.BLUE | ColorFlag.BLACK));
            Console.WriteLine("[✓] Excepted are all colors, result is " + GetColor(ColorFlag.ALL));

            
            
            Console.WriteLine("\n\n");
            Console.WriteLine("============== Test switch without return  ===============\n");
            ColorFlag color = ColorFlag.RED;
            Console.WriteLine("[✓] Excepted is Red");
            //
            Flag.Switch(
                new Switch(color)
                {
                    new Case(ColorFlag.BLUE)
                    {
                        Action = () => Console.WriteLine("Result is blue.")
                    },
                    new Case(ColorFlag.RED)
                    {
                        Action = () => Console.WriteLine("Result is red.")
                    },
                    //
                    new Default
                    {
                        Action = () => Console.WriteLine("Default color is selected.")
                    },
                });


            
            
            Console.WriteLine("\n\n");
            Console.WriteLine("================ Test switch with return =================");
            Console.WriteLine("[✓] Excepted is Red");
            string result = Flag.Switch(
                new Switch<string>(color)
                {
                    new Case<string>(ColorFlag.BLACK)
                    {
                        Func = () => "Black"
                    },
                    new Case<string>(ColorFlag.WHITE)
                    {
                        Func = () => "White"
                    },
                    new Case<string>(ColorFlag.YELLOW)
                    {
                        Func = () => "Yellow"
                    },
                    new Case<string>(ColorFlag.RED)
                    {
                        Func = () => "Red"
                    },
                });
            Console.WriteLine("Result is " + result);


            if((RangeFlag.RED & RangeFlag.RED_YELLOW_BLUE) > 0)
            {
                int x = 0;
            }


            Console.WriteLine("End of main");
            Console.ReadKey();
        }
    }
}
