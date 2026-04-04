using System;
using UnityEngine;

[CreateAssetMenu(fileName ="Compendium Page Provider SO", menuName = "ScriptableObject/Compendium/Page func provider")]
public class CompendiumPageProviderSO : ScriptableObject
{
    private Func<CompendiumID, CompendiumPage> _provider;

    public void Register(Func<CompendiumID, CompendiumPage> provider)
    {
        _provider = provider;
    }

    public void Unregister(Func<CompendiumID, CompendiumPage> provider)
    {
        if (_provider == provider) _provider = null;
    }

    public CompendiumPage GetPage(CompendiumID id)
    {
        if (_provider == null)
        {
            Debug.LogWarning("No provider registered!");
            return null;
        }    
        return _provider.Invoke(id);
    }
}
