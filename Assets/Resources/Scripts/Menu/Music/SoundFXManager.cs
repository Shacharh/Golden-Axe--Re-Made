using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
   
   public static SoundFXManager Instance;

    [SerializeField] private AudioSource soundFXObject;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void PlaySoundFXClip(AudioClip clip, Transform spawnTransform, float volume)
    {
        //spawn in the game object
        AudioSource audioSource = Instantiate (soundFXObject, spawnTransform.position, Quaternion.identity);
        // assign the audioclip
        audioSource.clip = clip;
        // assign volume contol
        audioSource.volume = volume;
        //play sound
        audioSource.Play();
        //get length of soundFX clip
        float clipLeangth = audioSource.clip.length;
        //destroy the clip once it is done
        Destroy(audioSource.gameObject, clipLeangth);
    }
    public void PlayRandomSoundFXClip(AudioClip[] clip, Transform spawnTransform, float volume)
    {
        //assign a random index
        int rand = Random.Range(0, clip.Length);
        //spawn in the game object
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        // assign the audioclip
        audioSource.clip = clip[rand];
        // assign volume contol
        audioSource.volume = volume;
        //play sound
        audioSource.Play();
        //get length of soundFX clip
        float clipLeangth = audioSource.clip.length;
        //destroy the clip once it is done
        Destroy(audioSource.gameObject, clipLeangth);
    }
}
