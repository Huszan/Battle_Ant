using UnityEngine;
public class Unit : MonoBehaviour
{
    [Header("Fields")]
    public string _name;
    public float hp;
    public float cost;
    public float range;
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

    public void MoveToPoint(Vector2 point) =>
            transform.position = Vector2.MoveTowards(
                transform.position, 
                point, 
                movSpeed * Time.deltaTime);

    void Update()
    {
        if (!Job.Finished)
            Job.Perform();
        else
            Job.Conclude();
    }

}
