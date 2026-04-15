using System;
using UnityEngine;

[CreateAssetMenu(fileName ="Store Data Event SO", menuName = "ScriptableObject/SaveLoad/Store Data")]
public class StoreDataEventSO : ScriptableObject
{
    public event Action<InteractionEvent> OnRaise;
    private Func<float> _timeProvider;

    public void Raise(InteractionEvent context)
    {
        if (_timeProvider != null)
        {
            context.timeStamp = _timeProvider();
        }
        OnRaise?.Invoke(context);
    }



    /// <summary>
    /// Only initialize this once. Will track time of the game
    /// </summary>
    /// <param name="timeProvider"></param>
    public void InitializeTimeProvider(Func<float> timeProvider)
    {
        _timeProvider = timeProvider;
    }

}
