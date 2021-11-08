using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MainCameraManager
{
    public static void CenterToMap(Vector2 mapSize, Vector3 tileSize)
    {
        Camera.main.transform.position =
            Camera.main.transform.position +
            new Vector3(
                mapSize.x * (float)(tileSize.x * .5), 
                mapSize.y * (float)(tileSize.y * .5 * .75));
    }
}
