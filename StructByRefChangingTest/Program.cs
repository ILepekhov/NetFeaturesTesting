using System.Windows;

static void ChangeThickness(ref Thickness thickness, double value)
{
    thickness.Left = thickness.Top = thickness.Right = thickness.Bottom = value;
}

static string PrintThickness(Thickness thickness)
{
    return $"Left {thickness.Left}, Top {thickness.Top}, Right {thickness.Right}, Bottom {thickness.Bottom}";
}

Thickness thickness = new Thickness(2, 3, 4, 5);

Console.WriteLine($"Thickness before changing: {PrintThickness(thickness)}");

ChangeThickness(ref thickness, 10);

Console.WriteLine($"Thickness after changing: {PrintThickness(thickness)}");