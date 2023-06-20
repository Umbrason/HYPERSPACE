using System;
using System.Collections.Generic;

public static class PauseManager
{
    private static HashSet<Guid> pauseHandles = new();
    public static event Action OnPause;
    public static event Action OnResume;

    public static Guid Pause()
    {
        var pauseHandle = Guid.NewGuid();
        pauseHandles.Add(pauseHandle);
        if (pauseHandles.Count == 1) OnPause?.Invoke();
        return pauseHandle;
    }

    public static void Resume(Guid pauseHandle)
    {
        pauseHandles.Remove(pauseHandle);
        if (pauseHandles.Count == 0) OnResume?.Invoke();
    }
}
