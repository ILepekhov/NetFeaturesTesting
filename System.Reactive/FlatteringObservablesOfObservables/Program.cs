using ExtensionsLibrary;
using FlatteringObservablesOfObservables;
using System.Reactive.Linq;
using System.Reactive.Subjects;

Subject<ChatRoom> roomsSubject = new();
IObservable<ChatRoom> rooms = roomsSubject.AsObservable();

rooms
    .Log("Rooms")
    .SelectMany(room => room.Messages, (room, message) => new ChatMessageViewModel(message) { RoomName = room.Id })
    .Subscribe(vm => AddToDashboard(vm));

void AddToDashboard(ChatMessageViewModel messageVM)
{
    if (messageVM is null)
    {
        throw new ArgumentNullException(nameof(messageVM));
    }

    Console.WriteLine($"Message from '{messageVM.RoomName}': {messageVM.Message} was sent by user '{messageVM.Name}' (id: {messageVM.Id})");
}

Subject<ChatMessage> room1 = new();
roomsSubject.OnNext(new ChatRoom("Room1", room1));
room1.OnNext(new ChatMessage { Content = "First message", Sender = 1 });
room1.OnNext(new ChatMessage { Content = "Second message", Sender = 1 });

Subject<ChatMessage> room2 = new();
roomsSubject.OnNext(new ChatRoom("Room2", room2));
room2.OnNext(new ChatMessage { Content = "Hello World", Sender = 2 });

room1.OnNext(new ChatMessage { Content = "Another message", Sender = 1 });

Console.ReadLine();
