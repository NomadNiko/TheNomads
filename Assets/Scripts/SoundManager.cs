using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class manages all audio functionality in the game.
public class SoundManager : MonoBehaviour {
    // Make Singleton Instance
    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }

    private SoundList soundList;
    private Dictionary<string, AudioClip> soundDict = new Dictionary<string, AudioClip>();
    private AudioSource audioSource;

    private void Awake() {
        soundList = FindObjectOfType<SoundList>();
        audioSource = GetComponent<AudioSource>();
        SetInstance();
        InitializeSoundList();
    }

    private void SetInstance() {
        // Ensuring only one instance of SoundManager exists in the scene.
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }

    }

    
    private void InitializeSoundList() {
        // This method populates the dictionary with the sounds from the sound list.

        foreach (var sound in soundList.sounds) {
            soundDict.Add(sound.SoundName, sound.AudioClip);
        }
    }

    // Overloaded PlaySound methods for playing sounds with different parameters.

    // Plays specified sound once at full volume.
    public void PlaySound(string soundName) {
        PlaySound(soundName, 1f, 1);
    }

    // Plays specified sound once at specified volume.
    public void PlaySound(string soundName, float volume) {
        PlaySound(soundName, volume, 1);
    }

    // Plays specified sound with specified volume and loop count.
    public void PlaySound(string soundName, float volume, int loopCount) {
        // Checking if sound exists in the dictionary.
        if (soundDict.ContainsKey(soundName)) {
            // Setting volume and clip of the AudioSource.
            audioSource.volume = volume;
            audioSource.clip = soundDict[soundName];

            // If loop count is greater than 1, enable looping and start coroutine.
            if (loopCount > 1) {
                audioSource.loop = true;
                audioSource.Play();
                StartCoroutine(PlayLooped(loopCount));
            } else {
                // If loop count is 1 or less, play the sound once.
                audioSource.loop = false;
                audioSource.PlayOneShot(soundDict[soundName]);
            }
        }
    }

    // Coroutine for playing the sound multiple times with a delay.
    private IEnumerator PlayLooped(int loopCount) {
        for (int i = 0; i < loopCount; i++) {
            // Wait for the duration of the audio clip before continuing.
            yield return new WaitForSeconds(audioSource.clip.length);
        }
        // Stop looping after the sound has played the specified number of times.
        audioSource.loop = false;
    }
}
