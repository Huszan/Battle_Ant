using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public float timeToDestroy;

    private float timeElapsed = 0f;
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= timeToDestroy)
            Destroy(transform.gameObject);
    }
}
