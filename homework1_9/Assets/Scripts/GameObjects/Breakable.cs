using UnityEngine;

public class Breakable : MonoBehaviour
{
    private Animator animator;
    private AudioSource breakSound;
    private bool isBroken = false;

    public GameObject dropItem;
    // Start is called before the first frame update
    void Start()
    {
        breakSound = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isBroken == false)
        {
            animator.SetTrigger("Break");
            breakSound.Play();
            isBroken = true;

            if (dropItem != null) SpawnDrop(dropItem);
        }
    }

    void SpawnDrop(GameObject spawnedDrop)
    {
        GameObject droppedItem = Instantiate(spawnedDrop, transform.position, transform.rotation);
        droppedItem.transform.parent = transform.parent;
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
