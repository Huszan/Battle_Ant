using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeArtwork : MonoBehaviour
{
    public Sprite[] Artworks;
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = Artworks[
            Random.Range(0, Artworks.Length)];
    }
}
