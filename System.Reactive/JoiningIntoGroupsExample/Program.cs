using ExtensionsLibrary;
using JoiningIntoGroupsExample;
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

var malesAcquaintances = maleEntering
    .GroupJoin(femaleEntrering,
        male => maleExiting.Where(exit => exit.Name == male.Name),
        female => femaleExiting.Where(exit => exit.Name == female.Name),
        (m, fs) => new { Male = m.Name, Females = fs });

/* analog with a LINQ query
 * 
 * from male in maleEntering
 * join female in femaleEntering on maleExiting.Where(e => e.Name == male.Name)
 * equals femaleExiting.Where(e => female.Name == e.Name)
 * into maleEncounters
 * select new { Male = male.Name, Females = maleEncounters };
 * 
 */

var amountPerUser =
    from acqaintances in malesAcquaintances
    from cnt in acqaintances.Females.Scan(0, (acc, curr) => acc + 1)
    select new { acqaintances.Male, cnt };

amountPerUser.SubscribeConsole("Amount of meetings for User");

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