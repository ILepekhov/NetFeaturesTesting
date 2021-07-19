using System;

namespace RecordsTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            TryModifyRecord();
        }

        static void TryModifyRecord()
        {
            Console.WriteLine("====TryModifyRecord====");

            Point point = new(1, 2);

            Console.WriteLine("Initial 'point' state: {0}", point);

            var pointType = typeof(Point);
            var xProp = pointType.GetProperty(nameof(Point.X));
            if (xProp is not null)
            {
                xProp.SetValue(point, 3);
            }
            else
            {
                Console.WriteLine("Can't get access to the X property");
            }

            Console.WriteLine("Final 'point' state: {0}", point);

            Console.WriteLine();
        }
    }
}
