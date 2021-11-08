using UnityEngine;
using UnityEngine.UI;

public class BackgroundHandler : MonoBehaviour
{
    [Header("Entity options")]
    public GameObject entityToSpawn;
    public float sizeMin = 0.1f;
    public float sizeMax = 2f;
    public float speedMin = 80f;
    public float speedMax = 230f;
    public Color[] colors;
    [Header("Spawn options")]
    public float timeBetweenSpawns = 0.5f;
    public int spawnAmount = 1;

    private float timePassed = 0f;

    private void FixedUpdate()
    {
        timePassed += Time.deltaTime;

        if (timePassed >= timeBetweenSpawns)
        {
            for (int i = 0; i < spawnAmount; i++) SpawnEntity();
            timePassed = 0f;
        }
    }

    private void SpawnEntity()
    {
        GameObject entity = Instantiate(entityToSpawn);

        entity.transform.SetParent(transform);
        entity.transform.position = new Vector3(
            Random.Range(0, Screen.width),
            sizeMax * -50,
            0);
        float scale = Random.Range(sizeMin, sizeMax);
        entity.transform.localScale = new Vector3(
            scale,
            scale,
            scale);

        SetupComponents(entity);
    }
    private void SetupComponents(GameObject entity)
    {
        MoveObject mover = entity.AddComponent<MoveObject>();
        mover.speed = Random.Range(speedMin, speedMax);
        mover.direction = MoveObject.Directions.up;

        DestroyObject destroyer = entity.AddComponent<DestroyObject>();
        destroyer.timeToDestroy = Screen.height / speedMin + 5;

        Image image = entity.GetComponent<Image>();
        if (colors.Length > 0)
            image.color = colors[Random.Range(0, colors.Length)];
    }
}
