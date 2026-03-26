using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Data/Level")]
public class LevelData : ScriptableObject
{
    [SerializeField, Header("The scenes to load for the level")] 
    private LevelName _name;
    [SerializeField] private SceneField[] _scenes;

    public LevelName Name => _name;
    public SceneField[] Scenes => _scenes;


}


