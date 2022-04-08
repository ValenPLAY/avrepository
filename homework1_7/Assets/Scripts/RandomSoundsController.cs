using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundsController : MonoBehaviour
{
    public GameObject soundPlayerPrefab;
    public List<AudioClip> soundClips = new List<AudioClip>();
    
    public float soundDelay = 10;
    private float soundDelayCurrent;
    // Start is called before the first frame update
    void Start()
    {
        soundDelayCurrent = soundDelay * Random.Range(0.2f,5f);
    }

    // Update is called once per frame
    void Update()
    {
        soundDelayCurrent -= Time.deltaTime;
        if (soundDelayCurrent<=0)
        {
            soundDelayCurrent = soundDelay * Random.Range(0.2f, 5f);
            Vector3 randomSoundPosition = new Vector3(Random.Range(-5.0f,5.0f), Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f));
            randomSoundPosition += transform.position;
            GameObject currentRandomSound = Instantiate(soundPlayerPrefab, randomSoundPosition, transform.rotation);
            AudioSource audioSource = currentRandomSound.GetComponent<AudioSource>();
            audioSource.clip = soundClips[Random.Range(0, soundClips.Count - 1)];
            audioSource.Play();
            Destroy(currentRandomSound, 30);
        }
    }
}
