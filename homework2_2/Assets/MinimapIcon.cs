using UnityEngine;

public class MinimapIcon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(-90f, 0f, 180f);
    }
}
