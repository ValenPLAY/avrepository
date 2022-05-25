using UnityEngine;

public class SoundController : Singleton<SoundController>
{
    [SerializeField] AudioSource soundEffectPrefab;
    [SerializeField] float pitchVariation;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnSoundEffect(AudioClip incomingSound, Vector3 position)
    {
        AudioSource spawnedSound = Instantiate(soundEffectPrefab, position, Quaternion.identity);
        spawnedSound.clip = incomingSound;
        spawnedSound.PlayDelayed(Random.Range(0.0f, 0.1f));
    }
}
