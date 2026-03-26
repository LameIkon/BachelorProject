using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Data/Level")]
public class LevelData : ScriptableObject
{
    [SerializeField, Header("The scenes to load for the level")] 
    private LevelName _name;
    [SerializeField] private InputState _inputState;
    [SerializeField] private SceneField[] _scenes;


    public LevelName Name => _name;
    public InputState GameState => _inputState;
    public SceneField[] Scenes => _scenes;


}


