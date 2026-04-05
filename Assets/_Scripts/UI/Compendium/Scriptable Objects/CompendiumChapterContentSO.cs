using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Compendium Chapter Content SO", menuName = "ScriptableObject/Compendium/Chapter")]
/// <summary>
/// For holding compendium content
/// </summary>
public class CompendiumChapterContentSO : ScriptableObject
{
    public string chapterTitle;
    public List<CompendiumContentSO> compendiumContentSO;
}