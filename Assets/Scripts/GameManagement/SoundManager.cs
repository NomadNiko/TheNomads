using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class manages all audio functionality in the game.
public class SoundManager : MonoBehaviour {
    // Make Singleton Instance
    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }

    [SerializeField] private SoundList soundList;

    private Dictionary<string, AudioClip> soundDict = new Dictionary<string, AudioClip>();
    private AudioSource audioSource;

    private void Awake() {
        
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
    public void PlaySound(string soundName1, string soundName2, float volume, int loopCount) {
        PlayMultipleSounds(new List<string> { soundName1, soundName2 }, volume, loopCount);
    }

    // Overloaded PlaySound method to play three sounds.
    public void PlaySound(string soundName1, string soundName2, string soundName3, float volume, int loopCount) {
        PlayMultipleSounds(new List<string> { soundName1, soundName2, soundName3 }, volume, loopCount);
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

    public void PlayMultipleSounds(List<string> soundNames, float volume, int loopCount) {
        // Start the coroutine for playing multiple sounds in a loop.
        StartCoroutine(PlayMultipleLooped(soundNames, volume, loopCount));
    }

    private IEnumerator PlayMultipleLooped(List<string> soundNames, float volume, int loopCount) {
        // Repeat the sequence of sounds the specified number of times.
        for (int i = 0; i < loopCount; i++) {
            // Cycle through each sound in the list.
            foreach (string soundName in soundNames) {
                // Check if the sound exists in the dictionary.
                if (soundDict.ContainsKey(soundName)) {
                    // Set the volume and clip of the AudioSource.
                    audioSource.volume = volume;
                    audioSource.clip = soundDict[soundName];

                    // Play the sound.
                    audioSource.Play();

                    // Wait for the duration of the audio clip before continuing.
                    yield return new WaitForSeconds(audioSource.clip.length);
                }
            }
        }
    }
}
