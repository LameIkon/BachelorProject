using UnityEngine;

[CreateAssetMenu(fileName = "Button Interaction SO", menuName = "ScriptableObject/Interactable/Button")]
public class BottonModuleConfigSO : InteractionBehaviourConfigSO
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
        if (identity is not ButtonInteractionIdentitySO buttonDef)
        {
            Debug.LogError($"Expected {nameof(ButtonInteractionIdentitySO)} but got {identity?.GetType().Name}");
            return default;
        }

        WorldButton module = new WorldButton(owner, this, buttonDef, source);
        return new InteractionModuleResult
        {
            interaction = module,
        };
    }
}
