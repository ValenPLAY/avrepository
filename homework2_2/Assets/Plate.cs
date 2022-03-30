using UnityEngine;

public class Plate : MonoBehaviour
{
    public Gate affectedGate;
    private AudioSource sndDone;
    public bool isPressed = false;

    private void Start()
    {
        sndDone = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {

        if (isPressed == false && other.CompareTag("Unit"))
        {
            Debug.Log(other.gameObject.name + " pressed the button!");

            isPressed = true;
            if (affectedGate != null) affectedGate.plateCheck();


            if (sndDone != null) sndDone.Play();
            GetComponent<Renderer>().material.color = new Color(0.2f, 0.2f, 0.2f);
        }
    }
}
