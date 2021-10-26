internal class Messenger
{
    #region Events

    public event EventHandler<string>? MessageReceived;

    #endregion

    #region Methods

    public void RaiseMessageReceived()
    {
        for (int i = 0; i < 3; i++)
        {
            Task.Factory.StartNew(obj => MessageReceived?.Invoke(this, obj.ToString()), "Msg" + i);
        }
    }

    #endregion
}