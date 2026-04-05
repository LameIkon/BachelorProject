using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "localized Content SO", menuName = "ScriptableObject/Localization/Content")]
/// <summary>
/// For compendium to know what image and text content to show. CompendiumContent is the text data
/// </summary>
public class LocalizedContentSO : ScriptableObject
{
    public List<LocalizedText> entries;

    public LocalizedText Get(Language language)
    {
        foreach (LocalizedText entry in entries)
        {
            if (entry.language == language) return entry;
        }

        return null;
    }
}