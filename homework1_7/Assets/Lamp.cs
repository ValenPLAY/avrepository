using UnityEngine;

public class Lamp : MonoBehaviour
{
    [Header("Lamp Settings")]
    [SerializeField] bool isBreakingInRange;
    [SerializeField] float lampBreakChance;
    private float lampBreakChanceCurrent;

    [Header("Prefab settings")]
    [SerializeField] GameObject brokenPrefab;
    [SerializeField] GameObject brakeSoundPrefab;


    private void Awake()
    {
        lampBreakChanceCurrent = Random.Range(0.0f, 1.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerCharacter enteredPlayer = other.GetComponent<PlayerCharacter>();
        if (enteredPlayer != null)
        {
            Break();
        }
    }

    private void Break()
    {
        if (isBreakingInRange && (lampBreakChanceCurrent <= lampBreakChance))
        {
            GameObject brokenLamp = Instantiate(brokenPrefab, transform.position, transform.rotation);
            brokenLamp.transform.parent = transform.parent;
            GameObject playedSound = GameMechanics.playerSound(brakeSoundPrefab, transform.position, 1.0f, 0.3f);
            Destroy(playedSound, 3);
            Destroy(gameObject);
        }
    }
}
