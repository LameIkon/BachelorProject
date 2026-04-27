using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class AudioLooper : MonoBehaviour
{
    [SerializeField] private AudioPlayer _player;

    private AudioSource _source;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _source = GetComponent<AudioSource>();
        _player.PlaySound(_source);
    }


}
