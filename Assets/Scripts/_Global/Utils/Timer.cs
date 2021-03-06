using UnityEngine;

public class Timer : MonoBehaviour
{
    public float Counter { get; private set; }
    private bool IsCounting = false;

    public void StartCounting() => IsCounting = true;
    public void StopCounting() => IsCounting = false;
    public void AddSeconds(float amount) => Counter += amount;
    public void ResetCounter() => Counter = 0f;
    public bool ReachedTreshold(float treshold) => Counter >= treshold;

    private void Update()
    {
        if (IsCounting)
            Counter += Time.deltaTime;
    }

}
