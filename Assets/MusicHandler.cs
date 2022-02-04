using System.Collections;
using UnityEngine;

public class MusicHandler : SoundHandler
{
    public void Start()
    {
        StartCoroutine(LoopAllTracks());
    }
    public IEnumerator LoopAllTracks()
    {
        int currentIndex = LibrarySize();
        while (true)
        {
            if (currentIndex >= LibrarySize())
            {
                currentIndex = 0;
                ShuffleLibrary();
            }
            else if (!source.isPlaying)
                PlaySound(currentIndex++);
            yield return new WaitForSeconds(1);
        }
    }
}
