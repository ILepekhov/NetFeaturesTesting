using System.Reactive.Linq;

namespace FlatteringObservablesOfObservables
{
    public sealed class ChatRoom
    {
        #region Properties

        public string Id { get; }

        public IObservable<ChatMessage> Messages { get; }

        #endregion

        #region Constructor

        public ChatRoom(string id, IObservable<ChatMessage> messages)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));
            }

            if (messages is null)
            {
                throw new ArgumentNullException(nameof(messages));
            }

            Id = id;
            Messages = messages;
        }

        #endregion
    }
}
