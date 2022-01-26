using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBarHandler : MonoBehaviour
{
    public Building building;
    public SpriteRenderer spriteRenderer;

    private float Max { get; set; }
    private float Current { get; set; }
    private void Awake()
    {
        Max = building.hp;
        Current = Max;
    }
    void Update()
    {
        Current = building.hp;
        var percent = Current / Max;
        if (percent < 1f)
        {
            spriteRenderer.enabled = true;
            gameObject.transform.localScale = new Vector3(percent / 2, 0.2f, 1f);
        }
    }
}
