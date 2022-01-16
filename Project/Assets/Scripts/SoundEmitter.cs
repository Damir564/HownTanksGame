using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// [RequireComponent(typeof(AudioSource))]
public class SoundEmitter : MonoBehaviour
{
    // // private AudioSource _audioSource;
	// [SerializeField]
	// private AudioSource _audioSource;

	// // public event UnityAction<SoundEmitter> OnSoundFinishedPlaying;

	// private void Awake()
	// {
	// 	// Debug.Log("before this.GetComponent" + _audioSource);
	// 	// _audioSource = this.GetComponent<AudioSource>();
	// 	// Debug.Log("after this.GetComponent" + _audioSource);
	// 	// _audioSource.playOnAwake = false;
	// }

	// /// <summary>
	// /// Instructs the AudioSource to play a single clip, with optional looping, in a position in 3D space.
	// /// </summary>
	// /// <param name="clip"></param>
	// /// <param name="settings"></param>
	// /// <param name="hasToLoop"></param>
	// /// <param name="position"></param>
	public void PlayAudioClip(AudioSource auu)
	{
		auu.enabled = true;
		auu.Play();
		// _audioSource.clip = clip;
		// settings.ApplyTo(_audioSource);
		// _audioSource.transform.position = position;
		// _audioSource.loop = hasToLoop;
		// _audioSource.Play();

		// if (!hasToLoop)
		// {
		// 	StartCoroutine(FinishedPlaying(clip.length));
		// }
	}
    // IEnumerator FinishedPlaying(float clipLength)
	// {
	// 	yield return new WaitForSeconds(clipLength);

	// 	_audioSource.Stop(); // The AudioManager will pick this up
	// }
}
