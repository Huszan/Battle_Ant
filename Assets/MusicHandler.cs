using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : SoundHandler
{
    public void Start()
    {
        StartCoroutine(LoopAllTracks());
    }
    public IEnumerator LoopAllTracks()
    {
        int currentIndex = 0;
        while (true)
        {
            if (currentIndex >= LibrarySize())
                currentIndex = 0;
            else if (!source.isPlaying)
                PlaySound(currentIndex++);
            yield return new WaitForSeconds(1);
        }
    }
}
