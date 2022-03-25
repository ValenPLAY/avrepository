using UnityEngine;

public class MusicController : MonoBehaviour
{
    private AudioSource musicController;
    public Character playableCharacter;
    // Start is called before the first frame update
    void Start()
    {
        musicController = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        musicController.pitch = playableCharacter.playerHealthCurrent / playableCharacter.playerHealthDefault;
    }
}
