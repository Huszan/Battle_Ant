using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Tilemap tilemap;
    public float speed = 20f;
    public float speedMultiplayer = 2f;

    private Camera mainCamera;
    private float currentSpeed;

    private void Start()
    {
        currentSpeed = speed;
        mainCamera = GetComponent<Camera>();
        if (mainCamera == null)
            Debug.LogError(name + ": there is no camera detected!");
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
            currentSpeed = speed * speedMultiplayer;
        }
        else
            currentSpeed = speed;

        Vector2 panLimit = tilemap.GetSize();
        Debug.Log(panLimit);
        pos.x = Mathf.Clamp(pos.x, 0, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, 0, panLimit.y * 0.75f);

        transform.position = pos;
    }

}
