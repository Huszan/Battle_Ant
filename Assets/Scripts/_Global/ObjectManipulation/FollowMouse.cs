using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public bool snapToGrid;

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(GetMousePosition(), Vector2.zero);
        if (snapToGrid && hit.collider != null && hit.collider.gameObject.CompareTag("Tile"))
            gameObject.transform.position = hit.collider.transform.position;
        else
            Follow();
    }
    private Vector3 GetMousePosition() => Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
    private void Follow() => gameObject.transform.position = GetMousePosition();
}
