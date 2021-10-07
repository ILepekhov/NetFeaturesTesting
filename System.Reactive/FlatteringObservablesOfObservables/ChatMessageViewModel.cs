namespace FlatteringObservablesOfObservables
{
    public sealed class ChatMessageViewModel
    {
        #region Fields

        private readonly ChatMessage _message;

        #endregion

        #region Properties

        public string Name => GetName(_message.Sender);

        public int Id => _message.Sender;

        public string Message => _message.Content;

        public string RoomName => _message.Room;

        #endregion

        #region Constructor

        public ChatMessageViewModel(ChatMessage message)
        {
            _message = message ?? throw new ArgumentNullException(nameof(message));
        }

        #endregion

        #region Helpers

        private string GetName(int id)
        {
            switch (id)
            {
                case 0: return "Anonymous";
                case 1: return "Ivan";
                case 2: return "Maksim";
                case 3: return "Valery";
                default: return "Kyrill";
            }
        }

        #endregion
    }
}
