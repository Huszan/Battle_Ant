using UnityEngine;

public class RandomOffsetGenerator : MonoBehaviour
{
    public static RandomOffsetGenerator Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    [SerializeField]
    private Vector2 range;
    public Vector2 RandomOffset { get; private set; }
    public Vector2 GetRange()
    {
        return range;
    }

    void Update()
    {
        RandomOffset = new Vector2(
            Random.Range(range.x, range.y),
            Random.Range(range.x, range.y));
    }
}
