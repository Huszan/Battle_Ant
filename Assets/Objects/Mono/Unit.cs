using UnityEngine;
public class Unit : MonoBehaviour
{
    [Header("Fields")]
    public string _name;
    public float hp;
    public float movSpeed;
    public int carryAmount;
    public Player Owner { get; private set; }
    public JobBase Job { get; private set; }

    public void Damage(float amount)
    {
        if (amount < hp)
            hp -= amount;
        else
            Kill();
    }
    public void Kill() => Destroy(transform.gameObject);
    public void SetOwner(Player player) => Owner = player;
    public void SetJob(JobBase job) => Job = job;

    public void MoveToPoint(Vector2 point)
    {
        transform.position = Vector2.MoveTowards(
                transform.position,
                point,
                movSpeed * Time.deltaTime);
        RotateTowards(point);
    }
    private void RotateTowards(Vector2 point)
    {
        Vector3 movDir = transform.position - (Vector3)point;
        float angle = Mathf.Atan2(movDir.y, movDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90f, Vector3.forward);
    }
            

    void Update()
    {
        if (!Job.Finished)
            Job.Perform();
        else
            Job.Conclude();
    }

}
