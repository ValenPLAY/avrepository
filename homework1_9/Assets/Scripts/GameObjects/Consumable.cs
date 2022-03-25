using UnityEngine;

public abstract class Consumable : MonoBehaviour
{
    public Character interactingCharacter;
    private Animator animator;
    private AudioSource audioSource;
    // Start is called before the first frame update
    public virtual void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        interactingCharacter = collision.GetComponent<Character>();
        if (interactingCharacter != null)
        {
            if (animator != null) animator.SetTrigger("Pickup");
            if (audioSource != null) audioSource.Play();
            Pickup();
        }
    }

    public virtual void Pickup()
    {
        Debug.Log("Pickup Consumable");
    }

    public virtual void Despawn()
    {
        Destroy(gameObject);
    }
}
