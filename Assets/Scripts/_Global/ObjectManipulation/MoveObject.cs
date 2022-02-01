using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public enum Directions { up, down, left, right };

    public float speed = 100f;
    public Directions direction;

    private void Update()
    {
        if (direction.ToString() == "up")
            transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        if (direction.ToString() == "down")
            transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
        if (direction.ToString() == "right")
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        if (direction.ToString() == "left")
            transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
    }

}
