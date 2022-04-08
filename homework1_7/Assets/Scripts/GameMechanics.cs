using UnityEngine;

public class GameMechanics : MonoBehaviour
{
    static public AudioSource playerSound(AudioSource prefab, Vector3 soundPosition, float defaultPitch, float pitchRange)
    {
        AudioSource createdSoundObject = Instantiate(prefab, soundPosition, Quaternion.identity);
        //AudioSource soundSource = createdSoundObject.GetComponent<AudioSource>();
        createdSoundObject.pitch = Random.Range(defaultPitch - pitchRange, defaultPitch + pitchRange);

        if (defaultPitch < 0.0f)
        {
            createdSoundObject.timeSamples = createdSoundObject.clip.samples - 1;
        }

        return createdSoundObject;
    }
}
