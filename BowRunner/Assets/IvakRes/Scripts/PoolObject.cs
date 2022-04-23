using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }

    public void ReturnToPool(float time)
    {
        Invoke("ReturnToPool", time);
    }
}
