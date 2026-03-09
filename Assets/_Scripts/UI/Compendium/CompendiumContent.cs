using System;
using UnityEngine;

[Serializable]
public class CompendiumContent
{
    [SerializeField] private LocalizedContent _danish;
    [SerializeField] private LocalizedContent _english;

    public LocalizedContent Get(Language language) // Get the content depending on language
    {
        switch (language)
        {
            case Language.Danish:
                return _danish;
            case Language.English:
            default:
                return _english;
        }
    }
}
