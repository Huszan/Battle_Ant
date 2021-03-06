using UnityEngine;

public class Factory<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField]
    private T prefab;

    public T GetNewInstance()
    {
        return Instantiate(prefab);
    }
}
