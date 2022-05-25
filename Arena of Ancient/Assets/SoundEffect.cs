using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    private float defaultDuration = 5.0f;
    private float delayCompensation = 1.0f;
    private float currentDuration;
    public AudioSource soundEffectSource;
    // Start is called before the first frame update
    void Awake()
    {
        UpdateDestroyTime(defaultDuration);
        soundEffectSource = GetComponent<AudioSource>();
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

    public void UpdateDestroyTime(float duration)
    {
        defaultDuration = duration + delayCompensation;
        currentDuration = defaultDuration;
    }
}
