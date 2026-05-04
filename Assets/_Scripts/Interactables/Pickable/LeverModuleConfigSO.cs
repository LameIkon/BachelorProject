using UnityEngine;

[CreateAssetMenu(fileName = "Lever Interaction SO", menuName = "ScriptableObject/Interactable/Interaction Type/Lever")]
public class LeverModuleConfigSO : InteractionBehaviourConfigSO
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
        if (identity is not LeverInteractionIdentitySO buttonIdentity)
        {
            Debug.LogError($"Expected {nameof(LeverInteractionIdentitySO)} but got {identity?.GetType().Name}");
            return default;
        }

        WorldLever module = new WorldLever(owner, this, buttonIdentity, source);
        return new InteractionModuleResult
        {
            interaction = module,
        };
    }
}
