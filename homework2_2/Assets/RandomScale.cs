using UnityEngine;

public class RandomScale : MonoBehaviour
{
    public Vector2 randomScaleRange = Vector3.one;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(Random.Range(randomScaleRange.x, randomScaleRange.y), Random.Range(randomScaleRange.x, randomScaleRange.y), Random.Range(randomScaleRange.x, randomScaleRange.y));
    }
}
