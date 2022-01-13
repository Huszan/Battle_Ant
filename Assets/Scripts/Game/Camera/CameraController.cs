using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 20f;
    public float panSpeedMultiplayer = 2f;
    public float scrollSpeed = 2f;
    public Vector2 scrollLimit = new Vector2(10, 20);

    private Tilemap tilemap;
    private Camera mainCamera;
    private float currentSpeed;

    private void Start()
    {
        currentSpeed = panSpeed;
        mainCamera = GetComponent<Camera>();
        if (mainCamera == null)
            Debug.LogError(name + ": there is no camera detected!");
        LookForTilemap();
    }

    private void Update()
    {
        ManageCameraMovement();
    }

    private void ManageCameraMovement()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
        {
            pos.y += currentSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))
        {
            pos.y -= currentSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            pos.x += currentSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            pos.x -= currentSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = panSpeed * panSpeedMultiplayer;
        }
        else
            currentSpeed = panSpeed;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        mainCamera.orthographicSize += -scroll * scrollSpeed * Time.deltaTime;

        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, scrollLimit.x, scrollLimit.y);

        Vector2 panLimit = tilemap.GetSize();
        pos.x = Mathf.Clamp(pos.x, 0, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, 0, panLimit.y * 0.75f);

        transform.position = pos;
    }
    private void LookForTilemap()
    {
        var tag = "Tilemap";
        var go = GameObject.FindGameObjectWithTag("Tilemap");
        if (go == null)
            Debug.LogError(name + ": I can't find gameobject with tag " + tag);
        else
        {
            tilemap = go.GetComponent<Tilemap>();
        }
    }

}
