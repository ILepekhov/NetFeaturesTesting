using ExtensionsLibrary;
using JoinsExample;
using System.Reactive.Linq;
using System.Reactive.Subjects;

Subject<DoorOpened> doorOpenedSubject = new();
IObservable<DoorOpened> doorOpened = doorOpenedSubject.AsObservable();

var entrances = doorOpened.Where(x => x.Direction == OpenDirection.Entering);
var maleEntering = entrances.Where(x => x.Gender == Gender.Male);
var femaleEntrering = entrances.Where(x => x.Gender == Gender.Female);

var exits = doorOpened.Where(x => x.Direction == OpenDirection.Leaving);
var maleExiting = exits.Where(x => x.Gender == Gender.Male);
var femaleExiting = exits.Where(x => x.Gender == Gender.Female);

maleEntering
    .Join(femaleEntrering,
        male => maleExiting.Where(exit => exit.Name == male.Name),
        female => femaleExiting.Where(exit => exit.Name == female.Name),
        (m, f) => new { Male = m.Name, Female = f.Name })
    .SubscribeConsole("Together At Room");

/* analog with a LINQ query
 * 
 * from male in maleEntering
 * join female in femaleEntering on maleEntering.Where(exit => exit.Name == male.Name)
 * equals femaleExiting.Where(exit => female.Name == exit.Name)
 * select new {Male = male.Name, Female = female.Name};
 * 
 */

doorOpenedSubject.OnNext(new DoorOpened { Name = "Bob", Gender = Gender.Male, Direction = OpenDirection.Entering });
doorOpenedSubject.OnNext(new DoorOpened { Name = "Sara", Gender = Gender.Female, Direction = OpenDirection.Entering });
doorOpenedSubject.OnNext(new DoorOpened { Name = "John", Gender = Gender.Male, Direction = OpenDirection.Entering });
doorOpenedSubject.OnNext(new DoorOpened { Name = "Sara", Gender = Gender.Female, Direction = OpenDirection.Leaving });
doorOpenedSubject.OnNext(new DoorOpened { Name = "Fibi", Gender = Gender.Female, Direction = OpenDirection.Entering });
doorOpenedSubject.OnNext(new DoorOpened { Name = "Bob", Gender = Gender.Male, Direction = OpenDirection.Leaving });
doorOpenedSubject.OnNext(new DoorOpened { Name = "Dan", Gender = Gender.Male, Direction = OpenDirection.Entering });
doorOpenedSubject.OnNext(new DoorOpened { Name = "Fibi", Gender = Gender.Female, Direction = OpenDirection.Leaving });
doorOpenedSubject.OnNext(new DoorOpened { Name = "John", Gender = Gender.Male, Direction = OpenDirection.Leaving });
doorOpenedSubject.OnNext(new DoorOpened { Name = "Dan", Gender = Gender.Male, Direction = OpenDirection.Leaving });
doorOpenedSubject.OnCompleted();

Console.ReadLine();