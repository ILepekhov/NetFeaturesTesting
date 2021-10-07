namespace FlatteringObservablesOfObservables
{
    public sealed class ChatMessage
    {
        #region Properties

        public string Content { get; set; }

        public int Sender { get; set; }

        #endregion

        #region Constructor

        public ChatMessage()
        {
            Content = string.Empty;
            Sender = 0;
        }

        #endregion
    }
}
