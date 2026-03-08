using UnityEngine;

[CreateAssetMenu(fileName = "Compendium Content SO", menuName = "ScriptableObject/Compendium/content")]
public class CompendiumContentSO : ScriptableObject
{
    public Sprite image;
    public CompendiumContent content;
}