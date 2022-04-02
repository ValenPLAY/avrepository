using UnityEngine;

public class Ghost : MonoBehaviour
{
    private PlayerCharacter currentPlayer;
    private Animator animator;

    private SphereCollider actionCollider;
    public float actionColliderRadius = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        currentPlayer = FindObjectOfType<PlayerCharacter>();
        actionCollider = GetComponent<SphereCollider>();
        animator = GetComponent<Animator>();

        actionCollider.radius = actionColliderRadius;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPlayer != null)
        {
            transform.LookAt(currentPlayer.transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Disappear();
    }

    public void Disappear()
    {
        if (animator != null)
        {
            animator.SetTrigger("Spotted");
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
