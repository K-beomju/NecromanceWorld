using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManger : MonoBehaviour
{
     public static void CreateAndPlay(AudioClip clip, GameObject parent, Transform Instposition, float destructionTime, float volume = 1f)
    {
        AudioSource audioSource = parent.AddComponent<AudioSource>();
        audioSource.transform.position = Instposition.position;
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.spatialBlend = 1;
        audioSource.Play();
        Destroy(audioSource, destructionTime);
    }
}
