using UnityEngine;

public class DamageBound : MonoBehaviour
{
    public GameObject collidedObject;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) collidedObject = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collidedObject = null;
    }
}
