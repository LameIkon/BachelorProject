using UnityEngine;

[CreateAssetMenu(fileName = "Pickup Identity SO", menuName = "ScriptableObject/Interactable/Identity/Pickup")]
public class PickupInteractionIdentitySO : InteractionIdentitySO
{
    public PickableType type;
    public bool disablePickupOnPlacement;
    public float weight;

    public AudioPlayerSO _audioPlayer;

    public void PlayAudio(AudioSource source)
    {
        if (_audioPlayer == null) return;
        _audioPlayer.PlaySound(source);
    }
    

}
