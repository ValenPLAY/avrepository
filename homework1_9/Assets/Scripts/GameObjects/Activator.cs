using UnityEngine;

public class Activator : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Animator animator;
    private AudioSource audioSource;
    public bool isTrue = false;

    private bool defaultState;
    public bool isOnTimer = false;
    public float timerTillReset = 10;
    private float timerTillResetCurrent;
    // Start is called before the first frame update
    void Start()
    {
        defaultState = isTrue;
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        if (isOnTimer) timerTillResetCurrent = timerTillReset;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Character>();
        if (player != null && isTrue == false)
        {
            Activate(true);
        }
    }

    void Activate(bool changedState)
    {
        Debug.Log("Activated");
        isTrue = changedState;
        animator.SetBool("isPressed", changedState);
        timerTillResetCurrent = timerTillReset;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnTimer && isTrue)
        {
            timerTillResetCurrent -= Time.deltaTime;
            if (timerTillResetCurrent <= 0)
            {
                Activate(false);
            }
        }
    }
}
