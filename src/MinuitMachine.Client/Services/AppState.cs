using System;

namespace MinuitMachine.Client.Services;

public class AppState
{
    public event Action<bool>? ConnectionChanged;

    public void NotifyConnectionChanged(bool connected)
    {
        ConnectionChanged?.Invoke(connected);
    }
}
