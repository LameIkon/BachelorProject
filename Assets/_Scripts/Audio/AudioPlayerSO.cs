using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Audio Player", menuName = "ScriptableObject/Audio/Player")]
public class AudioPlayerSO : ScriptableObject, ISoundPlayer
{
	[SerializeField] AudioMixerGroup _mixerGroup;
	[SerializeField] AudioClip[] _clips;
	[SerializeField, RangedFloat(0,1)] RangedFloat _volumeRange;
	[SerializeField, RangedFloat(-3,3)] RangedFloat _pitchRange;
	[SerializeField] bool _is3D;
	[SerializeField] bool _loops;


	public void PlaySound(AudioSource source) 
	{
		// Null checkers
		if (_mixerGroup == null) 
		{
			Debug.LogError($"{this.name}: Does not have a _mixerGroup attached!");
			return;
		}
		if (_clips == null) 
		{
			Debug.LogError($"{this.name}: Does not have any clips to play!");
			return;
		}

		source.loop = _loops;

		if (_is3D) source.spatialBlend = 1;
		else source.spatialBlend = 0;

		source.clip = _clips[Random.Range(0, _clips.Length)];
		source.volume = _volumeRange;
		source.pitch = _pitchRange;

		source.Play();
	}


}

public interface ISoundPlayer 
{
	public void PlaySound(AudioSource source);
}