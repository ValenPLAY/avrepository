using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public float defaultDuration = 5.0f;
    private float currentDuration;
    // Start is called before the first frame update
    void Awake()
    {
        currentDuration = defaultDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDuration > 0)
        {
            currentDuration -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
