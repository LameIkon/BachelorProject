using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest Give Provider SO", menuName = "ScriptableObject/Quest/Quest func provider")]
public class QuestGiveProviderSO : ScriptableObject
{
    private Func<Quest> _provider;

    public void Register(Func<Quest> provider)
    {
        _provider = provider;
    }

    public void Unregister(Func<Quest> provider)
    {
        if (_provider == provider) _provider = null;
    }

    public Quest GetQuest()
    {
        if (_provider == null)
        {
            Debug.LogWarning("No provider registered!");
            return null;
        }
        return _provider.Invoke();
    }
}
