using UnityEngine;

public class GameMechanics : MonoBehaviour
{
    static public GameObject playerSound(GameObject prefab, Vector3 soundPosition, float defaultPitch, float pitchRange)
    {
        GameObject createdSoundObject = Instantiate(prefab, soundPosition, Quaternion.identity);
        AudioSource soundSource = createdSoundObject.GetComponent<AudioSource>();
        soundSource.pitch = Random.Range(defaultPitch - pitchRange, defaultPitch + pitchRange);

        if (defaultPitch < 0.0f)
        {
            soundSource.timeSamples = soundSource.clip.samples - 1;
        }

        return createdSoundObject;
    }
}
