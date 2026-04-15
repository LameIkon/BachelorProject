using System;

public class SessionTimeProvider
{
    private readonly Func<float> _getTime;

    public SessionTimeProvider(Func<float> getTime)
    {
        _getTime = getTime;
    }

    public float GetTime() => _getTime();
}
