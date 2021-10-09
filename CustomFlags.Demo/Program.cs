using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomFlags.Demo
{
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

            Console.WriteLine("============= Test flags with bitwise/compare operators =============");

            var f_n = new Flag(10, false);
            var f_5 = new Flag(10, 5);
            var f_6 = new Flag(10, 6);
            var f56 = new Flag(10, 5, 6);
            var f_7 = new Flag(10, 7);

            var selected = f_6;
            if (selected & f56)
            {
                Console.WriteLine("Selected flag 5 or 6 using boolean overload");
            }
            if ((selected & f56) > 0)
            {
                Console.WriteLine("Selected flag 5 or 6 using > overload");
            }


            Console.WriteLine("=====================================================================\n");
            Console.WriteLine("=================== Test flags if/else operators  ===================");

            Console.WriteLine(GetColor(ColorFlag.BLUE | ColorFlag.BLACK));
            Console.WriteLine(GetColor(ColorFlag.ALL));

            Console.WriteLine("=====================================================================\n");
            Console.WriteLine("============== Test flags with switch without return  ===============");
            
            int value = (int)selected;
            double dValue = (int)selected;

            ColorFlag color = ColorFlag.RED;
            //
            Flag.Switch(
                new Switch(color)
                {
                    new Case(ColorFlag.BLUE)
                    {
                        Action = () => Console.WriteLine("Color is blue.")
                    },
                    new Case(ColorFlag.RED)
                    {
                        Action = () => Console.WriteLine("Color is red.")
                    },
                    //
                    new Default
                    {
                        Action = () => Console.WriteLine("Default color is selected.")
                    },
                });

            Console.WriteLine("=====================================================================\n");
            Console.WriteLine("================ Test flags with switch with return =================");

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
            Console.WriteLine(result);


            Console.WriteLine("End of main");
            Console.ReadKey();
        }
    }
}
