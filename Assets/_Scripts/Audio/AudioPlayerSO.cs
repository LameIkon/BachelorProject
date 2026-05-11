using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Audio Player", menuName = "ScriptableObject/Audio/Player")]
public class AudioPlayerSO : ScriptableObject
{
	[SerializeField] AudioMixerGroup _mixerGroup;
	[SerializeField] AudioClip[] _clips;
	[SerializeField, RangedFloat(0,1)] RangedFloat _volumeRange;
	[SerializeField, RangedFloat(-3,3)] RangedFloat _pitchRange;
	[SerializeField] bool _is3D;
	[SerializeField] bool _loops;
	[Header("Fadeout options")]
	[SerializeField, Tooltip("If this is zero the fadeout will not change pitch")] float _pitchChange;
	[SerializeField, Tooltip("Should the pitch change be up or down?")] bool _up; 
	[SerializeField] float _stopTime;

	private List<AudioSource> sources = new List<AudioSource>();


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

		if (!sources.Contains(source)) 
		{
			sources.Add(source);
		}

		source.loop = _loops;

		if (_is3D) source.spatialBlend = 1;
		else source.spatialBlend = 0;

		source.clip = _clips[Random.Range(0, _clips.Length)];
		source.volume = _volumeRange;
		source.pitch = _pitchRange;

		source.Play();
	}

	private void StopAll() 
	{
		if (sources.Count > 0)
		{
			foreach (AudioSource source in sources) 
			{
				source.Stop();
			}
			sources.Clear();
		}
	}

	public IEnumerator Fadeout() 
	{
        if (sources.Count > 0)
        {
            foreach (AudioSource source in sources)
            {
				for (float f = source.volume; f > 0; f -= _stopTime)
				{
					source.volume = f;
					source.pitch = _up ? source.pitch += _stopTime : source.pitch -= _stopTime;
					yield return null;
				}
            }
			sources.Clear();
        }
    }

}