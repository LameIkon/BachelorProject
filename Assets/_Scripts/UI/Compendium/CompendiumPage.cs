using UnityEngine;
/// <summary>
/// Page for the compendium. CompendiumContent to hold the data such as text, while CompendiumUIReferences holds the ui references
/// </summary>
public class CompendiumPage : MonoBehaviour
{
    [SerializeField] private CompendiumContent _content;
    [SerializeField] private CompendiumUIReferences _references;
}
