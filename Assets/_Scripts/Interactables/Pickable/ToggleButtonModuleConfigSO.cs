using UnityEngine;

[CreateAssetMenu(fileName = "Toggle Button Interaction SO", menuName = "ScriptableObject/Interactable/Interaction Type/Toggle Button")]
public class ToggleButtonModuleConfigSO : InteractionBehaviourConfigSO
{

    [SerializeField] private AudioPlayerSO _audioPlayer;

    public void PlaySound(AudioSource source) 
    {
        if (_audioPlayer == null) return; 
        _audioPlayer.PlaySound(source);
    }

    /// <summary>
    /// Button module will contain the logic of interaction
    /// </summary>
    /// <param name="owner"></param>
    /// <returns></returns>
    public override InteractionModuleResult Create(GameObject owner, InteractionIdentitySO identity,  IInteractionEvent interactionEvent, AudioSource source)
    {
        if (identity is not ToggleButtonInteractionIdentitySO toggleButtonIdentity)
        {
            Debug.LogError($"Expected {nameof(ToggleButtonInteractionIdentitySO)} but got {identity?.GetType().Name}");
            return default;
        }

        WorldToggleButton module = new WorldToggleButton(owner, this, toggleButtonIdentity, source);
        return new InteractionModuleResult
        {
            interaction = module,
        };
    }
}