using UnityEngine;

[CreateAssetMenu(fileName = "Compendium Content SO", menuName = "ScriptableObject/Compendium/content")]
/// <summary>
/// For compendium to know what image and text content to show. CompendiumContent is the text data
/// </summary>
public class CompendiumContentSO : ScriptableObject
{
    public CompendiumID compendiumID;
    public Sprite image;
    public CompendiumContent content;
}