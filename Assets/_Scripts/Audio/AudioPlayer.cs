using System;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Audio Player", menuName = "ScriptableObject/Audio/Player")]
public class AudioPlayer : ScriptableObject, ISoundPlayer
{
	[SerializeField] AudioMixerGroup _mixerGroup;
	[SerializeField] AudioClip[] _clips;
	[SerializeField, Tooltip("This value goes from 0 to 1")] RangedFloat _volumeRange;
	[SerializeField, Tooltip("This value goes from -3 to 3")] RangedFloat _pitchRange;
	[SerializeField] bool _is3D;



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

		if (_is3D) source.spatialBlend = 1;
		else source.spatialBlend = 0;

		source.clip = _clips[Random.Range(0, _clips.Length)];
		source.volume = _volumeRange.Value;
		source.pitch = _pitchRange.Value;

		source.Play();
	
	}


}

[Serializable]
public class RangedFloat
{
	[SerializeField] private float _minValue;
	[SerializeField] private float _maxValue;

	public float Value 
	{
		get 
		{
			return Random.Range(_minValue, _maxValue);
		}
	}

}



public interface ISoundPlayer 
{
	public void PlaySound(AudioSource source);
}