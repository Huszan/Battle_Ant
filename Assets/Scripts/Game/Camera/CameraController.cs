using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    [Header("Control")]
    public float panSpeed = 20f;
    public float panSpeedMultiplayer = 2f;
    public float scrollSpeed = 2f;
    public Vector2 scrollLimit = new Vector2(10, 20);

    private Camera mainCamera;
    private float currentSpeed;

    private void Start()
    {
        currentSpeed = panSpeed;
        mainCamera = GetComponent<Camera>();
        if (mainCamera == null)
            Debug.LogError(name + ": there is no camera detected!");
    }

    private void Update()
    {
        if (CanMove())
            ManageCameraMovement();
    }

    private void ManageCameraMovement()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
            pos.y += currentSpeed * Time.deltaTime;
        if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))
            pos.y -= currentSpeed * Time.deltaTime;
        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
            pos.x += currentSpeed * Time.deltaTime;
        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
            pos.x -= currentSpeed * Time.deltaTime;

        Vector3 mouseMovement = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0) * (mainCamera.orthographicSize/26);
        if (Input.GetKey(KeyCode.Mouse2))
            pos -= mouseMovement;

        if (Input.GetKey(KeyCode.LeftShift))
            currentSpeed = panSpeed * panSpeedMultiplayer;
        else
            currentSpeed = panSpeed;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        mainCamera.orthographicSize += -scroll * scrollSpeed * Time.deltaTime;

        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, scrollLimit.x, scrollLimit.y);

        Vector2 panLimit = 
            Tilemap.Instance.TransformSize;
        pos.x = Mathf.Clamp(pos.x, 0, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, 0, panLimit.y * 0.75f);

        transform.position = pos;
    }
    private bool CanMove()
    {
        if (GameManager.Instance.GameState == GameState.PLAYING ||
            GameManager.Instance.GameState == GameState.PAUSED)
            return true;
        else
            return false;
    }

}
