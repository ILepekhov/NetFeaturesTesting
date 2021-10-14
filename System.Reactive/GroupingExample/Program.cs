using ExtensionsLibrary;
using GroupingExample;
using System.Reactive.Linq;

IObservable<Human> people = new[]
{
    new Human("Ivan", Gender.Male, 30),
    new Human("Maksim", Gender.Male, 35),
    new Human("Liliya", Gender.Female, 31),
    new Human("Vera", Gender.Female, 31)
}.ToObservable();

var genderAge = 
    from gender in people.GroupBy(p => p.Gender)
    from avgAge in gender.Average(p => p.Age)
    select new { Gender = gender.Key, AvgAge = avgAge };

genderAge.SubscribeConsole("Gender Age");

Console.ReadLine();