using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Data/Level")]
public class LevelData : ScriptableObject
{
    [SerializeField, Header("The scenes to load for the level")]
    private SceneField[] _scenes;
    [SerializeField] private InputState _inputState;


    public InputState GameState => _inputState;
    public SceneField[] Scenes => _scenes;
    public int Id => name.GetHashCode();

}


