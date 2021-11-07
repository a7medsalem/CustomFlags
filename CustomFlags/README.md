# Custom Flags
## custom flags linbrary for C# proejcts.

This nuget provide flag class that can act like enum flags. It supports main functionalty like bitwise operations ( | &amp; ), implicit convertion to bool and comparing with integers. It's independent from machine architect limits so it can provide as many as user needs from flags count.


## Features

- Create unlimited flags with same functionalty as enum flags.
- Switch over flags "it can be used to switch over any object".


## How To Use
### Define your custom flags
```csharp
public class ColorFlag : Flag
{
    public static ColorFlag NON     = new ColorFlag(false);
    public static ColorFlag RED     = new ColorFlag(1);
    public static ColorFlag YELLOW  = new ColorFlag(2);
    public static ColorFlag BLUE    = new ColorFlag(3);
    public static ColorFlag BLACK   = new ColorFlag(4);
    public static ColorFlag WHITE   = new ColorFlag(5);
    public static ColorFlag BnW     = new ColorFlag(4, 5);
    public static ColorFlag ALL     = new ColorFlag(true);
    
    private const int LENGTH = 128;
    private ColorFlag(bool allTrue) : base(LENGTH, allTrue) { }
    private ColorFlag(params int[] indices) : base(LENGTH, indices) { }
}
```

### Use defined objects through the code
```csharp
private static string GetColors(ColorFlag flag)
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
```

### Using of Switch class
```csharp
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
        new Default
        {
            Action = () => Console.WriteLine("Default color is selected.")
        },
    });
```