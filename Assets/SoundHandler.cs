using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    public AudioSource source;
    [SerializeField]
    internal AudioClip[] audioLibrary;
    public int LibrarySize() => audioLibrary.Length;
    public void PlaySound(int index)
    {
        source.clip = audioLibrary[index];
        source.Play();
    }
}
