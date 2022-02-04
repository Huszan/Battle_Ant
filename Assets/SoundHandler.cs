using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    public AudioSource source;
    [SerializeField]
    internal List<AudioClip> audioLibrary;
    public int LibrarySize() => audioLibrary.Count;
    public void PlaySound(int index)
    {
        source.clip = audioLibrary[index];
        source.Play();
    }
    internal void ShuffleLibrary()
    {
        List<AudioClip> newLibrary = new List<AudioClip>();

        while (audioLibrary.Count > 0)
        {
            var random = Random.Range(0, audioLibrary.Count);
            newLibrary.Add(audioLibrary[random]);
            audioLibrary.RemoveAt(random);
        }

        audioLibrary = newLibrary;
            
    }

}
